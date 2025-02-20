using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using PizzaShop.ViewModels;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using DAL.Repositories;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using DAL.Models;

namespace PizzaShop.Controllers
{
    // [Authorize]
    public class HomeController : Controller
    {
        private readonly EmailService _mailService;
        private readonly EmailSettings _mailSettings;
        private readonly LoginService _loginService;
        private readonly JWTServices _jwtService;

        public HomeController(LoginService loginService, EmailService mailService, EmailSettings mailSettings, JWTServices jwtService)
        {
            _loginService = loginService;
            _mailService = mailService;
            _mailSettings = mailSettings;
            _jwtService = jwtService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            Console.WriteLine(Request.Cookies["AuthToken"]);
            // if (Request.Cookies["AuthToken"]!= null)
            if (Request.Cookies.ContainsKey("AuthToken"))
            {
                Console.WriteLine("Token does exist");
                // Response.Cookies.Delete(CookieUserEmail);
                return RedirectToAction("Dashboard");
            }
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid email or password";
                return View("Index", model);
            }

            var user = await _loginService.AuthenticateUserAsync(model.Email, model.Password);

            if (user != null)
            {
                string token = _jwtService.GenerateToken(user);

                if (model.RememberMe)
                {
                    Console.WriteLine("Inside check Remember me");
                    Console.WriteLine(model.RememberMe);
                    Console.WriteLine(model.Email);

                    CookieOptions options = new CookieOptions()
                    {
                        Domain = "localhost",
                        Path = "/", // Cookie is available within the entire application
                        Expires = model.RememberMe ? DateTime.Now.AddDays(30) : DateTime.Now.AddHours(1),
                        Secure = false, // Ensure the cookie is only sent over HTTPS (set to false for local development)
                        HttpOnly = false, // Prevent client-side scripts from accessing the cookie
                        IsEssential = true // Indicates the cookie is essential for the application to function

                    };
                    Response.Cookies.Append("AuthToken", token, options);
                }

                TempData["Email"] = model.Email;
                return RedirectToAction("Dashboard");
            }

            TempData["Error"] = "User not found!! Try entering valid email and password";
            return RedirectToAction("Index", model);
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Dashboard()
        {
            return View();
        }


        [Authorize(Roles = "Chef")]
        public IActionResult Forgot()
        {
            return View();
        }

        [HttpPost]
        // public async Task<IActionResult> ForgotPassword(string email){
        public async Task<IActionResult> ForgotPassword(ForgotViewModel model)
        {
            string resetCode = Guid.NewGuid().ToString();
            DateTime expirytime = DateTime.UtcNow.AddMinutes(1);
            Console.WriteLine(expirytime);
            model.Token = resetCode;
            model.ExpiryTime = expirytime;

            string resetLink = Url.Action("ResetPassword", "Home", new { email = model.Email, token = resetCode, time = expirytime }, Request.Scheme);
            await _mailService.SendForgotPasswordEmail(model.Email, resetLink, _mailSettings.Host, _mailSettings.EmailId, _mailSettings.Password, _mailSettings.Port);
            TempData["Message"] = "Password reset link has been sent to your email";
            return RedirectToAction("Forgot");
        }
        public IActionResult ResetPassword(string email, string token, DateTime time)
        {
            Console.WriteLine(time);
            if (email.IsNullOrEmpty())
            {
                TempData["Error"] = "Invalid email";
                return View();
            }
            if (time < DateTime.UtcNow)
            {
                TempData["Error"] = "Reset Token Expired !!";
                return View("Index");
            }
            var model = new ResetPasswordViewModel { Email = email, Token = token, ExpiryToken = time };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Enter valid new password!!";
                return View();
            }
            var user = await _loginService.GetUser(model.Email);
            if (user == null)
            {
                TempData["Error"] = "User not found!! Try entering valid email and password";
                return View();
            }

            if (model.Token != Request.Form["Token"] || model.ExpiryToken < DateTime.UtcNow)
            {
                TempData["Error"] = "Invalid token or Token Expired!!";
                return View("Index");
            }
            _loginService.UpdatePasswordService(user, model.NewPassword);

            TempData["Message"] = "Password Updated Successfully!";
            return RedirectToAction("Index");
        }
    }

}


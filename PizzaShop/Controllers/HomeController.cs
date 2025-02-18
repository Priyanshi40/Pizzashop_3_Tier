using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using PizzaShop.ViewModels;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using DAL.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace PizzaShop.Controllers
{
    public class HomeController : Controller
    {
        const string CookieUserEmail = "UserEmail";
        private readonly EmailService _mailService;
        private readonly EmailSettings _mailSettings;
        private readonly LoginService _loginService;
        
        public HomeController(LoginService loginService, EmailService mailService,EmailSettings mailSettings)
        {
            _loginService = loginService;
            _mailService = mailService;
            _mailSettings = mailSettings;

        }
        public IActionResult Index()
        {
            if (Request.Cookies.ContainsKey(CookieUserEmail))
            {
                Response.Cookies.Delete(CookieUserEmail);
                // return View("Dashboard");
            }
            return View(new LoginViewModel());
        }

        public IActionResult Forgot()
        {
            // ViewData["Email"] = TempData["Email"];
            // TempData.Keep("Email");
            // Console.WriteLine(email);
            // var model = new LoginViewModel
            // {
            //     Email = email
            // };
            // Console.WriteLine(model.Email);
            return View();
        }
        public IActionResult ResetPassword(string email)
        {
            if(email.IsNullOrEmpty()){
                TempData["Error"] = "Invalid email";
                return View();
            }
            var model = new ResetPasswordViewModel { Email = email};
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Enter valid password!!";
                return View();
            }
            var user = await _loginService.GetUser(model.Email);
            if (user == null)
            {
                TempData["Error"] = "User not found!! Try entering valid email and password";
                return View();
            }
            _loginService.UpdatePasswordService(user, model.NewPassword);

            TempData["Message"] = "Password Updated Successfully!";
            return RedirectToAction("Index");
        }
        public IActionResult Dashboard()
        {
            return View();
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
            if (user == null)
            {
                TempData["Error"] = "User not found!! Try entering valid email and password";
                return RedirectToAction("Index", model);
            }

            if (user != null)
            {
                // Console.WriteLine("Inside check not null");
                // Console.WriteLine(model.RememberMe);
                if (model.RememberMe)
                {
                    Console.WriteLine("Inside check Remember me");
                    Console.WriteLine(model.RememberMe);
                    Console.WriteLine(model.Email);
                    CookieOptions options = new CookieOptions()
                    {
                        Domain = "localhost",
                        Path = "/", // Cookie is available within the entire application
                        Expires = DateTime.Now.AddDays(7),
                        Secure = false, // Ensure the cookie is only sent over HTTPS (set to false for local development)
                        HttpOnly = true, // Prevent client-side scripts from accessing the cookie
                        IsEssential = true // Indicates the cookie is essential for the application to function
                    };
                    Response.Cookies.Append(CookieUserEmail, model.Email, options);

                }
            }

            TempData["Email"] = model.Email;
            //  HttpContext.Session.SetString("Email", model.Email ?? "");
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        // public async Task<IActionResult> ForgotPassword(string email){
        public async Task<IActionResult> ForgotPassword(string email){
            string resetLink = Url.Action("ResetPassword", "Home", new { email = email }, Request.Scheme);
            await _mailService.SendForgotPasswordEmail(email,resetLink,_mailSettings.Host,_mailSettings.EmailId,_mailSettings.Password,_mailSettings.Port);
            TempData["Message"]= "Password reset link has been sent to your email";
            return RedirectToAction("Forgot");
        }
    }

}


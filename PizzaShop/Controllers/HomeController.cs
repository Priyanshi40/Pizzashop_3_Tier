using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using PizzaShop.ViewModels;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;

namespace PizzaShop.Controllers
{
    public class HomeController : Controller
    {
        const string CookieUserEmail = "UserEmail";
        private readonly MailService _mailService;
        public HomeController(MailService mailService)
        {
            _mailService = mailService;
        }
        private readonly LoginService _loginService;
        //  public readonly PizzashopContext _context;
        public HomeController(LoginService loginService)
        {
            _loginService = loginService;
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

        public IActionResult Forgot(string? email)
        {
            // ViewData["Email"] = TempData["Email"];
            // TempData.Keep("Email");
            Console.WriteLine(email);
            var model = new LoginViewModel
            {
                Email = email
            };
            Console.WriteLine(model.Email);
            return View(model);
        }
        public IActionResult ResetPassword(string? token)
        {
            ViewBag.Token = token;
            return View();
        }
        public IActionResult ResetPassword(string? token, string? newPassword)
        {
            TempData["Message"] = "password Updated Successfully!";
            return RedirectToAction("Login");
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult Error()
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
        public async Task<IActionResult> ForgotPassword(string email){
            string resetToken = Guid.NewGuid().ToString();
            string resetLink = Url.Action("ResetPassword", "Home", new { token = resetToken }, Request.Scheme);
            await _mailService.SendForgotPasswordEmail(email,resetLink);
            TempData["Message"]= "Password reset link has been sent to your email";
            return RedirectToAction("Login");
        }
        
    
    }

    // public class MailService
    // {
    //     MailSettings Mail_Settings = null;
    //     public MailService(IOptions<MailSettings> options)
    //     {
    //         Mail_Settings = options.Value;
    //     }
    //     public bool SendMail(MailData Mail_Data)
    //     {
    //         try
    //         {
    //             //MimeMessage - a class from Mimekit
    //             MimeMessage email_Message = new MimeMessage();
    //             MailboxAddress email_From = new MailboxAddress(Mail_Settings.Name, Mail_Settings.EmailId);
    //             email_Message.From.Add(email_From);
    //             MailboxAddress email_To = new MailboxAddress(Mail_Data.EmailToName, Mail_Data.EmailToId);
    //             email_Message.To.Add(email_To);
    //             email_Message.Subject = Mail_Data.EmailSubject;
    //             BodyBuilder emailBodyBuilder = new BodyBuilder();
    //             emailBodyBuilder.TextBody = Mail_Data.EmailBody;
    //             email_Message.Body = emailBodyBuilder.ToMessageBody();
    //             //this is the SmtpClient class from the Mailkit.Net.Smtp namespace, not the System.Net.Mail one
    //             SmtpClient MailClient = new SmtpClient();
    //             MailClient.Connect(Mail_Settings.Host, Mail_Settings.Port, Mail_Settings.UseSSL);
    //             MailClient.Authenticate(Mail_Settings.EmailId, Mail_Settings.Password);
    //             MailClient.Send(email_Message);
    //             MailClient.Disconnect(true);
    //             MailClient.Dispose();
    //             return true;
    //         }
    //         catch (Exception ex)
    //         {
    //             // Exception Details
    //             return false;
    //         }
    //     }
    // }
}


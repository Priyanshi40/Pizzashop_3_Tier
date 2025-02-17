﻿using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using PizzaShop.ViewModels;

namespace PizzaShop.Controllers
{
    public class HomeController : Controller
    {
         private readonly LoginService _loginService;  
        //  public readonly PizzashopContext _context;

        public HomeController(LoginService loginService)
        {
           _loginService = loginService;
        }

        public IActionResult Index()
        {
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
                return View("Index",model);
            }

            var user = await _loginService.AuthenticateUserAsync(model.Email, model.Password);

            // Console.WriteLine(model.Email);
            // Console.WriteLine(model.Password);

            if (user == null)
            {
                TempData["Error"] = "User not found!! Try entering valid email and password";
                return RedirectToAction("Index",model);
            }

            TempData["Email"] = model.Email;
            //  HttpContext.Session.SetString("Email", model.Email ?? "");
            return RedirectToAction("Dashboard");
        }
    }
}


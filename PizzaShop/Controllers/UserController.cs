using Microsoft.AspNetCore.Mvc;

namespace PizzaShop.Controllers;

public class UserController : Controller
{
    public IActionResult Add_User()
    {
        return View();
    }

    public IActionResult Edit_user()
    {
        return View();
    }

    public IActionResult User_List()
    {
        return View();
    }
}

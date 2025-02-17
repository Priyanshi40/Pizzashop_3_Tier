using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PizzaShop.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
     public string Password { get; set; } = null!;

    [DisplayName("Remember Me")]
     public bool RememberMe {get; set;}
}

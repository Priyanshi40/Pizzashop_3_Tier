using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PizzaShop.ViewModels;

public class ResetPasswordViewModel
{
    public string Token {get; set;} = null!;
    public DateTime ExpiryToken {get; set;}
    [Required]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "New Password is required")]
    public string NewPassword { get; set; } = null!;

    [Required(ErrorMessage = "Confirm Password is required")]
    [Compare("NewPassword", ErrorMessage = "Passwords do  not match !!")]
    public string ConfirmPassword { get; set; } = null!;

}

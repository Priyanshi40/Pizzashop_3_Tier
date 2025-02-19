using System.ComponentModel.DataAnnotations;

namespace PizzaShop.ViewModels;

public class ForgotViewModel
{
    [Required]
    public string Token {get; set;} = null!;
    public DateTime ExpiryTime {get; set;} 
    [Required]
    public string Email { get; set; } = null!;
}

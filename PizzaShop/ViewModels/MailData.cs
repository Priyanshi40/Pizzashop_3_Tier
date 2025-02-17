namespace PizzaShop.ViewModels;

public class MailData
{
    public required string EmailToId { get; set; }
    public required string EmailToName { get; set; }
    public string? EmailSubject { get; set; }
    public required string EmailBody { get; set; }
}

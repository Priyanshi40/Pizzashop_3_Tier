using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;

namespace BLL.Services;

public class EmailService
{
    // MailSettings Mail_Settings = null;
    // public EmailService(IOptions<EmailSettings> options)
    // {
    //    Mail_Settings = options.Value;
    // }

    // private readonly IConfiguration _config;
    // public EmailService(IConfiguration config)
    // {
    //     _config = config;
    // }

    public async Task SendForgotPasswordEmail(string toEmail, string resetLink,string Host,string senderEmail,string password, int port)
    {
        
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("PizzaSHop", senderEmail));
        email.To.Add(new MailboxAddress("Recepient", toEmail));
        email.Subject = "Reset Your Password";
        email.Body = new TextPart("html")
        {
            Text = $"<h2> Reset Your Password</h2>" + $"<a href='{resetLink}'>Reset Password</a>"
        };

        using var Smtp = new SmtpClient();
        await Smtp.ConnectAsync(Host, port,false);
        await Smtp.AuthenticateAsync(senderEmail,password);
        await Smtp.SendAsync(email);
        await Smtp.DisconnectAsync(true);

    }
}

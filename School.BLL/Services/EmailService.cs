using MailKit.Net.Smtp;
using MimeKit;

namespace School.BLL.Services
{
    public class EmailService
    {
        public void SendEmailAsync(string email, string phoneNumber, string message, string name)
        {
            var emailMessage = new MimeMessage();
            
            emailMessage.From.Add(new MailboxAddress("Admin","nazarii.fisenko@gmail.com"));
            emailMessage.To.Add(new MailboxAddress(name,email));
            emailMessage.Subject = "ContactUs";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = phoneNumber + "<br/>" + message
            };

            using var client = new SmtpClient();
            client.Connect("smtp.gmail.com",
                587,
                false);
            client.Authenticate("nazarii.fisenko@gmail.com",
                "password");
            client.Send(emailMessage);

            client.Disconnect(true);
        }
    }
}
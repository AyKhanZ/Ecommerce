using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace ASP_Project.Services.Classes
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            SmtpClient Client = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = "zeynalovayxan70@gmail.com",
                    Password = "vvcssdtkqcjezuay"
                }
            };

            MailAddress FromEmail = new MailAddress("zeynalovayxan70@gmail.com", "From");
            MailAddress ToEmail = new MailAddress(email, "To");


            MailMessage Message = new MailMessage()
            {
                From = FromEmail,
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            Message.To.Add(ToEmail);

            try
            {
                await Client.SendMailAsync(Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

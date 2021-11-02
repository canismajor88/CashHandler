using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using CashHandlerAPI.ViewModels;

namespace CashHandlerAPI.Helper
{
    public class EmailHelper:IEmailHelper
    {
        public async Task Send(string emailAddress, string body, EmailOptions emailOptions)
        {
            MailMessage mailMessage = new MailMessage(emailOptions.SenderEmail, emailAddress);
            mailMessage.Subject = "verify email";
            mailMessage.Body = body;
            SmtpClient smtpClient = new("smtp.gmail.com", 587);
            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "cashhandler17@gmail.com",
                Password = "LotroFan123!"
            };
            smtpClient.EnableSsl=true;
            smtpClient.Send(mailMessage);
        }
    }
}

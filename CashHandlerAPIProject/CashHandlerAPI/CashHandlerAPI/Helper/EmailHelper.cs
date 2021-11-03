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
            MailMessage mailMessage = new MailMessage(emailOptions.UserName_SenderEmail, emailAddress);
            mailMessage.Subject = "verify email";
            mailMessage.Body = body;
            SmtpClient smtpClient = new(emailOptions.Host, emailOptions.Port);
            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = emailOptions.UserName_SenderEmail,
                Password = emailOptions.ApiKeySecret
            };
            smtpClient.EnableSsl=true;
            smtpClient.Send(mailMessage);
        }
    }
}

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
            var client = new SmtpClient();
            client.Host = emailOptions.Host;
            client.Credentials = new NetworkCredential(emailOptions.ApiKey, emailOptions.ApiKeySecret);
            client.Port = emailOptions.Port;
            var message = new MailMessage(emailOptions.SenderEmail, emailAddress);
            message.Body = body;
            message.IsBodyHtml = true;
            await client.SendMailAsync(message);
        }
    }
}

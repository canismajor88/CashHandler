using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using CashHandlerAPI.ViewModels;

namespace CashHandlerAPI.Helper
{
    public class EmailHelper:IEmailHelper
    {
        #region public methods
        public Task Send(string emailAddress, string body, string subject ,EmailOptions emailOptions)
        {
            MailMessage mailMessage = new(emailOptions.UserName_SenderEmail, emailAddress);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            SmtpClient smtpClient = new(emailOptions.Host, emailOptions.Port);
            smtpClient.Credentials = new NetworkCredential()
            {
                UserName = emailOptions.UserName_SenderEmail,
                Password = emailOptions.ApiKeySecret
            };
            smtpClient.EnableSsl=true;
            smtpClient.Send(mailMessage);
            return Task.CompletedTask;
        }

        public  string UrlStringBuilder(string receiverAddress, string token, string userId)
        {
            var uriBuilder = new UriBuilder(receiverAddress);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["token"] = token;
            query["userid"] = userId;
            uriBuilder.Query = query.ToString();
            return uriBuilder.ToString();
        }

        #endregion
        
    }
}

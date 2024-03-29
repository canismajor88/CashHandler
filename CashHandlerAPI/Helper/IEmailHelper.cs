﻿using System.Threading.Tasks;
using CashHandlerAPI.ViewModels;

namespace CashHandlerAPI.Helper
{
   public interface IEmailHelper
   {
       public Task Send(string emailAddress, string body, string subject, EmailOptions emailOptions);
       public string UrlStringBuilder(string receiverAddress, string token, string userId);
   }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CashHandlerAPI.Models;

namespace CashHandlerAPI.Helper
{
   public interface IEmailHelper
   {
       Task Send(string emailAddress, string body, EmailOptions emailOptions);
   }
}

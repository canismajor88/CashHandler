using System.Threading.Tasks;
using CashHandlerAPI.ViewModels;

namespace CashHandlerAPI.Helper
{
   public interface IEmailHelper
   {
       Task Send(string emailAddress, string body, EmailOptions emailOptions);
   }
}

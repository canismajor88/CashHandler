using System;
using System.Linq;
using System.Threading.Tasks;
using CashHandlerAPI.CashHandlerLogic;
using CashHandlerAPI.Data;
using CashHandlerAPI.Models;
using CashHandlerAPI.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CashHandlerAPI.Helper
{
    public class DatabaseHelper:IDatabaseHelper
    {
        #region private
        private readonly UserManager<User> _userManager;
        private readonly CashHandlerDBContext _context;
        #endregion

        #region constructors
        public DatabaseHelper(UserManager<User> userManager, CashHandlerDBContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        #endregion

        #region public methods

        #region AuthHelpers
        /// <summary>
        /// checks if username and password are a valid user in DB
        /// </summary>
        public async Task<bool> IsValidLogin(string userName, string password)
        {
            var currentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            if (currentUser == null) return false;
            var passwordResult = _userManager.PasswordHasher.VerifyHashedPassword(currentUser, currentUser.PasswordHash,
                password);
            if (passwordResult == PasswordVerificationResult.Failed) return false;
            if (currentUser.EmailConfirmed == false) return false;
            currentUser.LastSignIn = DateTime.Now;
            return await _context.SaveChangesAsync(true) > 0;
        }
        /// <summary>
        /// creates new user with a username, password, and email
        /// </summary>
        public async Task<bool> CreateNewUser(string userName, string password, string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            if (user != null) return false;
            var newUser = new User
            {
                UserName = userName,
                Email = email,
            };

            var result = await _userManager.CreateAsync(newUser, password);
            if (!result.Succeeded) return false;
            var currentUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == newUser.Id);


            var moneyAmountResult = await _context.MoneyAmounts.AddAsync(new MoneyAmount
            {

                TotalAmount = 0,
                DollarCoinAmount = 0,
                HalfDollarAmount = 0,
                QuartersAmount = 0,
                DimesAmount = 0,
                NicklesAmount = 0,
                PenniesAmount = 0,
                HundredsAmount = 0,
                FiftiesAmount = 0,
                TwentiesAmount = 0,
                TensAmount = 0,
                FivesAmount = 0,
                OnesAmount = 0
            });
            currentUser.MoneyAmount = moneyAmountResult.Entity;
            _context.Update(currentUser);
            return await _context.SaveChangesAsync(true) > 0;
        }
        /// <summary>
        /// confirms email for user and takes a permissions token
        /// </summary>
        public async Task<bool> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var confirm = await _userManager.ConfirmEmailAsync(user, Uri.UnescapeDataString(token));
            return confirm.Succeeded;
        }
        /// <summary>
        /// changes password for user takes a permissions token
        /// </summary>
        public async Task<bool> ChangePassword(string userId, string token, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            return result.Succeeded;
        }

        #endregion


        #region MoneyAmountsHelpers
        /// <summary>
        /// updates a moneyAmount and updates its values to the values in moneyAmountViewModel
        /// </summary>
        public async Task<bool> UpdateMoneyAmount(MoneyAmountViewModel moneyAmountViewModel, string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return false;
            var moneyAmountDB = await _context.MoneyAmounts.FindAsync(user.MoneyAmountId);
            if (moneyAmountDB.PenniesAmount == null) return false;
            moneyAmountDB = MoneyAmountsLogic.UpdateMoneyAmount(moneyAmountDB, moneyAmountViewModel);
            _context.Update(user);
            _context.Update(moneyAmountDB);
            return await _context.SaveChangesAsync(true) > 0;
        }
        /// <summary>
        /// re-balances money amount to target amount
        /// </summary>
        public async Task<ReBalanceResult> ReBalanceMoneyAmount(decimal targetAmount, string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return new ReBalanceResult{Success = false};
            var moneyAmountDB = await _context.MoneyAmounts.FindAsync(user.MoneyAmountId);
            if (moneyAmountDB == null) return new ReBalanceResult{Success = false};
            var oldMoneyAmounts = MoneyAmountsLogic.CreateMoneyAmountViewModel(moneyAmountDB);
            moneyAmountDB = MoneyAmountsLogic.ReBalanceMoneyAmount(moneyAmountDB, targetAmount);
            if (moneyAmountDB.PenniesAmount == -1)
                return new ReBalanceResult
                {
                    Success = false,
                    TakeOutString = "Bills need to be broken or not enough in Money Amounts to Re-Balance"
                };
            await _context.Transactions.AddAsync(new Transaction
            {
                Amount = -1 * ((double)oldMoneyAmounts.TotalAmount - (double)targetAmount),
                TransDate = DateTime.Now,
                Denominations = "Re-Balance and Money Amounts Withdraw",
                UserId = user.Id,
                User = user
            });
           _context.Update(moneyAmountDB);
           if (await _context.SaveChangesAsync(true) <= 0) return new ReBalanceResult { Success = false };
           return new ReBalanceResult
           {
               Success = true, TakeOutString = MoneyAmountsLogic.GenerateTakeOutString(moneyAmountDB, oldMoneyAmounts)
           };
        }

        #endregion

        #region transactionHelpers


        /// <summary>
        /// runs transaction and add it to database
        /// moneyAmountViewModel here is with whatever amount customer has given
        /// </summary>
        public async Task<AddTransactionResult> RunTransaction(MoneyAmountViewModel moneyAmountViewModel, string username, decimal itemCost)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return new AddTransactionResult { Success = false };
            var moneyAmountDB = await _context.MoneyAmounts.FindAsync(user.MoneyAmountId);
            var originalMoneyAmounts = MoneyAmountsLogic.CreateMoneyAmountViewModel(moneyAmountDB);
            if (moneyAmountDB == null) return new AddTransactionResult { Success = false };
            moneyAmountDB = MoneyAmountsLogic.RunTransaction(moneyAmountDB, moneyAmountViewModel, itemCost);
            if (moneyAmountDB.PenniesAmount==-1)
            {
                return new AddTransactionResult { Success = true,GiveBackString = "Something Went Wrong Need To Re-Balance Money Amounts, can't make change"};
            }
            var giveBackString = MoneyAmountsLogic.GenerateTakeOutString(moneyAmountDB, originalMoneyAmounts);
            await _context.Transactions.AddAsync(new Transaction
            {
                Amount = (double)itemCost,
                TransDate = DateTime.Now,
                Denominations = moneyAmountViewModel.Description,
                UserId = user.Id,
                User = user
            });
            _context.Update(user);
            _context.Update(moneyAmountDB);
            var transactionResult = new AddTransactionResult
            {
                MoneyAmountViewModel = MoneyAmountsLogic.CreateMoneyAmountViewModel(moneyAmountDB),
                Success = await _context.SaveChangesAsync(true) > 0,
                GiveBackString = giveBackString
            };
            return transactionResult;
        }

        public async Task<GetTransactionsResult> GetTransactions(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return new GetTransactionsResult { Success = false };

            return new GetTransactionsResult
            {
                Success = true,
                Transactions = await _context.Transactions.Where(x => x.UserId == user.Id).ToListAsync()
            };

        }

        public async Task<Transaction> GetTransaction(long transactionId)
        {
            return await _context.Transactions.FirstOrDefaultAsync(x => x.TransactionId == transactionId);
        }

        public async Task<GetMoneyAmountResult> GetMoneyAmountViewModel(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var moneyAmountDB = await _context.MoneyAmounts.FindAsync(user.MoneyAmountId);
            if (moneyAmountDB == null) return new GetMoneyAmountResult { Success = false };
            return new GetMoneyAmountResult
            {
                Success = true,
                MoneyAmountViewModel = MoneyAmountsLogic.CreateMoneyAmountViewModel(moneyAmountDB)
            };
        }


        #endregion



        #endregion
    }
}

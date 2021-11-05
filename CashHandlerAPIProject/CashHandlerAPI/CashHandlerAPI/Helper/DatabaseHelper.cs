using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CashHandlerAPI.Data;
using CashHandlerAPI.Models;
using CashHandlerAPI.ViewModels;
using Microsoft.AspNetCore.Identity;
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
        public async Task<bool> IsValidLogin(string userName, string password)
        {
            var currentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            if (currentUser == null) return false;
            var passwordResult = _userManager.PasswordHasher.VerifyHashedPassword(currentUser, currentUser.PasswordHash,
                password);
            if (passwordResult == PasswordVerificationResult.Failed) return false;
            return currentUser.EmailConfirmed != false;
        }

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

        public async Task<bool> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var confirm = await _userManager.ConfirmEmailAsync(user, Uri.UnescapeDataString(token));
            return confirm.Succeeded;
        }

        public async Task<bool> ChangePassword(string userId, string token, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            return result.Succeeded;
        }

        public async Task<bool> InitializeMoneyAmount(MoneyAmountViewModel moneyamout, string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var moneyAmountDB = await _context.MoneyAmounts.FindAsync(user.MoneyAmountId);
            if (moneyamout == null) return false;
            

            moneyAmountDB.DollarCoinAmount = moneyamout.DollarCoinAmount;
            moneyAmountDB.HalfDollarAmount = moneyamout.HalfDollarAmount;
            moneyAmountDB.QuartersAmount = moneyamout.QuartersAmount;
            moneyAmountDB.DimesAmount = moneyamout.DimesAmount;
            moneyAmountDB.NicklesAmount = moneyamout.NicklesAmount;
            moneyAmountDB.PenniesAmount = moneyamout.PenniesAmount;
            moneyAmountDB.HundredsAmount = moneyamout.HundredsAmount;
            moneyAmountDB.FiftiesAmount = moneyamout.FiftiesAmount;
            moneyAmountDB.TwentiesAmount = moneyamout.TwentiesAmount;
            moneyAmountDB.TensAmount = moneyamout.TensAmount;
            moneyAmountDB.FivesAmount = moneyamout.FivesAmount;
            moneyAmountDB.OnesAmount = moneyamout.OnesAmount;

            var coinAmount = moneyAmountDB.DollarCoinAmount + moneyAmountDB.HalfDollarAmount * .5 +
                            moneyAmountDB.QuartersAmount * .25 + moneyAmountDB.DimesAmount * .1 +
                            moneyAmountDB.NicklesAmount * .05 + moneyAmountDB.PenniesAmount * .01;

            var dollarAmount = (moneyAmountDB.HundredsAmount * 100 + moneyAmountDB.FiftiesAmount * 50 +
                                  moneyAmountDB.TwentiesAmount * 20 + moneyAmountDB.FivesAmount * 5 +
                                  moneyAmountDB.OnesAmount + moneyAmountDB.TensAmount * 10);
            var total = coinAmount + dollarAmount;
            moneyAmountDB.TotalAmount = (decimal)total;

            _context.Update(user);
            _context.Update(moneyAmountDB);
            return await _context.SaveChangesAsync(true) > 0;
        }
        #endregion
    }
}

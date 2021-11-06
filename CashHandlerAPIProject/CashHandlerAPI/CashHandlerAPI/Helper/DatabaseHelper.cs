﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CashHandlerAPI.CashHandlerLogic;
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

        public async Task<bool> InitializeMoneyAmount(MoneyAmountViewModel moneyamount, string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var moneyAmountDB = await _context.MoneyAmounts.FindAsync(user.MoneyAmountId);
            moneyAmountDB = MoneyAmountsLogic.UpdateMoneyAmount(moneyAmountDB, moneyamount);
            _context.Update(user);
            _context.Update(moneyAmountDB);
            return await _context.SaveChangesAsync(true) > 0;
        }
        #endregion
    }
}

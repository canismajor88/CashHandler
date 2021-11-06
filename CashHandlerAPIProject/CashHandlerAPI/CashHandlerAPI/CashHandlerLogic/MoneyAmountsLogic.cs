using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CashHandlerAPI.Models;
using CashHandlerAPI.ViewModels;

namespace CashHandlerAPI.CashHandlerLogic
{
    public static class MoneyAmountsLogic
    {
      public static decimal GetMoneyAmountsTotal(MoneyAmountViewModel moneyAmount)
        {
            var coinAmount =(decimal)( moneyAmount.DollarCoinAmount + moneyAmount.HalfDollarAmount * .5 +
                             moneyAmount.QuartersAmount * .25 + moneyAmount.DimesAmount * .1 +
                             moneyAmount.NicklesAmount * .05 + moneyAmount.PenniesAmount * .01);

            var dollarAmount =(decimal) (moneyAmount.HundredsAmount * 100 + moneyAmount.FiftiesAmount * 50 +
                                moneyAmount.TwentiesAmount * 20 + moneyAmount.FivesAmount * 5 +
                                moneyAmount.OnesAmount + moneyAmount.TensAmount * 10);
            return coinAmount + dollarAmount;
        }

      public static MoneyAmount RunTransaction(MoneyAmount moneyAmountDB,MoneyAmountViewModel moneyAmountViewModel ,decimal itemCost)
      {
            var realTotal = (decimal) moneyAmountDB.TotalAmount + itemCost;//money we are actually at

            moneyAmountDB = UpdateMoneyAmount(moneyAmountDB, moneyAmountViewModel);//update money amount to what user has given us before we make change
            
            var amountOfChangeToGiveBack = moneyAmountDB.TotalAmount - realTotal;

            if (amountOfChangeToGiveBack <= 0) return moneyAmountDB;

            moneyAmountDB.HundredsAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.HundredsAmount, 100, realTotal, moneyAmountDB);
            moneyAmountDB.FiftiesAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.FiftiesAmount, 50, realTotal, moneyAmountDB);
            moneyAmountDB.TwentiesAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.TwentiesAmount, 20, realTotal, moneyAmountDB);
            moneyAmountDB.TensAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.TensAmount, 10, realTotal, moneyAmountDB);
            moneyAmountDB.FivesAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.FivesAmount, 5, realTotal, moneyAmountDB);
            moneyAmountDB.OnesAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.OnesAmount, 1, realTotal, moneyAmountDB);
            moneyAmountDB.DollarCoinAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.DollarCoinAmount, 1, realTotal, moneyAmountDB);
            moneyAmountDB.HalfDollarAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.HalfDollarAmount, (decimal).5, realTotal, moneyAmountDB);
            moneyAmountDB.QuartersAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.QuartersAmount, (decimal).25, realTotal, moneyAmountDB);
            moneyAmountDB.DimesAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.DimesAmount, (decimal).1, realTotal, moneyAmountDB);
            moneyAmountDB.NicklesAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.NicklesAmount, (decimal).05, realTotal, moneyAmountDB);
            moneyAmountDB.PenniesAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.PenniesAmount, (decimal).01, realTotal, moneyAmountDB);
            // todo calculates correctly but still need a way to give message back to user
            return moneyAmountDB;
      }
        private static int TransactionAmountsProcessor(int moneyDenominationAmount, decimal denominationWorth, decimal targetAmount, MoneyAmount moneyAmountDB)
        {
            int i;
            for (i = 0; i != moneyDenominationAmount && moneyAmountDB.TotalAmount > targetAmount; i++)
                moneyAmountDB.TotalAmount -= denominationWorth;
            //put the bill back
            if (moneyAmountDB.TotalAmount < targetAmount)
            {
                i--;
                moneyAmountDB.TotalAmount += denominationWorth;
            }

            return i;
        }
        public static MoneyAmount UpdateMoneyAmount(MoneyAmount moneyAmountDB, MoneyAmountViewModel moneyAmountViewModel)
      {
          moneyAmountDB.DollarCoinAmount = moneyAmountViewModel.DollarCoinAmount;
          moneyAmountDB.HalfDollarAmount = moneyAmountViewModel.HalfDollarAmount;
          moneyAmountDB.QuartersAmount = moneyAmountViewModel.QuartersAmount;
          moneyAmountDB.DimesAmount = moneyAmountViewModel.DimesAmount;
          moneyAmountDB.NicklesAmount = moneyAmountViewModel.NicklesAmount;
          moneyAmountDB.PenniesAmount = moneyAmountViewModel.PenniesAmount;
          moneyAmountDB.HundredsAmount = moneyAmountViewModel.HundredsAmount;
          moneyAmountDB.FiftiesAmount = moneyAmountViewModel.FiftiesAmount;
          moneyAmountDB.TwentiesAmount = moneyAmountViewModel.TwentiesAmount;
          moneyAmountDB.TensAmount = moneyAmountViewModel.TensAmount;
          moneyAmountDB.FivesAmount = moneyAmountViewModel.FivesAmount;
          moneyAmountDB.OnesAmount = moneyAmountViewModel.OnesAmount;
          moneyAmountDB.TotalAmount = GetMoneyAmountsTotal(moneyAmountViewModel);
          return moneyAmountDB;
      }

        public static MoneyAmountViewModel CreateMoneyAmountViewModel(MoneyAmount moneyAmountDb)
        {
            return new MoneyAmountViewModel
            {
                DollarCoinAmount = (int)moneyAmountDb.DollarCoinAmount,
                HalfDollarAmount = (int)moneyAmountDb.HalfDollarAmount,
                QuartersAmount = (int)moneyAmountDb.QuartersAmount,
                DimesAmount = (int)moneyAmountDb.DimesAmount,
                NicklesAmount = (int)moneyAmountDb.NicklesAmount,
                PenniesAmount = (int)moneyAmountDb.PenniesAmount,
                HundredsAmount = (int)moneyAmountDb.HundredsAmount,
                FiftiesAmount = (int)moneyAmountDb.FiftiesAmount,
                TwentiesAmount = (int)moneyAmountDb.TwentiesAmount,
                TensAmount = (int)moneyAmountDb.TensAmount,
                FivesAmount = (int)moneyAmountDb.FivesAmount,
                OnesAmount = (int)moneyAmountDb.OnesAmount,
                TotalAmount = (int)moneyAmountDb.TotalAmount
            };
        }
    }
}

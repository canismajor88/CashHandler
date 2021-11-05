using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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


    }
}

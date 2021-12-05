using CashHandlerAPI.Models;
using CashHandlerAPI.ViewModels;

namespace CashHandlerAPI.CashHandlerLogic
{
    public static class MoneyAmountsLogic
    {
        #region private
        /// <summary>
        /// creates an invalid moneyAmount object
        /// </summary>
        private static MoneyAmount GenerateInvalidMoneyAmount()
        {
            return new MoneyAmount
            {
                PenniesAmount = -1,
                NicklesAmount = -1,
                DimesAmount = -1,
                QuartersAmount = -1,
                HalfDollarAmount = -1,
                DollarCoinAmount = -1,
                OnesAmount = -1,
                FivesAmount = -1,
                TensAmount = -1,
                TwentiesAmount = -1,
                FiftiesAmount = -1,
                HundredsAmount = -1
            };
        }

        #endregion
        #region public
        /// <summary>
        /// get total dollar amount associated with moneyAmount
        /// </summary>
        public static decimal GetMoneyAmountsTotal(MoneyAmount moneyAmount)
        {
          
            var coinAmount = (decimal)(moneyAmount.DollarCoinAmount + (moneyAmount.HalfDollarAmount * .5) +
                             moneyAmount.QuartersAmount * .25 + moneyAmount.DimesAmount * .1 +
                             moneyAmount.NicklesAmount * .05 + moneyAmount.PenniesAmount * .01);

            var dollarAmount = (decimal)(moneyAmount.HundredsAmount * 100 + moneyAmount.FiftiesAmount * 50 +
                                moneyAmount.TwentiesAmount * 20 + moneyAmount.FivesAmount * 5 +
                                moneyAmount.OnesAmount + moneyAmount.TensAmount * 10);
            return coinAmount + dollarAmount;
        }

        

        /// <summary>
        /// re-balances money amount to target amount
        /// </summary>
        public static MoneyAmount ReBalanceMoneyAmount(MoneyAmount moneyAmount, decimal targetAmount)
        {
            if (moneyAmount.TotalAmount <= targetAmount) return GenerateInvalidMoneyAmount();
            moneyAmount.HundredsAmount -=
                TransactionAmountsProcessor((int)moneyAmount.HundredsAmount, 100, targetAmount, moneyAmount);
          

            moneyAmount.FiftiesAmount -=
                TransactionAmountsProcessor((int)moneyAmount.FiftiesAmount, 50, targetAmount, moneyAmount);
         

            moneyAmount.TwentiesAmount -=
                TransactionAmountsProcessor((int)moneyAmount.TwentiesAmount, 20, targetAmount, moneyAmount);
           

            moneyAmount.TensAmount -=
                TransactionAmountsProcessor((int)moneyAmount.TensAmount, 10, targetAmount, moneyAmount);
        

            moneyAmount.FivesAmount -=
                TransactionAmountsProcessor((int)moneyAmount.FivesAmount, 5, targetAmount, moneyAmount);
         

            moneyAmount.OnesAmount -=
                TransactionAmountsProcessor((int)moneyAmount.OnesAmount, 1, targetAmount, moneyAmount);
           

            moneyAmount.DollarCoinAmount -=
                TransactionAmountsProcessor((int)moneyAmount.DollarCoinAmount, 1, targetAmount, moneyAmount);
      

            moneyAmount.HalfDollarAmount -=
                TransactionAmountsProcessor((int)moneyAmount.HalfDollarAmount, (decimal).5, targetAmount, moneyAmount);
            

            moneyAmount.QuartersAmount -=
                TransactionAmountsProcessor((int)moneyAmount.QuartersAmount, (decimal).25, targetAmount, moneyAmount);
           

            moneyAmount.DimesAmount -=
                TransactionAmountsProcessor((int)moneyAmount.DimesAmount, (decimal).1, targetAmount, moneyAmount);
          

            moneyAmount.NicklesAmount -=
                TransactionAmountsProcessor((int)moneyAmount.NicklesAmount, (decimal).05, targetAmount, moneyAmount);
          

            moneyAmount.PenniesAmount -=
                TransactionAmountsProcessor((int)moneyAmount.PenniesAmount, (decimal).01, targetAmount, moneyAmount);

            return moneyAmount.TotalAmount != targetAmount ? GenerateInvalidMoneyAmount() : moneyAmount;
        }
        /// <summary>
        /// runs transaction and adjust money amounts
        /// </summary>
        public static MoneyAmount RunTransaction(MoneyAmount moneyAmount, MoneyAmountViewModel moneyAmountViewModel, decimal itemCost)
        {
            var realTotal = (decimal)moneyAmount.TotalAmount + itemCost;//money we are actually at

            moneyAmount = AddMoneyAmounts(moneyAmount, moneyAmountViewModel);//update money amount to what user has given us before we make change
            
            var amountOfChangeToGiveBack = moneyAmount.TotalAmount - realTotal;
            //can we make that much change
            if (amountOfChangeToGiveBack < 0) return GenerateInvalidMoneyAmount();

            if (amountOfChangeToGiveBack == 0) return moneyAmount;

            moneyAmount.HundredsAmount -=
                TransactionAmountsProcessor((int)moneyAmount.HundredsAmount, 100, realTotal, moneyAmount);
         
           
            
            moneyAmount.FiftiesAmount -=
                TransactionAmountsProcessor((int)moneyAmount.FiftiesAmount, 50, realTotal, moneyAmount);
          
           
            moneyAmount.TwentiesAmount -=
                TransactionAmountsProcessor((int)moneyAmount.TwentiesAmount, 20, realTotal, moneyAmount);
           
           
            moneyAmount.TensAmount -=
                TransactionAmountsProcessor((int)moneyAmount.TensAmount, 10, realTotal, moneyAmount);
           
            
            moneyAmount.FivesAmount -=
                TransactionAmountsProcessor((int)moneyAmount.FivesAmount, 5, realTotal, moneyAmount);
           

            moneyAmount.OnesAmount -=
                TransactionAmountsProcessor((int)moneyAmount.OnesAmount, 1, realTotal, moneyAmount);
  

            moneyAmount.DollarCoinAmount -=
                TransactionAmountsProcessor((int)moneyAmount.DollarCoinAmount, 1, realTotal, moneyAmount);
           

            moneyAmount.HalfDollarAmount -=
                TransactionAmountsProcessor((int)moneyAmount.HalfDollarAmount, (decimal).5, realTotal, moneyAmount);
           

            moneyAmount.QuartersAmount -=
                TransactionAmountsProcessor((int)moneyAmount.QuartersAmount, (decimal).25, realTotal, moneyAmount);
         

            moneyAmount.DimesAmount -=
                TransactionAmountsProcessor((int)moneyAmount.DimesAmount, (decimal).1, realTotal, moneyAmount);
       

            moneyAmount.NicklesAmount -=
                TransactionAmountsProcessor((int)moneyAmount.NicklesAmount, (decimal).05, realTotal, moneyAmount);
          

            moneyAmount.PenniesAmount -=
                TransactionAmountsProcessor((int)moneyAmount.PenniesAmount, (decimal).01, realTotal, moneyAmount);
           

            return moneyAmount.TotalAmount != realTotal ? GenerateInvalidMoneyAmount() : moneyAmount;
        }
        /// <summary>
        /// finds how money bills to take out of the denomination put in to reach target amount
        /// </summary>
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
        /// <summary>
        /// updates money amount to view model totals
        /// </summary>
        public static MoneyAmount UpdateMoneyAmount(MoneyAmount moneyAmount, MoneyAmountViewModel moneyAmountViewModel)
        {
            moneyAmount.DollarCoinAmount = moneyAmountViewModel.DollarCoinAmount;
            moneyAmount.HalfDollarAmount = moneyAmountViewModel.HalfDollarAmount;
            moneyAmount.QuartersAmount = moneyAmountViewModel.QuartersAmount;
            moneyAmount.DimesAmount = moneyAmountViewModel.DimesAmount;
            moneyAmount.NicklesAmount = moneyAmountViewModel.NicklesAmount;
            moneyAmount.PenniesAmount = moneyAmountViewModel.PenniesAmount;
            moneyAmount.HundredsAmount = moneyAmountViewModel.HundredsAmount;
            moneyAmount.FiftiesAmount = moneyAmountViewModel.FiftiesAmount;
            moneyAmount.TwentiesAmount = moneyAmountViewModel.TwentiesAmount;
            moneyAmount.TensAmount = moneyAmountViewModel.TensAmount;
            moneyAmount.FivesAmount = moneyAmountViewModel.FivesAmount;
            moneyAmount.OnesAmount = moneyAmountViewModel.OnesAmount;
            moneyAmount.TotalAmount = GetMoneyAmountsTotal(moneyAmount);
            return moneyAmount;
        }
        /// <summary>
        /// adds money amount total to money amount 
        /// </summary>
        private static MoneyAmount AddMoneyAmounts(MoneyAmount moneyAmount, MoneyAmountViewModel moneyAmountViewModel)
        {
            moneyAmount.DollarCoinAmount += moneyAmountViewModel.DollarCoinAmount;
            moneyAmount.HalfDollarAmount += moneyAmountViewModel.HalfDollarAmount;
            moneyAmount.QuartersAmount += moneyAmountViewModel.QuartersAmount;
            moneyAmount.DimesAmount += moneyAmountViewModel.DimesAmount;
            moneyAmount.NicklesAmount += moneyAmountViewModel.NicklesAmount;
            moneyAmount.PenniesAmount += moneyAmountViewModel.PenniesAmount;
            moneyAmount.HundredsAmount += moneyAmountViewModel.HundredsAmount;
            moneyAmount.FiftiesAmount += moneyAmountViewModel.FiftiesAmount;
            moneyAmount.TwentiesAmount += moneyAmountViewModel.TwentiesAmount;
            moneyAmount.TensAmount += moneyAmountViewModel.TensAmount;
            moneyAmount.FivesAmount += moneyAmountViewModel.FivesAmount;
            moneyAmount.OnesAmount += moneyAmountViewModel.OnesAmount;
            moneyAmount.TotalAmount = GetMoneyAmountsTotal(moneyAmount);
            return moneyAmount;
        }

        /// <summary>
        /// tell user how much to take out. This is the difference in denomination from moneyAmount and moneyAmountViewModel
        /// moneyAmount is altered money amount, MoneyAmountViewModel is original
        /// </summary>
        public static string GenerateTakeOutString(MoneyAmount moneyAmount,
            MoneyAmountViewModel moneyAmountViewModel)
        {
            string output = "Take Out ";
            if (moneyAmountViewModel.HundredsAmount - moneyAmount.HundredsAmount > 0)
                output = output + (moneyAmountViewModel.HundredsAmount - moneyAmount.HundredsAmount) + " hundreds, ";
            if (moneyAmountViewModel.FiftiesAmount - moneyAmount.FiftiesAmount > 0)
                output = output + (moneyAmountViewModel.FiftiesAmount - moneyAmount.FiftiesAmount) + " fifties, ";
            if (moneyAmountViewModel.TwentiesAmount - moneyAmount.TwentiesAmount > 0)
                output = output + (moneyAmountViewModel.TwentiesAmount - moneyAmount.TwentiesAmount) + " twenties, ";
            if (moneyAmountViewModel.TensAmount - moneyAmount.TensAmount > 0)
                output = output + (moneyAmountViewModel.TensAmount - moneyAmount.TensAmount) + " tens, ";
            if (moneyAmountViewModel.FivesAmount - moneyAmount.FivesAmount > 0)
                output = output + (moneyAmountViewModel.FivesAmount - moneyAmount.FivesAmount) + " fives, ";
            if (moneyAmountViewModel.OnesAmount - moneyAmount.OnesAmount > 0)
                output = output + (moneyAmountViewModel.OnesAmount - moneyAmount.OnesAmount) + " ones, ";
            if (moneyAmountViewModel.DollarCoinAmount - moneyAmount.DollarCoinAmount > 0)
                output = output + (moneyAmountViewModel.DollarCoinAmount - moneyAmount.DollarCoinAmount) + " dollar coins, ";
            if (moneyAmountViewModel.HalfDollarAmount - moneyAmount.HalfDollarAmount > 0)
                output = output + (moneyAmountViewModel.HalfDollarAmount - moneyAmount.HalfDollarAmount) + " half dollars, ";
            if (moneyAmountViewModel.QuartersAmount - moneyAmount.QuartersAmount > 0)
                output = output + (moneyAmountViewModel.QuartersAmount - moneyAmount.QuartersAmount) + " quarters, ";
            if (moneyAmountViewModel.DimesAmount - moneyAmount.DimesAmount > 0)
                output = output + (moneyAmountViewModel.DimesAmount - moneyAmount.DimesAmount) + " dimes, ";
            if (moneyAmountViewModel.NicklesAmount - moneyAmount.NicklesAmount > 0)
                output = output + (moneyAmountViewModel.NicklesAmount - moneyAmount.NicklesAmount) + " nickles, ";
            if (moneyAmountViewModel.PenniesAmount - moneyAmount.PenniesAmount > 0)
                output = output + (moneyAmountViewModel.PenniesAmount - moneyAmount.PenniesAmount) + " pennies, ";
            if (output == "Take Out ")
            {
                output = "No change  ";
            }
            //remove white space
            return output.Remove(output.Length-2);
        }
        /// <summary>
        /// creates a moneyAmountViewModel Object
        /// </summary>
        public static MoneyAmountViewModel CreateMoneyAmountViewModel(MoneyAmount moneyAmount)
        {
            return new MoneyAmountViewModel
            {
                DollarCoinAmount = (int)moneyAmount.DollarCoinAmount,
                HalfDollarAmount = (int)moneyAmount.HalfDollarAmount,
                QuartersAmount = (int)moneyAmount.QuartersAmount,
                DimesAmount = (int)moneyAmount.DimesAmount,
                NicklesAmount = (int)moneyAmount.NicklesAmount,
                PenniesAmount = (int)moneyAmount.PenniesAmount,
                HundredsAmount = (int)moneyAmount.HundredsAmount,
                FiftiesAmount = (int)moneyAmount.FiftiesAmount,
                TwentiesAmount = (int)moneyAmount.TwentiesAmount,
                TensAmount = (int)moneyAmount.TensAmount,
                FivesAmount = (int)moneyAmount.FivesAmount,
                OnesAmount = (int)moneyAmount.OnesAmount,
                TotalAmount = (decimal)moneyAmount.TotalAmount
            };
        }

        #endregion

    }
}

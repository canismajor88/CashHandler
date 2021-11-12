using CashHandlerAPI.Models;
using CashHandlerAPI.ViewModels;

namespace CashHandlerAPI.CashHandlerLogic
{
    public static class MoneyAmountsLogic
    {
        #region public

        public static decimal GetMoneyAmountsTotal(MoneyAmount moneyAmount)
        {
          
            var coinAmount = (decimal)(moneyAmount.DollarCoinAmount + moneyAmount.HalfDollarAmount * .5 +
                             moneyAmount.QuartersAmount * .25 + moneyAmount.DimesAmount * .1 +
                             moneyAmount.NicklesAmount * .05 + moneyAmount.PenniesAmount * .01);

            var dollarAmount = (decimal)(moneyAmount.HundredsAmount * 100 + moneyAmount.FiftiesAmount * 50 +
                                moneyAmount.TwentiesAmount * 20 + moneyAmount.FivesAmount * 5 +
                                moneyAmount.OnesAmount + moneyAmount.TensAmount * 10);
            return coinAmount + dollarAmount;
        }
        public static MoneyAmount? ReBalanceMoneyAmount(MoneyAmount moneyAmountDB, decimal targetAmount)
        {
            if (moneyAmountDB.TotalAmount <= targetAmount) return null;
            moneyAmountDB.HundredsAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.HundredsAmount, 100, targetAmount, moneyAmountDB);
          

            moneyAmountDB.FiftiesAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.FiftiesAmount, 50, targetAmount, moneyAmountDB);
         

            moneyAmountDB.TwentiesAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.TwentiesAmount, 20, targetAmount, moneyAmountDB);
           

            moneyAmountDB.TensAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.TensAmount, 10, targetAmount, moneyAmountDB);
        

            moneyAmountDB.FivesAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.FivesAmount, 5, targetAmount, moneyAmountDB);
         

            moneyAmountDB.OnesAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.OnesAmount, 1, targetAmount, moneyAmountDB);
           

            moneyAmountDB.DollarCoinAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.DollarCoinAmount, 1, targetAmount, moneyAmountDB);
      

            moneyAmountDB.HalfDollarAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.HalfDollarAmount, (decimal).5, targetAmount, moneyAmountDB);
            

            moneyAmountDB.QuartersAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.QuartersAmount, (decimal).25, targetAmount, moneyAmountDB);
           

            moneyAmountDB.DimesAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.DimesAmount, (decimal).1, targetAmount, moneyAmountDB);
          

            moneyAmountDB.NicklesAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.NicklesAmount, (decimal).05, targetAmount, moneyAmountDB);
          

            moneyAmountDB.PenniesAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.PenniesAmount, (decimal).01, targetAmount, moneyAmountDB);

            return moneyAmountDB.TotalAmount != targetAmount ? null : moneyAmountDB;
        }
        public static MoneyAmount? RunTransaction(MoneyAmount moneyAmountDB, MoneyAmountViewModel moneyAmountViewModel, decimal itemCost)
        {
            var realTotal = (decimal)moneyAmountDB.TotalAmount + itemCost;//money we are actually at
            //bool flag for if chnage was made
            var isChangeMade = false;

            moneyAmountDB = AddMoneyAmounts(moneyAmountDB, moneyAmountViewModel);//update money amount to what user has given us before we make change
            
            //creates a temp money amount to detect if change was made
            var moneyAmountTemp = CreateMoneyAmountViewModel(moneyAmountDB);

            var amountOfChangeToGiveBack = moneyAmountDB.TotalAmount - realTotal;

            if (amountOfChangeToGiveBack <= 0) return moneyAmountDB;

            moneyAmountDB.HundredsAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.HundredsAmount, 100, realTotal, moneyAmountDB);
            //if this is true change was made
            if (moneyAmountDB.HundredsAmount < moneyAmountTemp.HundredsAmount) isChangeMade = true;
            
            moneyAmountDB.FiftiesAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.FiftiesAmount, 50, realTotal, moneyAmountDB);
            if (moneyAmountDB.FiftiesAmount < moneyAmountTemp.FiftiesAmount) isChangeMade = true;
           
            moneyAmountDB.TwentiesAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.TwentiesAmount, 20, realTotal, moneyAmountDB);
            if (moneyAmountDB.TwentiesAmount < moneyAmountTemp.TwentiesAmount) isChangeMade = true;
           
            moneyAmountDB.TensAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.TensAmount, 10, realTotal, moneyAmountDB);
            if (moneyAmountDB.TensAmount < moneyAmountTemp.TensAmount) isChangeMade = true;
            
            moneyAmountDB.FivesAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.FivesAmount, 5, realTotal, moneyAmountDB);
            if (moneyAmountDB.FivesAmount < moneyAmountTemp.FivesAmount) isChangeMade = true;

            moneyAmountDB.OnesAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.OnesAmount, 1, realTotal, moneyAmountDB);
            if (moneyAmountDB.OnesAmount < moneyAmountTemp.OnesAmount) isChangeMade = true;

            moneyAmountDB.DollarCoinAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.DollarCoinAmount, 1, realTotal, moneyAmountDB);
            if (moneyAmountDB.DollarCoinAmount < moneyAmountTemp.DollarCoinAmount) isChangeMade = true;

            moneyAmountDB.HalfDollarAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.HalfDollarAmount, (decimal).5, realTotal, moneyAmountDB);
            if (moneyAmountDB.HalfDollarAmount < moneyAmountTemp.HalfDollarAmount) isChangeMade = true;

            moneyAmountDB.QuartersAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.QuartersAmount, (decimal).25, realTotal, moneyAmountDB);
            if (moneyAmountDB.QuartersAmount < moneyAmountTemp.QuartersAmount) isChangeMade = true;

            moneyAmountDB.DimesAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.DimesAmount, (decimal).1, realTotal, moneyAmountDB);
            if (moneyAmountDB.DimesAmount < moneyAmountTemp.DimesAmount) isChangeMade = true;

            moneyAmountDB.NicklesAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.NicklesAmount, (decimal).05, realTotal, moneyAmountDB);
            if (moneyAmountDB.NicklesAmount < moneyAmountTemp.NicklesAmount) isChangeMade = true;

            moneyAmountDB.PenniesAmount -=
                TransactionAmountsProcessor((int)moneyAmountDB.PenniesAmount, (decimal).01, realTotal, moneyAmountDB);
            if (moneyAmountDB.PenniesAmount < moneyAmountTemp.PenniesAmount) isChangeMade = true;

            return !isChangeMade ? null : moneyAmountDB;

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
            moneyAmountDB.TotalAmount = GetMoneyAmountsTotal(moneyAmountDB);
            return moneyAmountDB;
        }
        public static MoneyAmount AddMoneyAmounts(MoneyAmount moneyAmountDB, MoneyAmountViewModel moneyAmountViewModel)
        {
            moneyAmountDB.DollarCoinAmount += moneyAmountViewModel.DollarCoinAmount;
            moneyAmountDB.HalfDollarAmount += moneyAmountViewModel.HalfDollarAmount;
            moneyAmountDB.QuartersAmount += moneyAmountViewModel.QuartersAmount;
            moneyAmountDB.DimesAmount += moneyAmountViewModel.DimesAmount;
            moneyAmountDB.NicklesAmount += moneyAmountViewModel.NicklesAmount;
            moneyAmountDB.PenniesAmount += moneyAmountViewModel.PenniesAmount;
            moneyAmountDB.HundredsAmount += moneyAmountViewModel.HundredsAmount;
            moneyAmountDB.FiftiesAmount += moneyAmountViewModel.FiftiesAmount;
            moneyAmountDB.TwentiesAmount += moneyAmountViewModel.TwentiesAmount;
            moneyAmountDB.TensAmount += moneyAmountViewModel.TensAmount;
            moneyAmountDB.FivesAmount += moneyAmountViewModel.FivesAmount;
            moneyAmountDB.OnesAmount += moneyAmountViewModel.OnesAmount;
            moneyAmountDB.TotalAmount = GetMoneyAmountsTotal(moneyAmountDB);
            return moneyAmountDB;
        }
        //moneyAmountDb is altered money amount, MoneyAmountViewModel is original
        public static string GenerateTakeOutString(MoneyAmount moneyAmountDb,
            MoneyAmountViewModel moneyAmountViewModel)
        {
            string output = "Take Out ";
            if (moneyAmountViewModel.HundredsAmount - moneyAmountDb.HundredsAmount > 0)
                output = output + (moneyAmountViewModel.HundredsAmount - moneyAmountDb.HundredsAmount) + " hundreds, ";
            if (moneyAmountViewModel.FiftiesAmount - moneyAmountDb.FiftiesAmount > 0)
                output = output + (moneyAmountViewModel.FiftiesAmount - moneyAmountDb.FiftiesAmount) + " fifties, ";
            if (moneyAmountViewModel.TwentiesAmount - moneyAmountDb.TwentiesAmount > 0)
                output = output + (moneyAmountViewModel.TwentiesAmount - moneyAmountDb.TwentiesAmount) + " twenties, ";
            if (moneyAmountViewModel.TensAmount - moneyAmountDb.TensAmount > 0)
                output = output + (moneyAmountViewModel.TensAmount - moneyAmountDb.TensAmount) + " tens, ";
            if (moneyAmountViewModel.FivesAmount - moneyAmountDb.FivesAmount > 0)
                output = output + (moneyAmountViewModel.FivesAmount - moneyAmountDb.FivesAmount) + " fives, ";
            if (moneyAmountViewModel.OnesAmount - moneyAmountDb.OnesAmount > 0)
                output = output + (moneyAmountViewModel.OnesAmount - moneyAmountDb.OnesAmount) + " ones, ";
            if (moneyAmountViewModel.DollarCoinAmount - moneyAmountDb.DollarCoinAmount > 0)
                output = output + (moneyAmountViewModel.DollarCoinAmount - moneyAmountDb.DollarCoinAmount) + " dollar coins, ";
            if (moneyAmountViewModel.HalfDollarAmount - moneyAmountDb.HalfDollarAmount > 0)
                output = output + (moneyAmountViewModel.HalfDollarAmount - moneyAmountDb.HalfDollarAmount) + " half dollars, ";
            if (moneyAmountViewModel.QuartersAmount - moneyAmountDb.QuartersAmount > 0)
                output = output + (moneyAmountViewModel.QuartersAmount - moneyAmountDb.QuartersAmount) + " quarters, ";
            if (moneyAmountViewModel.DimesAmount - moneyAmountDb.DimesAmount > 0)
                output = output + (moneyAmountViewModel.DimesAmount - moneyAmountDb.DimesAmount) + " dimes, ";
            if (moneyAmountViewModel.NicklesAmount - moneyAmountDb.NicklesAmount > 0)
                output = output + (moneyAmountViewModel.NicklesAmount - moneyAmountDb.NicklesAmount) + " nickles, ";
            if (moneyAmountViewModel.PenniesAmount - moneyAmountDb.PenniesAmount > 0)
                output = output + (moneyAmountViewModel.PenniesAmount - moneyAmountDb.PenniesAmount) + " pennies, ";
            if (output == "Give back ")
            {
                output = "No change  ";
            }
            return output.Remove(output.Length-2);
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

        #endregion

    }
}

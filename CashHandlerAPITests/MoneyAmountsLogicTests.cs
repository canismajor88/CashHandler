using System.Runtime.InteropServices.ComTypes;
using CashHandlerAPI.CashHandlerLogic;
using CashHandlerAPI.Models;
using CashHandlerAPI.ViewModels;
using NUnit.Framework;

namespace CashHandlerAPITests
{
    public class MoneyAmountsLogicTests
    {

        [Test]
        public void TestValidPopulation()
        {
            var moneyAmount = new MoneyAmount
            {
                HundredsAmount = 2,
                FiftiesAmount = 2,
                TwentiesAmount = 2,
                TensAmount = 2,
                FivesAmount = 2,
                OnesAmount = 2,
                DollarCoinAmount = 2,
                HalfDollarAmount = 2,
                QuartersAmount = 2,
                DimesAmount = 2,
                NicklesAmount = 2,
                PenniesAmount = 2
            };
            var sutValue = MoneyAmountsLogic.GetMoneyAmountsTotal(moneyAmount);
            const decimal actualAmount = (decimal)375.82;
            Assert.AreEqual(sutValue,actualAmount);
        }

        [Test]
        public void ReBalanceTest()
        {
            var moneyAmounts = new MoneyAmount
            {
                DollarCoinAmount = 0,
                HalfDollarAmount = 0,
                QuartersAmount = 20,
                DimesAmount = 64,
                NicklesAmount = 40,
                PenniesAmount = 78,
                HundredsAmount = 1,
                FiftiesAmount = 1,
                TwentiesAmount = 0,
                TensAmount = 2,
                FivesAmount = 9,
                OnesAmount = 35,
                TotalAmount = (decimal)264.18

            };
            var moneyAmountAfterReBalance = new MoneyAmount
            {
                DollarCoinAmount = 0,
                HalfDollarAmount = 0,
                QuartersAmount = 20,
                DimesAmount = 63,
                NicklesAmount = 39,
                PenniesAmount = 75,
                HundredsAmount = 0,
                FiftiesAmount = 0,
                TwentiesAmount = 0,
                TensAmount = 1,
                FivesAmount = 9,
                OnesAmount = 31,
                TotalAmount = 100
            };
            moneyAmounts = MoneyAmountsLogic.ReBalanceMoneyAmount(moneyAmounts, 100);
            Assert.AreEqual(moneyAmounts.TotalAmount,moneyAmountAfterReBalance.TotalAmount);
            Assert.AreEqual(moneyAmounts.HundredsAmount, moneyAmountAfterReBalance.HundredsAmount);
            Assert.AreEqual(moneyAmounts.FiftiesAmount, moneyAmountAfterReBalance.FiftiesAmount);
            Assert.AreEqual(moneyAmounts.TwentiesAmount, moneyAmountAfterReBalance.TwentiesAmount);
            Assert.AreEqual(moneyAmounts.TensAmount, moneyAmountAfterReBalance.TensAmount);
            Assert.AreEqual(moneyAmounts.OnesAmount, moneyAmountAfterReBalance.OnesAmount);
            Assert.AreEqual(moneyAmounts.DollarCoinAmount, moneyAmountAfterReBalance.DollarCoinAmount);
            Assert.AreEqual(moneyAmounts.HalfDollarAmount, moneyAmountAfterReBalance.HalfDollarAmount);
            Assert.AreEqual(moneyAmounts.QuartersAmount, moneyAmountAfterReBalance.QuartersAmount);
            Assert.AreEqual(moneyAmounts.DimesAmount, moneyAmountAfterReBalance.DimesAmount);
            Assert.AreEqual(moneyAmounts.NicklesAmount, moneyAmountAfterReBalance.NicklesAmount);
            Assert.AreEqual(moneyAmounts.PenniesAmount, moneyAmountAfterReBalance.PenniesAmount);
        }


        [Test]
        public void ReBalanceTestTwo()
        {
            var moneyAmounts = new MoneyAmount
            {
                DollarCoinAmount = 36,
                HalfDollarAmount = 3,
                QuartersAmount = 20,
                DimesAmount = 64,
                NicklesAmount = 40,
                PenniesAmount = 78,
                HundredsAmount = 0,
                FiftiesAmount = 0,
                TwentiesAmount = 10,
                TensAmount = 2,
                FivesAmount = 9,
                OnesAmount = 3,
                TotalAmount = (decimal)319.68

            };
            var moneyAmountAfterReBalance = new MoneyAmount
            {
                DollarCoinAmount = 35,
                HalfDollarAmount = 2,
                QuartersAmount = 20,
                DimesAmount = 63,
                NicklesAmount = 39,
                PenniesAmount = 75,
                HundredsAmount = 0,
                FiftiesAmount = 0,
                TwentiesAmount = 0,
                TensAmount = 1,
                FivesAmount = 8,
                OnesAmount = 0,
                TotalAmount = 100
            };
            moneyAmounts = MoneyAmountsLogic.ReBalanceMoneyAmount(moneyAmounts, 100);
            Assert.AreEqual(moneyAmounts.TotalAmount, moneyAmountAfterReBalance.TotalAmount);
            Assert.AreEqual(moneyAmounts.HundredsAmount, moneyAmountAfterReBalance.HundredsAmount);
            Assert.AreEqual(moneyAmounts.FiftiesAmount, moneyAmountAfterReBalance.FiftiesAmount);
            Assert.AreEqual(moneyAmounts.TwentiesAmount, moneyAmountAfterReBalance.TwentiesAmount);
            Assert.AreEqual(moneyAmounts.TensAmount, moneyAmountAfterReBalance.TensAmount);
            Assert.AreEqual(moneyAmounts.OnesAmount, moneyAmountAfterReBalance.OnesAmount);
            Assert.AreEqual(moneyAmounts.DollarCoinAmount, moneyAmountAfterReBalance.DollarCoinAmount);
            Assert.AreEqual(moneyAmounts.HalfDollarAmount, moneyAmountAfterReBalance.HalfDollarAmount);
            Assert.AreEqual(moneyAmounts.QuartersAmount, moneyAmountAfterReBalance.QuartersAmount);
            Assert.AreEqual(moneyAmounts.DimesAmount, moneyAmountAfterReBalance.DimesAmount);
            Assert.AreEqual(moneyAmounts.NicklesAmount, moneyAmountAfterReBalance.NicklesAmount);
            Assert.AreEqual(moneyAmounts.PenniesAmount, moneyAmountAfterReBalance.PenniesAmount);
        }

        [Test]
        public void ReBalanceTestTargetLargerThanTotal()
        {
            var moneyAmounts = new MoneyAmount
            {
                DollarCoinAmount = 36,
                HalfDollarAmount = 3,
                QuartersAmount = 20,
                DimesAmount = 64,
                NicklesAmount = 40,
                PenniesAmount = 78,
                HundredsAmount = 0,
                FiftiesAmount = 0,
                TwentiesAmount = 10,
                TensAmount = 2,
                FivesAmount = 9,
                OnesAmount = 3,
                TotalAmount = (decimal)319.68

            };
            moneyAmounts = MoneyAmountsLogic.ReBalanceMoneyAmount(moneyAmounts, 320);
            Assert.AreEqual(-1,moneyAmounts.PenniesAmount);
        }

        [Test]
        public void ReBalanceTestCantMakeChange()
        {
            var moneyAmounts = new MoneyAmount
            {
                DollarCoinAmount = 36,
                HalfDollarAmount = 0,
                QuartersAmount = 0,
                DimesAmount = 0,
                NicklesAmount = 0,
                PenniesAmount = 0,
                HundredsAmount = 0,
                FiftiesAmount = 0,
                TwentiesAmount = 0,
                TensAmount =0,
                FivesAmount = 0,
                OnesAmount = 0,
                TotalAmount = (decimal)319.68

            };
            moneyAmounts = MoneyAmountsLogic.ReBalanceMoneyAmount(moneyAmounts, 100);
            Assert.AreEqual(-1, moneyAmounts.PenniesAmount);
        }

        [Test]
        public void RunTransactionTest()
        {
            var moneyAmounts = new MoneyAmount
            {
                DollarCoinAmount = 36,
                HalfDollarAmount = 3,
                QuartersAmount = 20,
                DimesAmount = 64,
                NicklesAmount = 40,
                PenniesAmount = 110,
                HundredsAmount = 0,
                FiftiesAmount = 0,
                TwentiesAmount = 10,
                TensAmount = 2,
                FivesAmount = 9,
                OnesAmount = 3,
                TotalAmount = (decimal)320

            };
            var moneyAmountFromUser = new MoneyAmountViewModel()
            {
                DollarCoinAmount = 0,
                HalfDollarAmount = 0,
                QuartersAmount = 0,
                DimesAmount = 0,
                NicklesAmount = 0,
                PenniesAmount = 0,
                HundredsAmount = 1,
                FiftiesAmount = 0,
                TwentiesAmount = 0,
                TensAmount = 0,
                FivesAmount = 0,
                OnesAmount = 0,
                TransactionAmount = 1
            };
            moneyAmounts = MoneyAmountsLogic.RunTransaction(moneyAmounts, moneyAmountFromUser,(decimal)moneyAmountFromUser.TransactionAmount);
            var moneyAmountsPostTrans = new MoneyAmount
            {
                DollarCoinAmount = 35,
                HalfDollarAmount = 3,
                QuartersAmount = 20,
                DimesAmount = 64,
                NicklesAmount = 40,
                PenniesAmount = 110,
                HundredsAmount = 1,
                FiftiesAmount = 0,
                TwentiesAmount = 6,
                TensAmount = 1,
                FivesAmount = 8,
                OnesAmount = 0,
                TotalAmount = (decimal)321
            };
            Assert.AreEqual(moneyAmounts.TotalAmount, moneyAmountsPostTrans.TotalAmount);
            Assert.AreEqual(moneyAmounts.HundredsAmount, moneyAmountsPostTrans.HundredsAmount);
            Assert.AreEqual(moneyAmounts.FiftiesAmount, moneyAmountsPostTrans.FiftiesAmount);
            Assert.AreEqual(moneyAmounts.TwentiesAmount, moneyAmountsPostTrans.TwentiesAmount);
            Assert.AreEqual(moneyAmounts.TensAmount, moneyAmountsPostTrans.TensAmount);
            Assert.AreEqual(moneyAmounts.OnesAmount, moneyAmountsPostTrans.OnesAmount);
            Assert.AreEqual(moneyAmounts.DollarCoinAmount, moneyAmountsPostTrans.DollarCoinAmount);
            Assert.AreEqual(moneyAmounts.HalfDollarAmount, moneyAmountsPostTrans.HalfDollarAmount);
            Assert.AreEqual(moneyAmounts.QuartersAmount, moneyAmountsPostTrans.QuartersAmount);
            Assert.AreEqual(moneyAmounts.DimesAmount, moneyAmountsPostTrans.DimesAmount);
            Assert.AreEqual(moneyAmounts.NicklesAmount, moneyAmountsPostTrans.NicklesAmount);
            Assert.AreEqual(moneyAmounts.PenniesAmount, moneyAmountsPostTrans.PenniesAmount);
        }
        [Test]
        public void RunTransactionTwo()
        {
            var moneyAmounts = new MoneyAmount
            {
                DollarCoinAmount = 99,
                HalfDollarAmount = 3,
                QuartersAmount = 20,
                DimesAmount = 64,
                NicklesAmount = 40,
                PenniesAmount = 110,
                HundredsAmount = 0,
                FiftiesAmount = 0,
                TwentiesAmount = 0,
                TensAmount = 0,
                FivesAmount = 0,
                OnesAmount = 0,
                TotalAmount = (decimal)115

            };
            var moneyAmountFromUser = new MoneyAmountViewModel()
            {
                DollarCoinAmount = 0,
                HalfDollarAmount = 0,
                QuartersAmount = 0,
                DimesAmount = 0,
                NicklesAmount = 0,
                PenniesAmount = 0,
                HundredsAmount = 0,
                FiftiesAmount = 0,
                TwentiesAmount = 0,
                TensAmount = 1,
                FivesAmount = 0,
                OnesAmount = 0,
                TransactionAmount = 1
            };
            moneyAmounts = MoneyAmountsLogic.RunTransaction(moneyAmounts, moneyAmountFromUser, (decimal)moneyAmountFromUser.TransactionAmount);
            var moneyAmountsPostTrans = new MoneyAmount
            {
                DollarCoinAmount = 90,
                HalfDollarAmount = 3,
                QuartersAmount = 20,
                DimesAmount = 64,
                NicklesAmount = 40,
                PenniesAmount = 110,
                HundredsAmount = 0,
                FiftiesAmount = 0,
                TwentiesAmount = 0,
                TensAmount = 1,
                FivesAmount = 0,
                OnesAmount = 0,
                TotalAmount = (decimal)116
            };
            Assert.AreEqual(moneyAmounts.TotalAmount, moneyAmountsPostTrans.TotalAmount);
            Assert.AreEqual(moneyAmounts.HundredsAmount, moneyAmountsPostTrans.HundredsAmount);
            Assert.AreEqual(moneyAmounts.FiftiesAmount, moneyAmountsPostTrans.FiftiesAmount);
            Assert.AreEqual(moneyAmounts.TwentiesAmount, moneyAmountsPostTrans.TwentiesAmount);
            Assert.AreEqual(moneyAmounts.TensAmount, moneyAmountsPostTrans.TensAmount);
            Assert.AreEqual(moneyAmounts.OnesAmount, moneyAmountsPostTrans.OnesAmount);
            Assert.AreEqual(moneyAmounts.DollarCoinAmount, moneyAmountsPostTrans.DollarCoinAmount);
            Assert.AreEqual(moneyAmounts.HalfDollarAmount, moneyAmountsPostTrans.HalfDollarAmount);
            Assert.AreEqual(moneyAmounts.QuartersAmount, moneyAmountsPostTrans.QuartersAmount);
            Assert.AreEqual(moneyAmounts.DimesAmount, moneyAmountsPostTrans.DimesAmount);
            Assert.AreEqual(moneyAmounts.NicklesAmount, moneyAmountsPostTrans.NicklesAmount);
            Assert.AreEqual(moneyAmounts.PenniesAmount, moneyAmountsPostTrans.PenniesAmount);
        }
        [Test]
        public void RunTransactionItemCostLargerThanMoneyAmountTotal()
        {
            var moneyAmounts = new MoneyAmount
            {
                DollarCoinAmount = 99,
                HalfDollarAmount = 3,
                QuartersAmount = 20,
                DimesAmount = 64,
                NicklesAmount = 40,
                PenniesAmount = 110,
                HundredsAmount = 0,
                FiftiesAmount = 0,
                TwentiesAmount = 0,
                TensAmount = 0,
                FivesAmount = 0,
                OnesAmount = 0,
                TotalAmount = (decimal)115

            };
            var moneyAmountFromUser = new MoneyAmountViewModel()
            {
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
                OnesAmount = 0,
                TransactionAmount = 116
            };
            moneyAmounts = MoneyAmountsLogic.RunTransaction(moneyAmounts, moneyAmountFromUser, (decimal)moneyAmountFromUser.TransactionAmount);

            Assert.AreEqual(-1, moneyAmounts.PenniesAmount);
        }

        [Test]
        public void RunTransactionCanNotMakeChange()
        {
            var moneyAmounts = new MoneyAmount
            {
                DollarCoinAmount = 0,
                HalfDollarAmount = 0,
                QuartersAmount = 0,
                DimesAmount = 0,
                NicklesAmount = 0,
                PenniesAmount = 0,
                HundredsAmount = 3,
                FiftiesAmount = 0,
                TwentiesAmount = 0,
                TensAmount = 0,
                FivesAmount = 0,
                OnesAmount = 0,
                TotalAmount = (decimal)300

            };
            var moneyAmountFromUser = new MoneyAmountViewModel()
            {
                DollarCoinAmount = 0,
                HalfDollarAmount = 0,
                QuartersAmount = 0,
                DimesAmount = 0,
                NicklesAmount = 0,
                PenniesAmount = 0,
                HundredsAmount = 1,
                FiftiesAmount = 0,
                TwentiesAmount = 0,
                TensAmount = 0,
                FivesAmount = 0,
                OnesAmount = 0,
                TransactionAmount = 1
            };
            moneyAmounts = MoneyAmountsLogic.RunTransaction(moneyAmounts, moneyAmountFromUser, (decimal)moneyAmountFromUser.TransactionAmount);

            Assert.AreEqual(-1, moneyAmounts.PenniesAmount);
        }
        [Test]
        public void GenerateTakeOutString()
        {
            var moneyAmounts = new MoneyAmount
            {
                DollarCoinAmount = 99,
                HalfDollarAmount = 3,
                QuartersAmount = 20,
                DimesAmount = 64,
                NicklesAmount = 40,
                PenniesAmount = 110,
                HundredsAmount = 0,
                FiftiesAmount = 0,
                TwentiesAmount = 0,
                TensAmount = 0,
                FivesAmount = 0,
                OnesAmount = 0,
                TotalAmount = (decimal)115

            };
            var moneyAmountFromUser = new MoneyAmountViewModel()
            {
                DollarCoinAmount = 0,
                HalfDollarAmount = 0,
                QuartersAmount = 0,
                DimesAmount = 0,
                NicklesAmount = 0,
                PenniesAmount = 0,
                HundredsAmount = 0,
                FiftiesAmount = 0,
                TwentiesAmount = 0,
                TensAmount = 1,
                FivesAmount = 0,
                OnesAmount = 0,
                TransactionAmount = 1
            };
            var moneyAmountOriginal = MoneyAmountsLogic.CreateMoneyAmountViewModel(moneyAmounts);
            moneyAmounts = MoneyAmountsLogic.RunTransaction(moneyAmounts, moneyAmountFromUser, (decimal)moneyAmountFromUser.TransactionAmount);
            var takeOutString = MoneyAmountsLogic.GenerateTakeOutString(moneyAmounts, moneyAmountOriginal);
            Assert.AreEqual(takeOutString, "Take Out 9 dollar coins");
        }

        [Test]
        public void GenerateTakeOutStringExactChange()
        {
            var moneyAmounts = new MoneyAmount
            {
                DollarCoinAmount = 99,
                HalfDollarAmount = 3,
                QuartersAmount = 20,
                DimesAmount = 64,
                NicklesAmount = 40,
                PenniesAmount = 110,
                HundredsAmount = 0,
                FiftiesAmount = 0,
                TwentiesAmount = 0,
                TensAmount = 0,
                FivesAmount = 0,
                OnesAmount = 0,
                TotalAmount = (decimal)115

            };
            var moneyAmountFromUser = new MoneyAmountViewModel()
            {
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
                OnesAmount = 1,
                TransactionAmount = 1
            };
            var moneyAmountOriginal = MoneyAmountsLogic.CreateMoneyAmountViewModel(moneyAmounts);
            moneyAmounts = MoneyAmountsLogic.RunTransaction(moneyAmounts, moneyAmountFromUser, (decimal)moneyAmountFromUser.TransactionAmount);
            var takeOutString = MoneyAmountsLogic.GenerateTakeOutString(moneyAmounts, moneyAmountOriginal);
            Assert.AreEqual(takeOutString, "No change");
        }
    }
}




using NUnit.Framework;

namespace MoneyCalculatorTests
{
    [TestFixture]
    public class MoneyAmountsTests
    {
        private readonly double _registerTotal = 1; //amount register is supposed to be at for drop

        [Test]
        public void TestMoneyAmountClear()
        {
            var moneyAmounts = TestHelper.BuildMoneyAmounts(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, _registerTotal);
            moneyAmounts.Clear();
            var expected = 0;

            Assert.AreEqual(expected, moneyAmounts.DollarCoinAmount);
            Assert.AreEqual(expected, moneyAmounts.HalfDollarAmount);
            Assert.AreEqual(expected, moneyAmounts.QuartersAmount);
            Assert.AreEqual(expected, moneyAmounts.DimesAmount);
            Assert.AreEqual(expected, moneyAmounts.NicklesAmount);
            Assert.AreEqual(expected, moneyAmounts.PenniesAmount);
            Assert.AreEqual(expected, moneyAmounts.HundredsAmount);
            Assert.AreEqual(expected, moneyAmounts.FiftiesAmount);
            Assert.AreEqual(expected, moneyAmounts.TwentiesAmount);
            Assert.AreEqual(expected, moneyAmounts.TensAmount);
            Assert.AreEqual(expected, moneyAmounts.FivesAmount);
            Assert.AreEqual(expected, moneyAmounts.OnesAmount);
            Assert.AreEqual(expected, moneyAmounts.TotalAmount);
        }
    }
}
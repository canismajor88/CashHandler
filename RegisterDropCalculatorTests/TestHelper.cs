using RegisterDropCalculator;

namespace MoneyCalculatorTests
{
    internal static class TestHelper
    {
        public static MoneyAmounts BuildMoneyAmounts(
            int numDollarCoins,
            int numHalfDollar,
            int numQuarter,
            int numDimes,
            int numNickles,
            int numPennies,
            int numHundreds,
            int numFifties,
            int numTwenties,
            int numTens,
            int numFives,
            int numOnes,
            double numTotal)
        {
            return new MoneyAmounts
            {
                DollarCoinAmount = numDollarCoins,
                HalfDollarAmount = numHalfDollar,
                QuartersAmount = numQuarter,
                DimesAmount = numDimes,
                NicklesAmount = numNickles,
                PenniesAmount = numPennies,
                HundredsAmount = numHundreds,
                FiftiesAmount = numFifties,
                TwentiesAmount = numTwenties,
                TensAmount = numTens,
                FivesAmount = numFives,
                OnesAmount = numOnes,
                TotalAmount = numTotal
            };
        }
    }
}

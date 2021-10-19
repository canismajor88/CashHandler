namespace RegisterDropCalculator
{
    public class MoneyAmounts
    {
        public const int TargetAmount = 100;

        public int DollarCoinAmount { get; set; }

        public int HalfDollarAmount { get; set; }

        public int QuartersAmount { get; set; }

        public int DimesAmount { get; set; }

        public int NicklesAmount { get; set; }

        public int PenniesAmount { get; set; }

        public int HundredsAmount { get; set; }

        public int FiftiesAmount { get; set; }

        public int TwentiesAmount { get; set; }

        public int TensAmount { get; set; }

        public int FivesAmount { get; set; }

        public int OnesAmount { get; set; }

        public double TotalAmount { get; set; }

        public void Clear()
        {
            DollarCoinAmount = 0;

            HalfDollarAmount = 0;

            QuartersAmount = 0;

            DimesAmount = 0;

            NicklesAmount = 0;

            PenniesAmount = 0;

            HundredsAmount = 0;

            FiftiesAmount = 0;

            TwentiesAmount = 0;

            TensAmount = 0;

            FivesAmount = 0;

            OnesAmount = 0;

            TotalAmount = 0;
        }
    }
}
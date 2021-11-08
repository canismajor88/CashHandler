namespace CashHandlerAPI.ViewModels
{
    public class AddTransactionResult
    {
        public string? GiveBackString { get; set; }
        public MoneyAmountViewModel? MoneyAmountViewModel { get; set; }
        public bool Success  { get; set; }
    }
}

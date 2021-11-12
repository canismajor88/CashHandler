namespace CashHandlerAPI.ViewModels
{
    public class AddTransactionResult
    {
        #region public properties

        public string? GiveBackString { get; set; }
        public MoneyAmountViewModel? MoneyAmountViewModel { get; set; }
        public bool Success  { get; set; }

        #endregion
        
    }
}

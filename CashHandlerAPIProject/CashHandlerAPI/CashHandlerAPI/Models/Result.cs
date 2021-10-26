namespace CashHandlerAPI.Models
{
    public record Result
    {
        #region public properties
        public bool Success { get; init; }
        public dynamic Payload { get; init; }

        public dynamic Status { get; init; }
        #endregion
    }
}

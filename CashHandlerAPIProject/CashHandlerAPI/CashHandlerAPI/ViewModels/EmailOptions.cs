namespace CashHandlerAPI.ViewModels
{
    public record EmailOptions
    {
        public string Host { get; set; }
        public string UserName_SenderEmail { get; set; }
        public string ApiKeySecret { get; set; }
        public int Port { get; set; }

    }
}

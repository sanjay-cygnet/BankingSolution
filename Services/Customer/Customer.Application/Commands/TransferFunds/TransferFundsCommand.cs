namespace Customer.Application.Commands.TransferFunds
{
    public sealed class TransferFundsCommand : IRequest<bool>
    {
        public int SourceAccountId { get; set; }
        public double Amount { get; set; }
        public string DestinationAccountNo { get; set; } = string.Empty;
    }
}
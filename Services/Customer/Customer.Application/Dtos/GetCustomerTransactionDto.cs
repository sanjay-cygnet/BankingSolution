namespace Customer.Application.Dtos;
public class GetCustomerTransactionDto
{
    public double Amount { get; set; }
    public short TransactionType { get; set; }
    public DateTime EffectiveDate { get; set; }
    public string TransactionNumber { get; set; } = string.Empty;
}

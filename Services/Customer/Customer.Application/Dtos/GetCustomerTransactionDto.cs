namespace Customer.Application.Dtos;
public class GetCustomerTransactionDto
{
    public double Amount { get; set; }
    public string TransactionType { get; set; }= string.Empty;
    public DateTime? ActualTransactionDate { get; set; }
    public string TransactionNumber { get; set; } = string.Empty;
}

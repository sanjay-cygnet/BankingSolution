namespace Shared.Enum
{
    public enum TransactionStatusEnum
    {
        Pending = 1,
        Success = 2,
        Hold = 3,
        Failed = 4
    }

    public enum AccountStatusEnum
    {
        Active = 1,
        Suspended = 2
    }

    public enum CurrencyEnum//this could be table
    {
        UsDollar = 1,
        SADollar = 2,
        Rupee = 3
    }
}

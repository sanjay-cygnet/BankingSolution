namespace Shared.Constants
{
    /// <summary>
    /// Use to store resource messages
    /// </summary>
    public static class CustomerServiceConstants
    {
        public static class Messages
        {
            public const string BalanceGreaterThanZero = "Balance should greated than zero";
            public const string NotSufficientBalance = "Not sufficient Balance";
            public const string InvalidAccount = "Invalid Account";
            public const string NoTransactionsFound = "No transactions found";
            public const string AcconutBlockedOrSuspended = "Account is blocked or suspended";
            public const string NoConnectionStringProvided = "No connection string found for CustomerDbContext";
            public const string MaximumTransactionAmounReached = "Maximum transfer amount limit reached";
        }


        /// <summary>
        /// following rules are store temporarily in constants, it should wither came from config or from Db/Cache
        /// </summary>
        public static class Rules
        {
            public const double MinimumBalanceAmount = 2000;
            public const double TransferAmountLimitPerDay = 50000;
        }
    }
}

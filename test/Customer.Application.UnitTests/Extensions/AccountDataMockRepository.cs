namespace Customer.Application.UnitTests.Extensions
{

    /// <summary>
    /// AccountDataMockRepository will provide default/static data required in Unit test cases
    /// </summary>
    internal static class AccountDataMockRepository
    {
        public static List<Account> Accounts = new List<Account>()
        {
           new Account()
           {
               AccountNumber="12345",
               Id=1,
               Status=1,
               Balance=500000,
               BankBranchId=1,
               CustomerId=1,
               IsActive=true,
               Type=1,
           },
              new Account()
           {
               AccountNumber="3456",
               Id=2,
               Status=1,
               Balance=700000,
               BankBranchId=1,
               CustomerId=1,
               IsActive=true,
               Type=1,
           }
        };

        /// <summary>
        ///Just Adding valid transfer objects to 
        /// </summary>
        //static AccountDataMockRepository()
        //{
        //    Accounts[0].TransferFund(Accounts[0], 200, "xyz");
        //    Accounts[0].TransferFund(Accounts[0], 300, "xyz1");
        //    Accounts[1].TransferFund(Accounts[0], 5688, "xyz");
        //    Accounts[1].TransferFund(Accounts[0], 30550, "xyz1");
        //}

        public static List<Transaction> Transactions = new List<Transaction>()
        {
            new Transaction(Accounts[0],200,2),
             new Transaction(Accounts[0],400,1),
               new Transaction(Accounts[1],400,1)
        };


    }
}

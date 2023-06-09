﻿namespace Customer.Application.UnitTests.Extensions;

/// <summary>
/// AccountDataMockRepository will provide default/static data required in Unit test cases
/// </summary>
internal static class AccountDataMockRepository
{
    public static List<Account> Accounts = new List<Account>();
    public static List<Transaction> Transactions = new List<Transaction>();

    static AccountDataMockRepository()
    {
        SetAccountData();
    }

    public static void SetAccountData()
    {
        Accounts = new List<Account>()
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
    }

    public static void SetTransactionData()
    {
        Transactions = new List<Transaction>()
                {
                    new Transaction(Accounts[1],200,Shared.Enum.TransactionTypeEnum.Debit),
                     new Transaction(Accounts[0],400,Shared.Enum.TransactionTypeEnum.Debit),
                       new Transaction(Accounts[1],400,Shared.Enum.TransactionTypeEnum.Debit)
                };
    }


}

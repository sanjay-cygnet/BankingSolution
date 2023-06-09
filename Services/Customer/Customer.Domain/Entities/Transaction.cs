﻿namespace Customer.Domain.Entities;

using BuildingBlocks.Shared.DomainObjects;
using global::Customer.Domain.Exceptions;
using Shared.Enum;

public class Transaction : Entity
{
    #region Members
    public long AccountId { get; set; }
    public double Amount { get; private set; } = 0;
    public short TransactionType { get; set; }//1-debit,2-credit
    public DateTime EffectiveDate { get; set; }
    public DateTime? ActualTransactionDate { get; set; }
    public string TransactionNumber { get; private set; } = string.Empty;
    public short Status { get; set; }//1-Pending, 2-Success, 3-Hold,4-Failed
    public Account Account { get; set; }
    #endregion

    #region Ctor
    //Prevent from creating transaction empty object 
    protected Transaction()
    {
        Account = new Account();
    }
    public Transaction(
       Account account,
        double amount,
        TransactionTypeEnum transactionType)
    {
        TransactionType = transactionType > 0 && (short)transactionType <= 2 ? (short)transactionType : throw new CustomerDomainException(nameof(amount));///1 for debit, 2 for credit
        Amount = amount >= 0 ? amount : throw new CustomerDomainException(nameof(amount));
        EffectiveDate = DateTime.UtcNow;
        Account = account;
        Status = 1;//Pending by default status
        TransactionNumber = Guid.NewGuid().ToString();///We can use some random generator instead
    }
    #endregion
}

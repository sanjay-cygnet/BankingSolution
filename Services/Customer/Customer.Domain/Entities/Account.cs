namespace Customer.Domain.Entities;

using BuildingBlocks.Shared.DomainObjects;
using BuildingBlocks.Shared.Extensions;
using BuildingBlocks.Shared.Model;
using CAARepositoryLibrary.Entities;
using global::Customer.Domain.DomainEvents;
using Shared.Constants;
using Shared.Enum;
using System.Net;

public class Account : Entity, IBaseEntity, IAggregateRoot
{
    #region Members
    public new long Id { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public int BankBranchId { get; set; }
    public int CustomerId { get; set; }
    public DateTime OpeningDate { get; set; }
    public double Balance { get; set; }
    public short Type { get; set; }
    public short Status { get; set; }///1-Active, 2-Suspended
    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; }
    public string CreatedById { get; set; } = String.Empty;
    public string? LastModifiedById { get; set; }//Allows nullable in database
    public DateTime? LastModifiedDate { get; set; }
    public bool IsDeleted { get; set; }
    public BankBranch BankBranch { get; private set; }
    public Customer Customer { get; private set; }
    public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();
    private List<Transaction> _transactions { get; set; }
    #endregion

    #region Ctor
    public Account()
    {
        BankBranch = new BankBranch();
        Customer = new Customer();
        _transactions = new List<Transaction>();
    }
    #endregion

    #region Method(s)
    public ApiResponse<bool> TransferFund(Account account, double amountToTransfer, string destinationAccountNo)//Transfer is only valid through account
    {
        string errorMessage = string.Empty;

        if (account.Status != 1)//If account is not active
        {
            errorMessage = CustomerServiceConstants.Messages.AcconutBlockedOrSuspended;
        }
        else if (account.Balance <= 0)//No balance
        {
            errorMessage = CustomerServiceConstants.Messages.NotSufficientBalance;
        }
        else if (account.Balance < amountToTransfer)//not sufficient balance
        {
            errorMessage = CustomerServiceConstants.Messages.NotSufficientBalance;
        }
        else if (ExceedsTodaysTransferLimit())//check todays' limit
        {
            errorMessage = CustomerServiceConstants.Messages.MaximumTransactionAmounReached;
        }
        else
        {
            //TODO : Actual code to transfer amount thorugh payment gateway,

            Transaction transactionDetails = new Transaction(account, amountToTransfer, TransactionTypeEnum.Debit);

            //Following code should work after actual fund transfer through payment gateway
            account.Balance = account.Balance - amountToTransfer;
            transactionDetails.ActualTransactionDate = DateTime.UtcNow;
            transactionDetails.Status = (short)TransactionStatusEnum.Success;
            _transactions.Add(transactionDetails);

            if (account.Balance <= CustomerServiceConstants.BankRules.MinimumBalanceAmount)///Check for minimum balance
            {
                AddDomainEvent(new AccountAtMinimumBalanceDomainEvent(account));
            }
        }

        return new ApiResponse<bool>()
        {
            Success = errorMessage.IsNull(),
            StatusCode = errorMessage.IsNull() ? HttpStatusCode.OK.ToInt() : HttpStatusCode.BadRequest.ToInt(),
            Error = errorMessage.IsNull() ? null : new ErrorResponse(errorMessage)
        };
    }

    private bool ExceedsTodaysTransferLimit()
    {
        //Check rule CustomerServiceConstants.Rules.TransferAmountLimitPerDay
        return false;
    }
    #endregion
}
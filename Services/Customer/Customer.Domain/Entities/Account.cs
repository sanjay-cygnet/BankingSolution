using BuildingBlocks.Shared.DomainObjects;
using CAARepositoryLibrary.Entities;
using Customer.Domain.DomainEvents;
using Customer.Domain.Exceptions;
using Shared.Constants;
using Shared.Enum;

namespace Customer.Domain.Entities
{
    public class Account : Entity, IBaseEntity, IAggregateRoot
    {
        #region Members
        public long Id { get; set; }
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
        public void TransferFund(Account account, double amountToTransfer, string destinationAccountNo)//Transfer is only valid through account
        {
            if (account.Status != 1)
            {
                throw new CustomerDomainException(CustomerServiceConstants.Messages.AcconutBlockedOrSuspended);
            }

            if (account.Balance <= 0)//No balance
            {
                throw new CustomerDomainException(CustomerServiceConstants.Messages.NotSufficientBalance);
            }

            if (account.Balance < amountToTransfer)//not sufficient balance
            {
                throw new CustomerDomainException(CustomerServiceConstants.Messages.NotSufficientBalance);
            }

            if (ExceedsTodaysTransferLimit())
            {
                throw new CustomerDomainException(CustomerServiceConstants.Messages.MaximumTransactionAmounReached);
            }

            if (true)
            {
                //TODO : Actual code to transfer amount thorugh payment gateway,
                Transaction transactionDetails = new Transaction(account, amountToTransfer, transactionType: 1);//transactionType: 1 for debit

                //Following code should work after actual fund transfer through payment gateway
                account.Balance = account.Balance - amountToTransfer;
                transactionDetails.ActualTransactionDate = DateTime.UtcNow;
                transactionDetails.Status = (short)TransactionStatusEnum.Success;
                _transactions.Add(transactionDetails);

                if (account.Balance <= CustomerServiceConstants.Rules.MinimumBalanceAmount)///Check for minimum balance
                {
                    AddDomainEvent(new AccountBalanceReachedMinimumBalanceDomainEvent(account));
                }
            }
        }

        private bool ExceedsTodaysTransferLimit()
        {
            //Check rule CustomerServiceConstants.Rules.TransferAmountLimitPerDay
            return false;
        }
        #endregion
    }
}
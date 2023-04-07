using BuildingBlocks.EventBus.EventPublisherModel;
using BuildingBlocks.EventBus.QueuePublisher;
using BuildingBlocks.Repository.Service;
using Customer.Domain.Entities;
using Customer.Domain.Exceptions;
using Shared.Constants;

namespace Customer.Application.Commands.TransferFunds
{
    internal sealed class TransferFundsCommandHandler : IRequestHandler<TransferFundsCommand, bool>
    {
        #region Members
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailQueuePublisher _emailQueuePublisher;
        #endregion

        #region Ctor
        public TransferFundsCommandHandler(
            IEmailQueuePublisher emailQueuePublisher,
            IUnitOfWork unitOfWork)
        {
            _emailQueuePublisher = emailQueuePublisher;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Method(s)
        public async Task<bool> Handle(TransferFundsCommand request, CancellationToken cancellationToken)
        {
            var account = await _unitOfWork.GetRepositoryAsync<Account>().FirstOrDefaultAsync(f => f.Id == request.SourceAccountId);

            if (account == null || account.Id <= 0)
                throw new CustomerDomainException(CustomerServiceConstants.Messages.InvalidAccount);

            account.TransferFund(account, request.Amount, request.DestinationAccountNo);
            _unitOfWork.SaveChanges();

            ///Here publish message to email queue to send transfer detail to customer
            await _emailQueuePublisher.Publish(new EmailPublisherModel() { TemplateId = 1, Subject = $"Sending Mail to customer to inform about amount trnasfer of {request.Amount}" });
            return true;
        }
        #endregion


    }
}

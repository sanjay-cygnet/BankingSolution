using BuildingBlocks.EventBus.QueuePublisher;
using Customer.Application.Commands.TransferFunds;
using Customer.Domain.Exceptions;
using Moq;

namespace Customer.Application.UnitTests.Commands
{
    public class TransferFundsCommandHandlerTest
    {
        [Theory]
        [InlineData(true, false, false, false)]
        [InlineData(false, true, false, false)]
        [InlineData(false, false, true, false)]
        [InlineData(false, false, false, true)]
        public async Task Test(
            bool Fail_If_InvalidAccount,
            bool Fail_If_AccountBlocked,
            bool Fail_If_InSufficientBalance,
            bool Success_If_AllValid
            )
        {

            #region Arrange
            var mockUnitOfWork = UnitOfWorkExtensions.GetMockUnitOfWork();
            var mockEmailQueuePublisherl = new Mock<IEmailQueuePublisher>();
            var mockAccountRepository = new Mock<IRepositoryAsync<Account>>();

            Transaction transferAccountDetail = AccountDataMockRepository.Transactions.FirstOrDefault();
            transferAccountDetail.Account.Id = 1;
            if (Fail_If_InvalidAccount)
                transferAccountDetail.Account.Id = 0;

            if (Fail_If_AccountBlocked)
                transferAccountDetail.Account.Status = 2;

            if (Fail_If_InSufficientBalance)
                transferAccountDetail.Account.Balance = 0;

            mockAccountRepository.FirstOrDefaultAsync<Account>(transferAccountDetail.Account);
            mockUnitOfWork.SetupRepositoryInUnitOfWork<Account>(mockAccountRepository);

            var handler = new TransferFundsCommandHandler(
                mockEmailQueuePublisherl.Object,
                mockUnitOfWork.Object);
            #endregion

            #region Act
            bool result = false;
            try//As of now handlers are throwing error on invalid operations
            {
                result = await handler.Handle(new TransferFundsCommand()
                {
                    Amount = 200,
                    DestinationAccountNo = "ax",
                    SourceAccountId = 1
                }, new CancellationToken());
            }
            catch (CustomerDomainException)//only caching domain exceptions
            {
                result = false;
            }
            #endregion

            #region Assert
            if (Fail_If_InvalidAccount)
                Assert.True(!result);

            if (Fail_If_AccountBlocked)
                Assert.True(!result);

            if (Fail_If_InSufficientBalance)
                Assert.True(!result);

            if (Success_If_AllValid)
                Assert.True(result);
            #endregion
        }


    }
}

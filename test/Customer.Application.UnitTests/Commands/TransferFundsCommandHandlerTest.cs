
namespace Customer.Application.UnitTests.Commands;

using BuildingBlocks.EventBus.QueuePublisher;
using Customer.Application.Commands.TransferFunds;
using Moq;

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

        AccountDataMockRepository.SetAccountData();//Set static data
        AccountDataMockRepository.SetTransactionData();//Set static data

        //Setup repository data
        Transaction transferAccountDetail = AccountDataMockRepository.Transactions.FirstOrDefault();
        transferAccountDetail.Account.Id = 1;
        if (Fail_If_InvalidAccount)
            transferAccountDetail.Account = null;

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
        var result = await handler.Handle(new TransferFundsCommand()
        {
            Amount = 200,
            DestinationAccountNo = "ax",
            SourceAccountId = 1
        }, new CancellationToken());
        #endregion

        #region Assert
        if (Fail_If_InvalidAccount)
            Assert.False(result.Success);

        if (Fail_If_AccountBlocked)
            Assert.False(result.Success);

        if (Fail_If_InSufficientBalance)
            Assert.False(result.Success);

        if (Success_If_AllValid)
            Assert.True(result.Success);
        #endregion
    }


}

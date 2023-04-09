namespace Customer.Application.UnitTests.Queries;
public class GetCustomerBalanceQueryHandlerTest
{
    [Fact]
    public async Task Success_If_Account_IsValid()
    {
        #region Arrange
        var mockUnitOfWork = UnitOfWorkExtensions.GetMockUnitOfWork();
        var mockAcconutRepository = new Account().GetMockRepository();

        //Setup repository mock data
        AccountDataMockRepository.SetAccountData();//Set static data
        AccountDataMockRepository.SetTransactionData();//Set static data

        mockUnitOfWork.SetupRepositoryInUnitOfWork<Account>(mockAcconutRepository);

        mockAcconutRepository.FirstOrDefaultAsync<Account>(AccountDataMockRepository.Accounts.FirstOrDefault());

        var handler = new GetCustomerBalanceQueryHandler(mockUnitOfWork.Object);
        #endregion

        #region Act
        var result = await handler.Handle(new GetCustomerBalanceQuery()
        {
            AccountId = 1
        }, new CancellationToken());
        #endregion

        #region Assert
        Assert.True(result.Success);
        #endregion
    }

    [Fact]
    public async Task Fail_If_Account_Is_InValid()
    {
        #region Arrange
        var mockUnitOfWork = UnitOfWorkExtensions.GetMockUnitOfWork();
        var mockAcconutRepository = new Account().GetMockRepository();

        mockUnitOfWork.SetupRepositoryInUnitOfWork<Account>(mockAcconutRepository);

        mockAcconutRepository.FirstOrDefaultAsync<Account>(null);

        var handler = new GetCustomerBalanceQueryHandler(mockUnitOfWork.Object);
        #endregion

        #region Act
        var result = await handler.Handle(new GetCustomerBalanceQuery()
        {
            AccountId = 0
        }, new CancellationToken());

        #endregion

        #region Assert
        Assert.False(result.Success);
        #endregion
    }
}

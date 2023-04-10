namespace Customer.Application.UnitTests.Queries;

using Customer.Application.DataMapper;
using Moq;
#nullable disable
public class GetCustomerTransactionQueryHandlerTest
{

    [Fact]
    public async Task Success_If_Account_IsValid()
    {
        #region Arrange
        var mockUnitOfWork = UnitOfWorkExtensions.GetMockUnitOfWork();

        var mockTransactionRepository = new Mock<IRepositoryAsync<Transaction>>();
        //Setup repository mock data
        AccountDataMockRepository.SetAccountData();//Set static data
        AccountDataMockRepository.SetTransactionData();//Set static data

        mockTransactionRepository.Query<Transaction>(AccountDataMockRepository.Transactions);
        mockUnitOfWork.SetupRepositoryInUnitOfWork<Transaction>(mockTransactionRepository);

        var mockMapper = CommonExtensions.SetupMapper(new DataMapperProfile());

        var handler = new GetCustomerTransactionQueryHandler(mockUnitOfWork.Object, mockMapper);
        #endregion

        #region Act
        var result = await handler.Handle(new GetCustomerTransactionQuery()
        {
            AccountId = 1
        }, new CancellationToken());
        #endregion

        #region Assert
        Assert.True(result.Success);
        #endregion
    }

    [Fact]
    public async Task Fail_If_Account_IsInvalid()
    {
        #region Arrange
        var mockUnitOfWork = UnitOfWorkExtensions.GetMockUnitOfWork();
        var mockTransactionRepository = new Mock<IRepositoryAsync<Transaction>>();

        //Setup repository mock data
        AccountDataMockRepository.SetAccountData();//Set static data
        AccountDataMockRepository.SetTransactionData();//Set static data

        mockUnitOfWork.SetupRepositoryInUnitOfWork<Transaction>(mockTransactionRepository);
        mockTransactionRepository.Query<Transaction>(new List<Transaction>());
        var mockMapper = CommonExtensions.SetupMapper(new DataMapperProfile());

        var handler = new GetCustomerTransactionQueryHandler(mockUnitOfWork.Object, mockMapper);
        #endregion

        #region Act
        var result = await handler.Handle(new GetCustomerTransactionQuery()
        {
            AccountId = 1
        }, new CancellationToken());

        #endregion

        #region Assert
        Assert.False(result.Success);
        #endregion
    }
}


using Customer.Application.DataMapper;
using Customer.Application.Dtos;
using Customer.Domain.Exceptions;
using Moq;

namespace Customer.Application.UnitTests.Queries
{
    public class GetCustomerTransactionQueryHandlerTest
    {
        private IMapper _mapper;

        [Fact]
        public async Task Success_If_Account_IsValid()
        {
            #region Arrange
            var mockUnitOfWork = UnitOfWorkExtensions.GetMockUnitOfWork();

            var mockTransactionRepository = new Mock<IRepositoryAsync<Transaction>>();
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
            Assert.NotNull(result);
            #endregion
        }


        [Fact]
        public async Task Fail_If_Account_IsInvalid()
        {
            #region Arrange
            var mockUnitOfWork = UnitOfWorkExtensions.GetMockUnitOfWork();
            var mockTransactionRepository = new Mock<IRepositoryAsync<Transaction>>();

            mockUnitOfWork.SetupRepositoryInUnitOfWork<Transaction>(mockTransactionRepository);
            mockTransactionRepository.Query<Transaction>(new List<Transaction>());
            var mockMapper = CommonExtensions.SetupMapper(new DataMapperProfile());

            var handler = new GetCustomerTransactionQueryHandler(mockUnitOfWork.Object, mockMapper);
            #endregion

            #region Act
            List<GetCustomerTransactionDto> result = new List<GetCustomerTransactionDto>();
            try//As of now handlers are throwing error on invalid operations
            {
                result = await handler.Handle(new GetCustomerTransactionQuery()
                {
                    AccountId = 1
                }, new CancellationToken());
            }
            catch (CustomerDomainException)//only caching domain exceptions
            {
                result = null;
            }

            #endregion

            #region Assert
            Assert.Null(result);
            #endregion
        }
    }

}

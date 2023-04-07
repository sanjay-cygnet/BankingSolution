using BuildingBlocks.Repository.Service;
using Moq;

namespace BuildingBlocks.UnitTest.Extensions
{
    public static class UnitOfWorkExtensions
    {
        public static Mock<IUnitOfWork> GetMockUnitOfWork()
        {
            return new Mock<IUnitOfWork>();
        }

        public static void SetupRepositoryInUnitOfWork<TRepo>(this Mock<IUnitOfWork> unitOfWorkMock, Mock<IRepositoryAsync<TRepo>> output)
         where TRepo : class
        {
            unitOfWorkMock.Setup(uw => uw.GetRepositoryAsync<TRepo>()).Returns(output.Object);
        }
    }
}

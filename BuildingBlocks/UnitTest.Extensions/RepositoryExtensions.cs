namespace BuildingBlocks.UnitTest.Extensions;
using BuildingBlocks.Repository.Service;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;

public static class  RepositoryExtensions
{

    /// <summary>
    /// Gets the mock repository object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="taget">The taget.</param>
    /// <returns></returns>
    public static Mock<IRepositoryAsync<T>> GetMockRepository<T>(this T taget) where T : class
    {
        return new Mock<IRepositoryAsync<T>>();
    }

    /// <summary>
    /// Set FirstOrDefaultAsync method for generic repository
    /// </summary>
    /// <typeparam name="TRepo">The type of the repo.</typeparam>
    /// <param name="mockRepository">The mock repository.</param>
    /// <param name="output">The output.</param>
    public static void FirstOrDefaultAsync<TRepo>(this Mock<IRepositoryAsync<TRepo>> mockRepository, TRepo output) where TRepo : class
    {
        mockRepository.Setup(s => s.FirstOrDefaultAsync(
            It.IsAny<Expression<Func<TRepo, bool>>?>(),
            It.IsAny<Func<IQueryable<TRepo>, IOrderedQueryable<TRepo>>?>(),
            It.IsAny<Func<IQueryable<TRepo>, IIncludableQueryable<TRepo, object>>?>(), It.IsAny<bool>())).ReturnsAsync(output);
    }

    /// <summary>
    /// Set Query setup for generic repository
    /// </summary>
    /// <typeparam name="TRepo">The type of the repo.</typeparam>
    /// <param name="mockRepository">The mock repository.</param>
    /// <param name="output">The output.</param>
    public static void Query<TRepo>(this Mock<IRepositoryAsync<TRepo>> mockRepository, List<TRepo> output) where TRepo : class
    {
        mockRepository.Setup(s => s.Query(
            It.IsAny<Expression<Func<TRepo, bool>>?>(),
            It.IsAny<Func<IQueryable<TRepo>,
                    IIncludableQueryable<TRepo, object>>?>(), It.IsAny<bool>(), It.IsAny<string>())).Returns(output.AsQueryable());
    }
}
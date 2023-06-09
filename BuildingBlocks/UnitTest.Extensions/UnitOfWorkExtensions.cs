﻿namespace BuildingBlocks.UnitTest.Extensions;

using BuildingBlocks.Repository.Service;
using Microsoft.EntityFrameworkCore;
using Moq;

public static class UnitOfWorkExtensions
{
    public static Mock<IUnitOfWork> GetMockUnitOfWork()
    {
        return new Mock<IUnitOfWork>();
    }

    public static Mock<IUnitOfWork<TContenxt>> GetMockUnitOfWork<TContenxt>() where TContenxt : DbContext
    {
        return new Mock<IUnitOfWork<TContenxt>>();
    }

    public static void SetupRepositoryInUnitOfWork<TRepo>(this Mock<IUnitOfWork> unitOfWorkMock, Mock<IRepositoryAsync<TRepo>> output)
     where TRepo : class
    {
        unitOfWorkMock.Setup(uw => uw.GetRepositoryAsync<TRepo>()).Returns(output.Object);
    }
}

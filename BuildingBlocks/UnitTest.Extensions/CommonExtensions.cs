namespace BuildingBlocks.UnitTest.Extensions;
using AutoMapper;

public static class CommonExtensions
{
    public static IMapper SetupMapper(Profile dataProfile)
    {
        return new MapperConfiguration(cfg => { cfg.AddProfile(dataProfile); }).CreateMapper();
    }
}

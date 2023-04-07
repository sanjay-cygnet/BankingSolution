using AutoMapper;

namespace BuildingBlocks.UnitTest.Extensions
{
    public static class CommonExtensions
    {
        public static IMapper SetupMapper(Profile dataProfile)
        {
            return new MapperConfiguration(cfg => { cfg.AddProfile(dataProfile); }).CreateMapper();
        }
    }
}

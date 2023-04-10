namespace BuildingBlocks.Shared.Extensions;

using System.Net;

public static class HttpExtensions
{
    public static int ToInt(this HttpStatusCode httpStatus)
    {
        return (int)httpStatus;
    }
}
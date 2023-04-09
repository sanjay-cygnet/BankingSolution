using System.Net;

namespace Shared.Extensions
{
    public static class HttpExtensions
    {
        public static int ToInt(this HttpStatusCode httpStatus)
        {
            return (int)httpStatus;
        }
    }
}
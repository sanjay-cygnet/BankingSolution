namespace BuildingBlocks.Shared.Constants
{
    /// <summary>
    /// GlobalConstants : that will be used to set global values that could used accross entire application.
    /// </summary>
    public static class GlobalConstants
    {
        public const string Route = "[Controller]";
    }

    public static class CustomerServiceAssemblies
    {
        public const string CustomerApi = "Customer.Api";
        public const string CustomerApplication = "Customer.Application";
        public const string CustomerDomain = "Customer.Domain";
        public const string CustomerInfrastructure = "Customer.Infrastructure";
    }

    public static class Validations
    {
        public const string RequestCouldNotProcessed = "Request could not be processed!";
    }
}

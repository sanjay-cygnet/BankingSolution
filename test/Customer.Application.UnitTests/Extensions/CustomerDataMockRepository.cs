using Customers = Customer.Domain.Entities.Customer;
namespace Customer.Application.UnitTests.Extensions
{
    /// <summary>
    /// CustomerDataMockRepository will provide default/static data required in Unit test cases
    /// </summary>
    internal static class CustomerDataMockRepository
    {
        internal static List<Domain.Entities.Customer> Customers = new List<Customers>()
        {
            new Customers()
            {
                Id=1,
                City="New York",
                Address="LA",
                Dob= DateTime.Parse("1/1/2000"),
                FirstName="Alex",
                LastName="George"
            },
            new Customers()
            {
                Id=1,
                City="New York",
                Address="LA",
                Dob= DateTime.Parse("1/1/1992"),
                FirstName="Minaxi",
                LastName="George"
            }
        };

    }
}

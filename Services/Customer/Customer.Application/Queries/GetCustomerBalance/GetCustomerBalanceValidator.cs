using FluentValidation;

namespace Customer.Application.Queries
{
    internal sealed class GetCustomerBalanceValidator : AbstractValidator<GetCustomerBalanceQuery>
    {
        public GetCustomerBalanceValidator()
        {
            RuleFor(r => r.AccountId).NotEmpty().NotNull();
        }
    }
}

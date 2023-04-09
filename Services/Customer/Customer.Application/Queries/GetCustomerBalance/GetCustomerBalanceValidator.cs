namespace Customer.Application.Queries;

using FluentValidation;

public sealed class GetCustomerBalanceValidator : AbstractValidator<GetCustomerBalanceQuery>
{
    public GetCustomerBalanceValidator()
    {
        RuleFor(r => r.AccountId).GreaterThan(0).NotEmpty().NotNull();
    }
}

namespace Customer.Application.Queries;

using FluentValidation;

public sealed class GetCustomerTransactionValidator : AbstractValidator<GetCustomerTransactionQuery>
{
    public GetCustomerTransactionValidator()
    {
        RuleFor(r => r.AccountId).GreaterThan(0).NotEmpty().NotNull();
        RuleFor(r => r.FromDate).NotEmpty().NotNull();
        RuleFor(r => r.ToDate).NotEmpty().NotNull();
        RuleFor(r => r.FromDate.Date).LessThanOrEqualTo(g => g.ToDate.Date);
    }
}
using FluentValidation;

namespace Customer.Application.Queries
{
    internal sealed class GetCustomerTransactionValidator : AbstractValidator<GetCustomerTransactionQuery>
    {
        public GetCustomerTransactionValidator()
        {
            RuleFor(r => r.AccountId).NotEmpty().NotNull();
            RuleFor(r => r.FromDate).NotEmpty().NotNull();
            RuleFor(r => r.ToDate).NotEmpty().NotNull();
            RuleFor(r => r.FromDate.Date).LessThanOrEqualTo(g => g.ToDate.Date);
        }
    }
}
using FluentValidation;

namespace Customer.Application.Commands.TransferFunds
{
    internal sealed class TransferFundsCommandValidator : AbstractValidator<TransferFundsCommand>
    {
        public TransferFundsCommandValidator()
        {
            RuleFor(r => r.Amount).GreaterThan(0);
            RuleFor(r => r.DestinationAccountNo).NotEmpty().NotNull();
            RuleFor(r => r.SourceAccountId).NotEmpty().NotNull();
        }
    }
}

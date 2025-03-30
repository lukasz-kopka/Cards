using FluentValidation;
using Cards.Application.Queries.Actions;

namespace Cards.API.Validators;

public sealed class GetCardActionsValidator : AbstractValidator<GetCardActions>
{
    public GetCardActionsValidator()
    {
        RuleFor(req => req.UserId)
            .NotEmpty();
        RuleFor(req => req.CardNumber)
            .NotEmpty();
    }
}

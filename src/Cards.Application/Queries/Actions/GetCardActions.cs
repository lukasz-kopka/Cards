using Cards.Application.Models;
using Cards.Application.Services;
using Cards.Core.Constants;
using Cards.Core.Exceptions;

namespace Cards.Application.Queries.Actions;

public sealed record GetCardActions(string UserId, string CardNumber) : IRequest<CardActions>;

public sealed class GetCardActionsHandler : BaseHandler, IRequestHandler<GetCardActions, CardActions>
{
    private readonly ICardService _cardService;
    private readonly Dictionary<ActionName, (List<CardType> CardTypes, List<CardStatus> CardStatuses)> _cardsActions;
    private readonly Dictionary<ActionName, List<(List<CardStatus> CardStatuses, bool IsPinSet)>> _cardsActionsAdditionalRules;

    public GetCardActionsHandler(ICardService cardService, ILogger logger): base(logger)
    {
        _cardService = cardService;
        _cardsActions = AppConstants.CardActions();
        _cardsActionsAdditionalRules = AppConstants.CardActionsWithAdditionalPinData();
    }

    public async Task<CardActions> Handle(GetCardActions request, CancellationToken cancellationToken)
    {
        Logger.Debug(LoggerTemplates.HandlerInvokeInformation, nameof(GetCardActionsHandler), request);

        var card = await _cardService.GetCardDetails(request.UserId, request.CardNumber, cancellationToken);
        List<ActionName> availableActions = new List<ActionName>();

        if (card is null)
        {
            throw new NotFoundException(UserIdOrCardNumberNotFound);
        }

        foreach (var actionName in _cardsActions)
        {
            if (actionName.Value.CardTypes.Contains(card.CardType) && actionName.Value.CardStatuses.Contains(card.CardStatus))
            {
                bool isAvailableAction = true;
                if (_cardsActionsAdditionalRules.ContainsKey(actionName.Key))
                {
                    isAvailableAction = _cardsActionsAdditionalRules[actionName.Key].Exists(x => x.CardStatuses.Contains(card.CardStatus) && x.IsPinSet == card.IsPinSet);
                }

                if (isAvailableAction)
                {
                    availableActions.Add(actionName.Key);
                }
            }
        }

        var result = new CardActions(request.UserId, request.CardNumber, availableActions);
        Logger.Debug(LoggerTemplates.HandlerReturnInformation, nameof(GetCardActionsHandler), result);

        return result;
    }
}

using Cards.Core.Enums;

namespace Cards.Core.Constants;

public static class AppConstants
{
    public static Dictionary<ActionName, (List<CardType>, List<CardStatus>)> CardActions()
    {
        Dictionary<ActionName, (List<CardType>, List<CardStatus>)> result = [];
        List<CardType> allCardType =
            [CardType.Prepaid, CardType.Debit, CardType.Credit];
        List<CardStatus> allCardStatus =
            [CardStatus.Ordered, CardStatus.Inactive, CardStatus.Active, CardStatus.Restricted, CardStatus.Blocked, CardStatus.Expired, CardStatus.Closed];

        result.Add(ActionName.ACTION1, new(allCardType, [CardStatus.Active]));
        result.Add(ActionName.ACTION2, new(allCardType, [CardStatus.Inactive]));
        result.Add(ActionName.ACTION3, new(allCardType, allCardStatus));
        result.Add(ActionName.ACTION4, new(allCardType, allCardStatus));
        result.Add(ActionName.ACTION5, new([CardType.Credit], allCardStatus));
        result.Add(ActionName.ACTION6, new(allCardType, [CardStatus.Ordered, CardStatus.Inactive, CardStatus.Active, CardStatus.Blocked]));
        result.Add(ActionName.ACTION7, new(allCardType, [CardStatus.Ordered, CardStatus.Inactive, CardStatus.Active, CardStatus.Blocked]));
        result.Add(ActionName.ACTION8, new(allCardType, [CardStatus.Ordered, CardStatus.Inactive, CardStatus.Active, CardStatus.Blocked]));
        result.Add(ActionName.ACTION9, new(allCardType, allCardStatus));
        result.Add(ActionName.ACTION10, new(allCardType, [CardStatus.Ordered, CardStatus.Inactive, CardStatus.Active]));
        result.Add(ActionName.ACTION11, new(allCardType, [CardStatus.Inactive, CardStatus.Active]));
        result.Add(ActionName.ACTION12, new(allCardType, [CardStatus.Ordered, CardStatus.Inactive, CardStatus.Active]));
        result.Add(ActionName.ACTION13, new(allCardType, [CardStatus.Ordered, CardStatus.Inactive, CardStatus.Active]));

        return result;
    }

    public static Dictionary<ActionName, List<(List<CardStatus>, bool)>> CardActionsWithAdditionalPinData()
    {
        Dictionary<ActionName, List<(List<CardStatus>, bool)>> result = [];

        result.Add(ActionName.ACTION6, new([new([CardStatus.Ordered, CardStatus.Inactive, CardStatus.Active, CardStatus.Blocked], true)]));
        result.Add(ActionName.ACTION7, new([new ([CardStatus.Ordered, CardStatus.Inactive, CardStatus.Active,], false),
                                                new ([CardStatus.Blocked], true)]));

        return result;
    }
}

namespace Cards.Application.Models;

public sealed record CardDetails(string CardNumber, CardType CardType, CardStatus CardStatus, bool IsPinSet);

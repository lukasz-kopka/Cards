namespace Cards.Application.Models;

public sealed record CardActions(string UserId, string CardNumber, List<ActionName> Actions);

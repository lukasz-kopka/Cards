namespace Cards.Core.Exceptions;

public sealed class InvalidActionException(string? message) : Exception(message)
{
}

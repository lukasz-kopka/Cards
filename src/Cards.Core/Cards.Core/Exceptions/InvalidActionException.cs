namespace Cards.Core.Exceptions;

public sealed class InvalidActionException : Exception
{
    public InvalidActionException(string? message) : base(message)
    {
    }
}

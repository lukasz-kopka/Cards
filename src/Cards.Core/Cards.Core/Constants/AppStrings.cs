namespace Cards.Core.Constants;

public static class AppStrings
{
    public const string SwaggerTitleCardsApi = "CARDS API";

    public static class Errors
    {
        public const string UserIdOrCardNumberNotFound = "Provided user id or card number not found";
    }

    public static class LoggerTemplates
    {
        public const string ExceptionOccured = "Exception occured, {@p1}";
        public const string CardDetailNotFound = "Card detail for user id: {$p1} and card number: {$p2} not found.";
        public const string HandlerInvokeInformation = "Invoke handler: {$p1} with parameter: {@p2}";
        public const string HandlerReturnInformation = "Handler: {$p1} return: {@p2}";
    }
}

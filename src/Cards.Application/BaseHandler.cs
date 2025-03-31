namespace Cards.Application;

public class BaseHandler(ILogger logger)
{
    protected readonly ILogger Logger = logger;
}

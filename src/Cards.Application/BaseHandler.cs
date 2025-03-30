namespace Cards.Application;

public class BaseHandler
{
    protected readonly ILogger Logger;
    public BaseHandler(ILogger logger)
    {
        Logger = logger;
    }
}

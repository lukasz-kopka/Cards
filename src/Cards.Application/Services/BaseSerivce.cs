namespace Cards.Application.Services;

public class BaseSerivce
{
    protected readonly ILogger Logger;
    public BaseSerivce(ILogger logger)
    {
        Logger = logger;
    }
}
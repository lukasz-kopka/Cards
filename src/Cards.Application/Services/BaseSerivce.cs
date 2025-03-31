namespace Cards.Application.Services;

public class BaseSerivce(ILogger logger)
{
    protected readonly ILogger Logger = logger;
}
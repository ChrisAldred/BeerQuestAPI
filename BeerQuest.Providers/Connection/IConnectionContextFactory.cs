namespace BeerQuest.Providers.Connection
{
    public interface IConnectionContextFactory
    {
        ConnectionContext GetCurrentConnectionContext();
    }
}
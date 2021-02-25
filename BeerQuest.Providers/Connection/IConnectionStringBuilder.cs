namespace BeerQuest.Providers.Connection
{
    public interface IConnectionStringBuilder
    {
        IConnectionStringBuilder InitializeWith(string connectionString);
        IConnectionStringBuilder SetApplicationName(string clientName);
        string Build();
    }
}
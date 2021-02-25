using System;
using System.Text.RegularExpressions;

namespace BeerQuest.Providers.Connection
{
    public class ConnectionStringBuilder : IConnectionStringBuilder
    {
        private string _connectionString;
        private string _clientName;

        public IConnectionStringBuilder InitializeWith(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            return this;
        }

        public IConnectionStringBuilder SetApplicationName(string clientName)
        {
            _clientName = clientName ?? throw new ArgumentNullException(nameof(clientName));

            if (ApplicationNameSetToClientName())
            {
                return this;
            }

            if (ApplicationNameMissingFromConnectionString())
            {
                _connectionString = string.Concat(_connectionString, "application Name=", clientName, ";");
                return this;
            }

            _connectionString = Regex.Replace(_connectionString, @"(application Name=)([\w\s]+?);", $"$1{clientName};");

            return this;
        }

        public string Build()
        {
            return _connectionString;
        }

        private bool ApplicationNameSetToClientName()
        {
            var match = GetAppNameMatch();

            if (match.Success && match.Groups["AppName"].Value.Equals(_clientName))
            {
                return true;
            }

            return false;
        }

        private bool ApplicationNameMissingFromConnectionString()
        {
            var match = GetAppNameMatch();

            if (string.IsNullOrEmpty(match.Groups["AppName"]?.Value))
            {
                return true;
            }

            return false;
        }

        private Match GetAppNameMatch()
        {
            return Regex.Match(_connectionString, @"application Name=(?'AppName'[\w\s]*?);");
        }
    }
}
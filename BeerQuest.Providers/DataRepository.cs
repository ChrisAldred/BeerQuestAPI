using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BeerQuest.Providers.Connection;
using Dapper;

namespace BeerQuest.Providers
{
    public class DataRepository : IDataRepository, IDisposable
    {
        private readonly IDbConnection _connection;
        public DataRepository(IConnectionFactory connectionFactory)
        {
            if (connectionFactory == null) throw new ArgumentNullException(nameof(connectionFactory));

            _connection = connectionFactory.CreateConnection();
                          
            _connection.Open();
        }

        public async Task<IReadOnlyList<T>> ExecuteStoredProcedureAsync<T>(string procedureName, object param)
        {
            var rows = await _connection.QueryAsync<T>(
                procedureName,
                param,
                null,
                null,
                CommandType.StoredProcedure
            );

            return rows.ToArray();
        }

        public async Task ExecuteStoredProcedureAsync(string procedureName, object param)
        {
            var rows = await _connection.QueryAsync(
                procedureName,
                param,
                null,
                null,
                CommandType.StoredProcedure
            );
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
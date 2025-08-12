using Dapper;
using Microsoft.Data.SqlClient;

namespace AcceptanceTests.Services.DbClients;

public class DapperClientBase(string connectionString)
{
    public SqlConnection GetConnection() =>
        new SqlConnection(connectionString);

    public async ValueTask<IEnumerable<TValue>> Query<TValue>(string query)
    {
        return await GetConnection().QueryAsync<TValue>(query);
    }
}
namespace AcceptanceTests.Services.DbClients;

public class OrderDbClient(string connectionString)
    : DapperClientBase(connectionString)
{ }
namespace AcceptanceTests.Services.DbClients;

public class PolicyDbClient(string connectionString)
    : DapperClientBase(connectionString)
{ }
namespace AcceptanceTests.Services.DbClients;

public class UserDbClient(string connectionString)
    : DapperClientBase(connectionString)
{ }
namespace AcceptanceTests.Services.DbClients;

public class BillingDbClient(string connectionString)
    : DapperClientBase(connectionString)
{ }
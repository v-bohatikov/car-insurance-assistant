namespace AcceptanceTests.Services.DbClients;

public class DocumentDbClient(string connectionString)
    : DapperClientBase(connectionString)
{ }
using Host.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Host.Infrastructure.Extensions;

public static class ResourceIntegrationExtensions
{
    public static IHostApplicationBuilder AddServiceBusClient(
        this IHostApplicationBuilder builder)
    {
#if !INFRA
        builder.AddAzureServiceBusClient(ApplicationReferences.ServiceBusResourceName);
#endif

        return builder;
    }


    public static IHostApplicationBuilder AddBlobClient(
        this IHostApplicationBuilder builder)
    {
#if !INFRA
        builder.AddAzureBlobServiceClient(
            ApplicationReferences.BlobStorageResourceName);
#endif

        return builder;
    }


    public static IHostApplicationBuilder AddUserDbContext<TDbContext>(
        this IHostApplicationBuilder builder)
        where TDbContext : DbContext
    {
        return builder.AddDbContext<TDbContext>(
            ApplicationReferences.UserDbResourceName);
    }

    public static IHostApplicationBuilder AddPolicyDbContext<TDbContext>(
        this IHostApplicationBuilder builder)
        where TDbContext : DbContext
    {
        return builder.AddDbContext<TDbContext>(
            ApplicationReferences.PolicyDbResourceName);
    }

    public static IHostApplicationBuilder AddOrderDbContext<TDbContext>(
        this IHostApplicationBuilder builder)
        where TDbContext : DbContext
    {
        return builder.AddDbContext<TDbContext>(
            ApplicationReferences.OrderDbResourceName);
    }

    public static IHostApplicationBuilder AddDocumentDbContext<TDbContext>(
        this IHostApplicationBuilder builder)
        where TDbContext : DbContext
    {
        return builder.AddDbContext<TDbContext>(
            ApplicationReferences.DocumentDbResourceName);
    }

    public static IHostApplicationBuilder AddBillingDbContext<TDbContext>(
        this IHostApplicationBuilder builder)
        where TDbContext : DbContext
    {
        return builder.AddDbContext<TDbContext>(
            ApplicationReferences.BillingDbResourceName);
    }

    private static IHostApplicationBuilder AddDbContext<TDbContext>(
        this IHostApplicationBuilder builder,
        string dbResourceReference)
        where TDbContext : DbContext
    {
#if !INFRA
        builder.AddSqlServerDbContext<TDbContext>(dbResourceReference);
#endif

        return builder;
    }


    public static IHostApplicationBuilder AddLoggingDb<TDbContext>(
        this IHostApplicationBuilder builder)
        where TDbContext : DbContext
    {
        return builder.AddNoSqlDbContext<TDbContext>(
            ApplicationReferences.LoggingDbResourceName);
    }

    public static IHostApplicationBuilder AddAuditorDb<TDbContext>(
        this IHostApplicationBuilder builder)
        where TDbContext : DbContext
    {
        return builder.AddNoSqlDbContext<TDbContext>(
            ApplicationReferences.AuditorDbResourceName);
    }

    public static IHostApplicationBuilder AddConversationDb<TDbContext>(
        this IHostApplicationBuilder builder)
        where TDbContext : DbContext
    {
        return builder.AddNoSqlDbContext<TDbContext>(
            ApplicationReferences.ConversationDbResourceName);
    }

    private static IHostApplicationBuilder AddNoSqlDbContext<TDbContext>(
        this IHostApplicationBuilder builder,
        string dbResourceReference)
        where TDbContext : DbContext
    {
#if !INFRA
        builder.AddCosmosDbContext<TDbContext>(
            ApplicationReferences.NoSqlStorageResourceName,
            dbResourceReference);
#endif

        return builder;
    }
}
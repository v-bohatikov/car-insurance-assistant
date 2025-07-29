using Host.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Host.Infrastructure.Extensions;

public static class ResourceIntegrationExtensions
{
    public static TBuilder AddBlobClient<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
#if !INFRA
        builder.AddAzureBlobClient(
            ApplicationReferences.BlobStorageResourceName);
#endif

        return builder;
    }


    public static TBuilder AddUserDbContext<TBuilder, TDbContext>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
        where TDbContext : DbContext
    {
#if !INFRA
        return builder.AddDbContext<TBuilder, TDbContext>(
            ApplicationReferences.UserDbResourceName);
#endif

        return builder;
    }

    public static TBuilder AddPolicyDbContext<TBuilder, TDbContext>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
        where TDbContext : DbContext
    {
#if !INFRA
        builder.AddDbContext<TBuilder, TDbContext>(
            ApplicationReferences.PolicyDbResourceName);
#endif

        return builder;
    }

    public static TBuilder AddOrderDbContext<TBuilder, TDbContext>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
        where TDbContext : DbContext
    {
#if !INFRA
        builder.AddDbContext<TBuilder, TDbContext>(
            ApplicationReferences.OrderDbResourceName);
#endif

        return builder;
    }

    private static TBuilder AddDbContext<TBuilder, TDbContext>(
        this TBuilder builder,
        string dbResourceName)
        where TBuilder : IHostApplicationBuilder
        where TDbContext : DbContext
    {
#if !INFRA
        builder.AddSqlServerDbContext<TDbContext>(dbResourceName);
#endif

        return builder;
    }


    public static TBuilder AddLoggingDb<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
#if !INFRA
        builder.AddMongoDBClient(
            ApplicationReferences.LoggingDbResourceName);
#endif

        return builder;
    }

    public static TBuilder AddAuditorDb<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
#if !INFRA
        builder.AddMongoDBClient(
            ApplicationReferences.AuditorDbResourceName);
#endif

        return builder;
    }

    public static TBuilder AddConversationDb<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
#if !INFRA
        builder.AddMongoDBClient(
            ApplicationReferences.ConversationDbResourceName);
#endif

        return builder;
    }


    public static TBuilder AddServiceBusClient<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
#if !INFRA
        builder.AddAzureServiceBusClient(ApplicationReferences.ServiceBusResourceName);
#endif

        return builder;
    }
}
using Host.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository.Infrastructure.Abstractions;
using Repository.Infrastructure.Interceptors;

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
        builder.AddSqlServerDbContext<TDbContext>(
            dbResourceReference,
            configureDbContextOptions: options => 
                options.AddInterceptors(new AuditingSaveChangesInterceptor()));
#endif

        return builder;
    }


    public static IHostApplicationBuilder AddLoggingDb<TDbContext>(
        this IHostApplicationBuilder builder)
        where TDbContext : DbContext
    {
        return builder.AddNoSqlDbContext<TDbContext>(
            ApplicationReferences.LoggingDbResourceName,
            ApplicationReferences.ErrorContainerResourceName);
    }

    public static IHostApplicationBuilder AddAuditorDb<TDbContext>(
        this IHostApplicationBuilder builder)
        where TDbContext : DbContext
    {
        return builder.AddNoSqlDbContext<TDbContext>(
            ApplicationReferences.AuditorDbResourceName,
            ApplicationReferences.EventsContainerResourceName);
    }

    public static IHostApplicationBuilder AddConversationDb<TDbContext>(
        this IHostApplicationBuilder builder)
        where TDbContext : DbContext
    {
        return builder.AddNoSqlDbContext<TDbContext>(
            ApplicationReferences.ConversationDbResourceName,
            ApplicationReferences.ConversationContainerResourceName);
    }

    private static IHostApplicationBuilder AddNoSqlDbContext<TDbContext>(
        this IHostApplicationBuilder builder,
        string dbResourceReference,
        string defaultContainerReference)
        where TDbContext : DbContext
    {
#if !INFRA
        builder.Services.AddTransient<IDefaultContainerProvider>(_ =>
            DefaultContainerProvider.Create(defaultContainerReference));

        // NOTE: Because we are using the container reference to configure the reference
        // to NoSql database for our projects we need to use the same reference here,
        // so the connection can be resolved later.
        builder.AddCosmosDbContext<TDbContext>(
            defaultContainerReference,
            dbResourceReference,
            configureDbContextOptions: options =>
                options.AddInterceptors(new AuditingSaveChangesInterceptor()));
#endif

        return builder;
    }
}
using Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Extensions;

public static class ResourceIntegrationExtensions
{
    public static TBuilder AddBlobClient<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        builder.AddAzureBlobClient(
            ApplicationReferences.BlobStorageResourceName);
        return builder;
    }


    public static TBuilder AddUserDbContext<TBuilder, TDbContext>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
        where TDbContext : DbContext
    {
        return builder.AddDbContext<TBuilder, TDbContext>(
            ApplicationReferences.UserDbResourceName);
        return builder;
    }

    public static TBuilder AddPolicyDbContext<TBuilder, TDbContext>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
        where TDbContext : DbContext
    {
        return builder.AddDbContext<TBuilder, TDbContext>(
            ApplicationReferences.PolicyDbResourceName);
    }

    public static TBuilder AddOrderDbContext<TBuilder, TDbContext>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
        where TDbContext : DbContext
    {
        return builder.AddDbContext<TBuilder, TDbContext>(
            ApplicationReferences.OrderDbResourceName);
    }

    private static TBuilder AddDbContext<TBuilder, TDbContext>(
        this TBuilder builder,
        string dbResourceName)
        where TBuilder : IHostApplicationBuilder
        where TDbContext : DbContext
    {
        builder.AddSqlServerDbContext<TDbContext>(dbResourceName);
        return builder;
    }


    public static TBuilder AddLoggingDb<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        builder.AddMongoDBClient(
            ApplicationReferences.LoggingDbResourceName);
        return builder;
    }

    public static TBuilder AddAuditorDb<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        builder.AddMongoDBClient(
            ApplicationReferences.AuditorDbResourceName);
        return builder;
    }

    public static TBuilder AddConversationDb<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        builder.AddMongoDBClient(
            ApplicationReferences.ConversationDbResourceName);
        return builder;
    }


    public static TBuilder AddAuditorQueue<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        builder.AddServiceBusQueue(
            ApplicationReferences.AuditorQueueResourceName);
        return builder;
    }

    public static TBuilder AddUserQueue<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        builder.AddServiceBusQueue(
            ApplicationReferences.UserQueueResourceName);
        return builder;
    }

    public static TBuilder AddDocumentQueue<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        builder.AddServiceBusQueue(
            ApplicationReferences.DocumentQueueResourceName);
        return builder;
    }

    public static TBuilder AddPolicyQueue<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        builder.AddServiceBusQueue(
            ApplicationReferences.PolicyQueueResourceName);
        return builder;
    }

    public static TBuilder AddOrderQueue<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        builder.AddServiceBusQueue(
            ApplicationReferences.OrderQueueResourceName);
        return builder;
    }

    public static TBuilder AddBillingQueue<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        builder.AddServiceBusQueue(
            ApplicationReferences.BillingQueueResourceName);
        return builder;
    }

    public static TBuilder AddConversationQueue<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        builder.AddServiceBusQueue(
            ApplicationReferences.ConversationQueueResourceName);
        return builder;
    }

    private static TBuilder AddServiceBusQueue<TBuilder>(
        this TBuilder builder,
        string queueResourceName)
        where TBuilder : IHostApplicationBuilder
    {
        builder.AddAzureServiceBusClient(queueResourceName);
        return builder;
    }
}
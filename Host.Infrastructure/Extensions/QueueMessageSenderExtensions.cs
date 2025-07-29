using Application.Infrastructure.Abstractions;
using Host.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Host.Infrastructure.Extensions;

public static class QueueMessageSenderExtensions
{
    public static TBuilder AddUserQueueMessageSender<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
#if !INFRA
        builder.Services.AddSingleton<IUserQueueMessageSender, UserQueueMessageSender>();
#endif

        return builder;
    }

    public static TBuilder AddAuditorQueueMessageSender<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
#if !INFRA
        builder.Services.AddSingleton<IAuditorQueueMessageSender, AuditorQueueMessageSender>();
#endif

        return builder;
    }

    public static TBuilder AddDocumentQueueMessageSender<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
#if !INFRA
        builder.Services.AddSingleton<IDocumentQueueMessageSender, DocumentQueueMessageSender>();
#endif

        return builder;
    }

    public static TBuilder AddPolicyQueueMessageSender<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
#if !INFRA
        builder.Services.AddSingleton<IPolicyQueueMessageSender, PolicyQueueMessageSender>();
#endif

        return builder;
    }

    public static TBuilder AddOrderQueueMessageSender<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
#if !INFRA
        builder.Services.AddSingleton<IOrderQueueMessageSender, OrderQueueMessageSender>();
#endif

        return builder;
    }

    public static TBuilder AddBillingQueueMessageSender<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
#if !INFRA
        builder.Services.AddSingleton<IBillingQueueMessageSender, BillingQueueMessageSender>();
#endif

        return builder;
    }

    public static TBuilder AddConversationQueueMessageSender<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
#if !INFRA
        builder.Services.AddSingleton<IConversationQueueMessageSender, ConversationQueueMessageSender>();
#endif

        return builder;
    }
}
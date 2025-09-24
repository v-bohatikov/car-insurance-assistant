using Application.Infrastructure.Abstractions;
using Host.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Host.Infrastructure.Extensions;

public static class EventSenderExtensions
{
    public static TBuilder AddUserEventSender<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
#if !INFRA
        builder.Services.AddSingleton<IUserQueueMessageSender, UserQueueMessageSender>();
        builder.Services.AddSingleton<IAuditorQueueMessageSender, AuditorQueueMessageSender>();

        builder.Services.AddSingleton<IUserEventSender, UserEventSender>();
#endif

        return builder;
    }

    public static TBuilder AddDocumentEventSender<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
#if !INFRA
        builder.Services.AddSingleton<IDocumentQueueMessageSender, DocumentQueueMessageSender>();
        builder.Services.AddSingleton<IAuditorQueueMessageSender, AuditorQueueMessageSender>();

        builder.Services.AddSingleton<IDocumentEventSender, DocumentEventSender>();
#endif

        return builder;
    }

    public static TBuilder AddPolicyEventSender<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
#if !INFRA
        builder.Services.AddSingleton<IPolicyQueueMessageSender, PolicyQueueMessageSender>();
        builder.Services.AddSingleton<IAuditorQueueMessageSender, AuditorQueueMessageSender>();

        builder.Services.AddSingleton<IPolicyEventSender, PolicyEventSender>();
#endif

        return builder;
    }

    public static TBuilder AddOrderEventSender<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
#if !INFRA
        builder.Services.AddSingleton<IOrderQueueMessageSender, OrderQueueMessageSender>();
        builder.Services.AddSingleton<IAuditorQueueMessageSender, AuditorQueueMessageSender>();

        builder.Services.AddSingleton<IOrderEventSender, OrderEventSender>();
#endif

        return builder;
    }

    public static TBuilder AddBillingEventSender<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
#if !INFRA
        builder.Services.AddSingleton<IBillingQueueMessageSender, BillingQueueMessageSender>();
        builder.Services.AddSingleton<IAuditorQueueMessageSender, AuditorQueueMessageSender>();

        builder.Services.AddSingleton<IBillingEventSender, BillingEventSender>();
#endif

        return builder;
    }

    public static TBuilder AddConversationEventSender<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
#if !INFRA
        builder.Services.AddSingleton<IConversationQueueMessageSender, ConversationQueueMessageSender>();
        builder.Services.AddSingleton<IAuditorQueueMessageSender, AuditorQueueMessageSender>();

        builder.Services.AddSingleton<IConversationEventSender, ConversationEventSender>();
#endif

        return builder;
    }
}
namespace Host.Infrastructure.Settings;

public static class ApplicationReferences
{
    // Services.
    public static readonly string ConversationalAdapterServiceName = "conversational-adapter";

    public static readonly string OrderProcessorServiceName = "order-processor";

    public static readonly string UserProcessorServiceName = "user-processor";

    public static readonly string DocumentProcessorServiceName = "document-processor";

    public static readonly string PolicyProcessorServiceName = "policy-processor";

    public static readonly string BillingProcessorServiceName = "billing-processor";

    public static readonly string AuditorServiceName = "auditor";

    public static readonly string ApiGatewayServiceName = "api-gateway";

    // Blob storage.
    public static readonly string BlobStorageResourceName = "azure-blob";

    // NoSQL databases.
    public static readonly string NoSqlStorageResourceName = "azure-nosql";
    
    public static readonly string LoggingDbResourceName = "logging-db";

    public static readonly string ErrorContainerResourceName = "errors";

    public static readonly string AuditorDbResourceName = "auditor-db";

    public static readonly string EventsContainerResourceName = "events";

    public static readonly string ConversationDbResourceName = "conversation-db";

    public static readonly string ConversationContainerResourceName = "conversations";

    // SQL databases.
    public static readonly string SqlStorageResourceName = "azure-sql";

    public static readonly string UserDbResourceName = "user-db";

    public static readonly string PolicyDbResourceName = "policy-db";

    public static readonly string OrderDbResourceName = "order-db";

    public static readonly string DocumentDbResourceName = "document-db";

    public static readonly string BillingDbResourceName = "billing-db";

    // Messaging queues.
    public static readonly string ServiceBusResourceName = "azure-service-bus";

    public static readonly string AuditorQueueResourceName = "auditor-queue";

    public static readonly string UserQueueResourceName = "user-queue";

    public static readonly string DocumentQueueResourceName = "document-queue";

    public static readonly string PolicyQueueResourceName = "policy-queue";

    public static readonly string OrderQueueResourceName = "order-queue";

    public static readonly string BillingQueueResourceName = "billing-queue";

    public static readonly string ConversationQueueResourceName = "conversation-queue";
}
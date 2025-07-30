@description('The location for the resource(s) to be deployed.')
param location string = resourceGroup().location

param sku string = 'Standard'

resource azure_service_bus 'Microsoft.ServiceBus/namespaces@2024-01-01' = {
  name: take('azureservicebus-${uniqueString(resourceGroup().id)}', 50)
  location: location
  properties: {
    disableLocalAuth: true
  }
  sku: {
    name: 'Basic'
  }
  tags: {
    'aspire-resource-name': 'azure-service-bus'
    ExampleKey: 'Example value'
  }
}

resource auditor_queue 'Microsoft.ServiceBus/namespaces/queues@2024-01-01' = {
  name: 'auditor-queue'
  parent: azure_service_bus
}

resource user_queue 'Microsoft.ServiceBus/namespaces/queues@2024-01-01' = {
  name: 'user-queue'
  parent: azure_service_bus
}

resource document_queue 'Microsoft.ServiceBus/namespaces/queues@2024-01-01' = {
  name: 'document-queue'
  parent: azure_service_bus
}

resource policy_queue 'Microsoft.ServiceBus/namespaces/queues@2024-01-01' = {
  name: 'policy-queue'
  parent: azure_service_bus
}

resource order_queue 'Microsoft.ServiceBus/namespaces/queues@2024-01-01' = {
  name: 'order-queue'
  parent: azure_service_bus
}

resource billing_queue 'Microsoft.ServiceBus/namespaces/queues@2024-01-01' = {
  name: 'billing-queue'
  parent: azure_service_bus
}

resource conversation_queue 'Microsoft.ServiceBus/namespaces/queues@2024-01-01' = {
  name: 'conversation-queue'
  parent: azure_service_bus
}

output serviceBusEndpoint string = azure_service_bus.properties.serviceBusEndpoint

output name string = azure_service_bus.name
@description('The location for the resource(s) to be deployed.')
param location string = resourceGroup().location

resource azure_nosql 'Microsoft.DocumentDB/databaseAccounts@2024-08-15' = {
  name: take('azurenosql-${uniqueString(resourceGroup().id)}', 44)
  location: location
  properties: {
    locations: [
      {
        locationName: location
        failoverPriority: 0
      }
    ]
    consistencyPolicy: {
      defaultConsistencyLevel: 'Strong'
    }
    databaseAccountOfferType: 'Standard'
    disableLocalAuth: true
  }
  kind: 'MongoDB'
  tags: {
    'aspire-resource-name': 'azure-nosql'
    ExampleKey: 'Example value'
  }
}

resource logging_db 'Microsoft.DocumentDB/databaseAccounts/sqlDatabases@2024-08-15' = {
  name: 'logging-db'
  location: location
  properties: {
    resource: {
      id: 'logging-db'
    }
  }
  parent: azure_nosql
}

resource auditor_db 'Microsoft.DocumentDB/databaseAccounts/sqlDatabases@2024-08-15' = {
  name: 'auditor-db'
  location: location
  properties: {
    resource: {
      id: 'auditor-db'
    }
  }
  parent: azure_nosql
}

output connectionString string = azure_nosql.properties.documentEndpoint

output name string = azure_nosql.name
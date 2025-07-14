targetScope = 'subscription'

@minLength(1)
@maxLength(64)
@description('Name of the environment that can be used as part of naming resource convention, the name of the resource group for your application will use this name, prefixed with rg-')
param environmentName string

@minLength(1)
@description('The location used for all deployed resources')
param location string

@description('Id of the user or app to assign application roles')
param principalId string = ''

@metadata({azd: {
  type: 'generate'
  config: {length:22,noSpecial:true}
  }
})
@secure()
param cache_password string

var tags = {
  'azd-env-name': environmentName
}

resource rg 'Microsoft.Resources/resourceGroups@2022-09-01' = {
  name: 'rg-${environmentName}'
  location: location
  tags: tags
}
module resources 'resources.bicep' = {
  scope: rg
  name: 'resources'
  params: {
    location: location
    tags: tags
    principalId: principalId
  }
}

module azure_nosql 'azure-nosql/azure-nosql.module.bicep' = {
  name: 'azure-nosql'
  scope: rg
  params: {
    location: location
  }
}
module azure_nosql_roles 'azure-nosql-roles/azure-nosql-roles.module.bicep' = {
  name: 'azure-nosql-roles'
  scope: rg
  params: {
    azure_nosql_outputs_name: azure_nosql.outputs.name
    location: location
    principalId: resources.outputs.MANAGED_IDENTITY_PRINCIPAL_ID
  }
}
module azure_service_bus 'azure-service-bus/azure-service-bus.module.bicep' = {
  name: 'azure-service-bus'
  scope: rg
  params: {
    location: location
  }
}
module azure_service_bus_roles 'azure-service-bus-roles/azure-service-bus-roles.module.bicep' = {
  name: 'azure-service-bus-roles'
  scope: rg
  params: {
    azure_service_bus_outputs_name: azure_service_bus.outputs.name
    location: location
    principalId: resources.outputs.MANAGED_IDENTITY_PRINCIPAL_ID
    principalType: 'ServicePrincipal'
  }
}
module azure_sql 'azure-sql/azure-sql.module.bicep' = {
  name: 'azure-sql'
  scope: rg
  params: {
    location: location
  }
}
module azure_sql_roles 'azure-sql-roles/azure-sql-roles.module.bicep' = {
  name: 'azure-sql-roles'
  scope: rg
  params: {
    azure_sql_outputs_name: azure_sql.outputs.name
    azure_sql_outputs_sqlserveradminname: azure_sql.outputs.sqlServerAdminName
    location: location
    principalId: resources.outputs.MANAGED_IDENTITY_PRINCIPAL_ID
    principalName: resources.outputs.MANAGED_IDENTITY_NAME
    principalType: 'ServicePrincipal'
  }
}
module azure_storage 'azure-storage/azure-storage.module.bicep' = {
  name: 'azure-storage'
  scope: rg
  params: {
    location: location
  }
}
module azure_storage_roles 'azure-storage-roles/azure-storage-roles.module.bicep' = {
  name: 'azure-storage-roles'
  scope: rg
  params: {
    azure_storage_outputs_name: azure_storage.outputs.name
    location: location
    principalId: resources.outputs.MANAGED_IDENTITY_PRINCIPAL_ID
    principalType: 'ServicePrincipal'
  }
}
module infra 'infra/infra.module.bicep' = {
  name: 'infra'
  scope: rg
  params: {
    location: location
  }
}

output MANAGED_IDENTITY_CLIENT_ID string = resources.outputs.MANAGED_IDENTITY_CLIENT_ID
output MANAGED_IDENTITY_NAME string = resources.outputs.MANAGED_IDENTITY_NAME
output AZURE_LOG_ANALYTICS_WORKSPACE_NAME string = resources.outputs.AZURE_LOG_ANALYTICS_WORKSPACE_NAME
output AZURE_CONTAINER_REGISTRY_ENDPOINT string = resources.outputs.AZURE_CONTAINER_REGISTRY_ENDPOINT
output AZURE_CONTAINER_REGISTRY_MANAGED_IDENTITY_ID string = resources.outputs.AZURE_CONTAINER_REGISTRY_MANAGED_IDENTITY_ID
output AZURE_CONTAINER_REGISTRY_NAME string = resources.outputs.AZURE_CONTAINER_REGISTRY_NAME
output AZURE_CONTAINER_APPS_ENVIRONMENT_NAME string = resources.outputs.AZURE_CONTAINER_APPS_ENVIRONMENT_NAME
output AZURE_CONTAINER_APPS_ENVIRONMENT_ID string = resources.outputs.AZURE_CONTAINER_APPS_ENVIRONMENT_ID
output AZURE_CONTAINER_APPS_ENVIRONMENT_DEFAULT_DOMAIN string = resources.outputs.AZURE_CONTAINER_APPS_ENVIRONMENT_DEFAULT_DOMAIN
output AZURE_NOSQL_CONNECTIONSTRING string = azure_nosql.outputs.connectionString
output AZURE_SERVICE_BUS_SERVICEBUSENDPOINT string = azure_service_bus.outputs.serviceBusEndpoint
output AZURE_SQL_SQLSERVERFQDN string = azure_sql.outputs.sqlServerFqdn
output AZURE_STORAGE_BLOBENDPOINT string = azure_storage.outputs.blobEndpoint

@description('The location for the resource(s) to be deployed.')
param location string = resourceGroup().location

resource sqlServerAdminManagedIdentity 'Microsoft.ManagedIdentity/userAssignedIdentities@2023-01-31' = {
  name: take('azure_sql-admin-${uniqueString(resourceGroup().id)}', 63)
  location: location
}

resource azure_sql 'Microsoft.Sql/servers@2021-11-01' = {
  name: take('azuresql-${uniqueString(resourceGroup().id)}', 63)
  location: location
  properties: {
    administrators: {
      administratorType: 'ActiveDirectory'
      login: sqlServerAdminManagedIdentity.name
      sid: sqlServerAdminManagedIdentity.properties.principalId
      tenantId: subscription().tenantId
      azureADOnlyAuthentication: true
    }
    minimalTlsVersion: '1.2'
    publicNetworkAccess: 'Enabled'
    version: '12.0'
  }
  tags: {
    'aspire-resource-name': 'azure-sql'
  }
}

resource sqlFirewallRule_AllowAllAzureIps 'Microsoft.Sql/servers/firewallRules@2021-11-01' = {
  name: 'AllowAllAzureIps'
  properties: {
    endIpAddress: '0.0.0.0'
    startIpAddress: '0.0.0.0'
  }
  parent: azure_sql
}

resource user_db 'Microsoft.Sql/servers/databases@2023-08-01' = {
  name: 'user-db'
  location: location
  properties: {
    freeLimitExhaustionBehavior: 'AutoPause'
    useFreeLimit: true
  }
  sku: {
    name: 'GP_S_Gen5_2'
  }
  parent: azure_sql
}

resource policy_db 'Microsoft.Sql/servers/databases@2023-08-01' = {
  name: 'policy-db'
  location: location
  properties: {
    freeLimitExhaustionBehavior: 'AutoPause'
    useFreeLimit: true
  }
  sku: {
    name: 'GP_S_Gen5_2'
  }
  parent: azure_sql
}

resource order_db 'Microsoft.Sql/servers/databases@2023-08-01' = {
  name: 'order-db'
  location: location
  properties: {
    freeLimitExhaustionBehavior: 'AutoPause'
    useFreeLimit: true
  }
  sku: {
    name: 'GP_S_Gen5_2'
  }
  parent: azure_sql
}

output sqlServerFqdn string = azure_sql.properties.fullyQualifiedDomainName

output name string = azure_sql.name

output sqlServerAdminName string = sqlServerAdminManagedIdentity.name
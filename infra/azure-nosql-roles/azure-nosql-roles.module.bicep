@description('The location for the resource(s) to be deployed.')
param location string = resourceGroup().location

param azure_nosql_outputs_name string

param principalId string

resource azure_nosql 'Microsoft.DocumentDB/databaseAccounts@2024-08-15' existing = {
  name: azure_nosql_outputs_name
}

resource azure_nosql_roleDefinition 'Microsoft.DocumentDB/databaseAccounts/sqlRoleDefinitions@2024-08-15' existing = {
  name: '00000000-0000-0000-0000-000000000002'
  parent: azure_nosql
}

resource azure_nosql_roleAssignment 'Microsoft.DocumentDB/databaseAccounts/sqlRoleAssignments@2024-08-15' = {
  name: guid(principalId, azure_nosql_roleDefinition.id, azure_nosql.id)
  properties: {
    principalId: principalId
    roleDefinitionId: azure_nosql_roleDefinition.id
    scope: azure_nosql.id
  }
  parent: azure_nosql
}
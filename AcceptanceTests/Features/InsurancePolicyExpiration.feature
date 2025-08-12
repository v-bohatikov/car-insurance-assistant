Feature: InsurancePolicyExpiration

Expiration of insurance policy after the period of validity has expired

@policy @auditor
Scenario: Expiration of insurance policy after the period of validity has expired
	Given policy is issued
	When period of validity has expired
	Then insurance policy should be moved to failed status with expiration reason
	And insurance policy document should be removed
	And event about policy expiration should be registered in Auditor

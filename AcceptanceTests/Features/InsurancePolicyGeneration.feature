Feature: InsurancePolicyGeneration

Generation of insurance policy after the completion of order

@order @policy @document @auditor
Scenario: Generation of insurance policy after the completion of order
	Given user is confirmed
	And vehicle is confirmed
	And insurance plan is existed
	And there are no active insurance policies for the vehicle
	When order is completed
	Then insurance policy should be created in pending status
	And request for generation of policy document should be registered
	And event about policy creation should be registered in Auditor

@policy @document @auditor
Scenario: Failure of insurance policy after the policy document generation have failed
	Given policy is pending
	When policy document generation have failed
	Then insurance policy should be moved to failed status with provided reason
	And event about policy failure should be registered in Auditor

@policy @document @auditor
Scenario: Completion of insurance policy after the policy document generation have succeeded
	Given policy is pending
	When policy document generation have succeeded
	Then insurance policy should be moved to issued state
	And event about policy completion should be registered in Auditor

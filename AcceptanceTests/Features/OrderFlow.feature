Feature: OrderFlow

Creation and processing of order after the request to issue the insurance policy

@order @user @vehicle @policy @auditor
Scenario: Creation of order after the request to issue the insurance policy from user
	Given user is confirmed
	And vehicle is confirmed
	And insurance plan is existed
	And there are no active insurance policies for the vehicle
	When request to issue insurance policy is received
	Then order should be created in pending status
	And request for approval of order from employee should be registered
	And event about order creation should be registered in Auditor

@order @employee @auditor
Scenario: Failure of order after the request for approval was declined by employee 
	Given order is in pending status
	When request for approval was declined by employee 
	Then order should be moved to failed status with provided reason
	And event about order request decline should be registered in Auditor
	
@order @employee @billing @auditor
Scenario: Payment request creation for the order after the request for approval was approved by employee 
	Given order is in pending status
	When request for approval was approved by employee 
	Then order should be moved to approved status
	And request for payment should be registered
	And event about order request approval should be registered in Auditor

@order @billing @auditor
Scenario: Failure of order after the payment has failed
	Given order is in approved status
	When payment has failed/declined
	Then order should be moved to failed status with provided reason
	And event about payment failure should be registered in Auditor

@order @billing @auditor
Scenario: Completion of order after the payment was processed successfully
	Given order is in approved status
	When payment was processed successfully
	Then order should be moved to completed status
	And event about order completion should be registered in Auditor


Feature: VehicleRegistration

Registration of user's vehicle via vehicle registration certificate

@user @vehicle @document @auditor
Scenario: Registration of vehicle after vehicle registration certificate is being provided
	Given user is confirmed
	And vehicle is registered on this user 
	When vehicle registration is being provided
	Then vehicle should be created
	And correct registration data should be set for the registered vehicle
	And vehicle registration certificate should be stored
	And event about vehicle registration should be registered in Auditor

@user @vehicle @document @auditor
Scenario: Removal of vehicle and vehicle registration document after the decline of the results of vehicle data extraction
	Given user is confirmed
	And vehicle is created
	And vehicle registration document is stored
	When user declines the results of vehicle data extraction
	Then vehicle should be removed
	And vehicle registration document should be removed
	And event about the decline of vehicle data extraction should be registered in Auditor
	
@user @vehicle @document @auditor
Scenario: Confirmation of vehicle after the approval of the results of vehicle data extraction
	Given user is confirmed
	And vehicle is created
	And vehicle registration document is stored
	When user approves the results of vehicle data extraction
	Then vehicle should be confirmed
	And event about vehicle confirmation should be registered in Auditor

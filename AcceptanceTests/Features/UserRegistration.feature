Feature: UserRegistration

Registration of the new user and their confirmation via passport

@user @auditor
Scenario: Registeration of user after phone number is being provided
	Given user provided correct phone number
	And there is no user with the same phone number registered
	When user contact information is provided
	Then a new user should be created
	And user creation event should be registered in Auditor

@user @document @auditor
Scenario: Extraction of passport data for existed user after passport is being provided
	Given user is created
	And passport data is not set for the user
	When passport document is being provided
	Then passport document should be successfully processed
	And user should still be in created state
	And correct passport data should be set for this user
	And passport document should be stored
	And event about user passport being processed should be registered in Auditor

@user @document @auditor
Scenario: Removal of extracted data and passport document after the decline of the results of passport data extraction
	Given user is created
	And passport data is set for the user
	And passport document is stored
	When user declines the results of passport data extraction
	Then passport data should be removed from the user
	And passport document should be removed
	And event about the decline of passport data extraction should be registered in Auditor
	
@user @document @auditor
Scenario: Confirmation of user after the approve of results of passport data extraction
	Given user is created
	And passport data is set for the user
	And passport document is stored
	When user approves the results of passport data extraction
	Then user should be confirmed
	And event about the user confirmation should be registered in Auditor

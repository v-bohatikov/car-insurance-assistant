namespace AcceptanceTests.StepDefinitions;

[Binding]
public class UserStepDefinitions(
    FeatureContext featureContext,
    ScenarioContext scenarioContext)
    : StepDefinitionsBase(featureContext, scenarioContext)
{

    [Given("user provided correct phone number")]
    public void GivenUserProvidedCorrectPhoneNumber()
    {
        throw new PendingStepException();
    }

    [Given("there is no user with the same phone number registered")]
    public void GivenThereIsNoUserWithTheSamePhoneNumberRegistered()
    {
        throw new PendingStepException();
    }

    [When("user contact information is provided")]
    public void WhenUserContactInformationIsProvided()
    {
        throw new PendingStepException();
    }

    [Then("a new user should be created")]
    public void ThenANewUserShouldBeCreated()
    {
        throw new PendingStepException();
    }

    [Then("user creation event should be registered in Auditor")]
    public void ThenUserCreationEventShouldBeRegisteredInAuditor()
    {
        throw new PendingStepException();
    }

    [Given("user is created")]
    public void GivenUserIsCreated()
    {
        throw new PendingStepException();
    }

    [Given("passport data is not set for the user")]
    public void GivenPassportDataIsNotSetForTheUser()
    {
        throw new PendingStepException();
    }

    [Then("user should still be in created state")]
    public void ThenUserShouldStillBeInCreatedState()
    {
        throw new PendingStepException();
    }

    [Then("correct passport data should be set for this user")]
    public void ThenCorrectPassportDataShouldBeSetForThisUser()
    {
        throw new PendingStepException();
    }

    [Given("passport data is set for the user")]
    public void GivenPassportDataIsSetForTheUser()
    {
        throw new PendingStepException();
    }

    [When("user declines the results of passport data extraction")]
    public void WhenUserDeclinesTheResultsOfPassportDataExtraction()
    {
        throw new PendingStepException();
    }

    [Then("passport data should be removed from the user")]
    public void ThenPassportDataShouldBeRemovedFromTheUser()
    {
        throw new PendingStepException();
    }

    [When("user approves the results of passport data extraction")]
    public void WhenUserApprovesTheResultsOfPassportDataExtraction()
    {
        throw new PendingStepException();
    }

    [Then("user should be confirmed")]
    public void ThenUserShouldBeConfirmed()
    {
        throw new PendingStepException();
    }

    [Given("user is confirmed")]
    public void GivenUserIsConfirmed()
    {
        throw new PendingStepException();
    }

    [Given("vehicle is registered on this user")]
    public void GivenVehicleIsRegisteredOnThisUser()
    {
        throw new PendingStepException();
    }

    [When("vehicle registration is being provided")]
    public void WhenVehicleRegistrationIsBeingProvided()
    {
        throw new PendingStepException();
    }

    [Then("vehicle should be created")]
    public void ThenVehicleShouldBeCreated()
    {
        throw new PendingStepException();
    }

    [Then("correct registration data should be set for the registered vehicle")]
    public void ThenCorrectRegistrationDataShouldBeSetForTheRegisteredVehicle()
    {
        throw new PendingStepException();
    }

    [Given("vehicle is created")]
    public void GivenVehicleIsCreated()
    {
        throw new PendingStepException();
    }

    [When("user declines the results of vehicle data extraction")]
    public void WhenUserDeclinesTheResultsOfVehicleDataExtraction()
    {
        throw new PendingStepException();
    }

    [Then("vehicle should be removed")]
    public void ThenVehicleShouldBeRemoved()
    {
        throw new PendingStepException();
    }

    [Given("vehicle is confirmed")]
    public void GivenVehicleIsConfirmed()
    {
        throw new PendingStepException();
    }
}
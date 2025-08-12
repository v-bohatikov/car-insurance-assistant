namespace AcceptanceTests.StepDefinitions;

[Binding]
public class AuditorStepDefinitions(
    FeatureContext featureContext,
    ScenarioContext scenarioContext)
    : StepDefinitionsBase(featureContext, scenarioContext)
{
    [Then("event about user passport being processed should be registered in Auditor")]
    public void ThenEventAboutUserPassportBeingProcessedShouldBeRegisteredInAuditor()
    {
        throw new PendingStepException();
    }

    [Then("event about the decline of passport data extraction should be registered in Auditor")]
    public void ThenEventAboutTheDeclineOfPassportDataExtractionShouldBeRegisteredInAuditor()
    {
        throw new PendingStepException();
    }

    [Then("event about the user confirmation should be registered in Auditor")]
    public void ThenEventAboutTheUserConfirmationShouldBeRegisteredInAuditor()
    {
        throw new PendingStepException();
    }

    [Then("event about vehicle registration should be registered in Auditor")]
    public void ThenEventAboutVehicleRegistrationShouldBeRegisteredInAuditor()
    {
        throw new PendingStepException();
    }

    [Then("event about the decline of vehicle data extraction should be registered in Auditor")]
    public void ThenEventAboutTheDeclineOfVehicleDataExtractionShouldBeRegisteredInAuditor()
    {
        throw new PendingStepException();
    }

    [When("user approves the results of vehicle data extraction")]
    public void WhenUserApprovesTheResultsOfVehicleDataExtraction()
    {
        throw new PendingStepException();
    }

    [Then("vehicle should be confirmed")]
    public void ThenVehicleShouldBeConfirmed()
    {
        throw new PendingStepException();
    }

    [Then("event about vehicle confirmation should be registered in Auditor")]
    public void ThenEventAboutVehicleConfirmationShouldBeRegisteredInAuditor()
    {
        throw new PendingStepException();
    }

    [Then("event about order creation should be registered in Auditor")]
    public void ThenEventAboutOrderCreationShouldBeRegisteredInAuditor()
    {
        throw new PendingStepException();
    }

    [Then("event about order request decline should be registered in Auditor")]
    public void ThenEventAboutOrderRequestDeclineShouldBeRegisteredInAuditor()
    {
        throw new PendingStepException();
    }

    [When("request for approval was approved by employee")]
    public void WhenRequestForApprovalWasApprovedByEmployee()
    {
        throw new PendingStepException();
    }

    [Then("order should be moved to approved status")]
    public void ThenOrderShouldBeMovedToApprovedStatus()
    {
        throw new PendingStepException();
    }

    [Then("event about order request approval should be registered in Auditor")]
    public void ThenEventAboutOrderRequestApprovalShouldBeRegisteredInAuditor()
    {
        throw new PendingStepException();
    }

    [Then("event about payment failure should be registered in Auditor")]
    public void ThenEventAboutPaymentFailureShouldBeRegisteredInAuditor()
    {
        throw new PendingStepException();
    }

    [Then("event about order completion should be registered in Auditor")]
    public void ThenEventAboutOrderCompletionShouldBeRegisteredInAuditor()
    {
        throw new PendingStepException();
    }

    [Then("event about policy creation should be registered in Auditor")]
    public void ThenEventAboutPolicyCreationShouldBeRegisteredInAuditor()
    {
        throw new PendingStepException();
    }

    [Then("event about policy failure should be registered in Auditor")]
    public void ThenEventAboutPolicyFailureShouldBeRegisteredInAuditor()
    {
        throw new PendingStepException();
    }

    [Then("event about policy completion should be registered in Auditor")]
    public void ThenEventAboutPolicyCompletionShouldBeRegisteredInAuditor()
    {
        throw new PendingStepException();
    }

    [Then("event about policy expiration should be registered in Auditor")]
    public void ThenEventAboutPolicyExpirationShouldBeRegisteredInAuditor()
    {
        throw new PendingStepException();
    }
}
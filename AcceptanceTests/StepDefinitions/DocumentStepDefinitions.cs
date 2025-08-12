namespace AcceptanceTests.StepDefinitions;

[Binding]
public class DocumentStepDefinitions(
    FeatureContext featureContext,
    ScenarioContext scenarioContext)
    : StepDefinitionsBase(featureContext, scenarioContext)
{
    [When("passport document is being provided")]
    public void WhenPassportDocumentIsBeingProvided()
    {
        throw new PendingStepException();
    }

    [Then("passport document should be successfully processed")]
    public void ThenPassportDocumentShouldBeSuccessfullyProcessed()
    {
        throw new PendingStepException();
    }

    [Then("passport document should be stored")]
    public void ThenPassportDocumentShouldBeStored()
    {
        throw new PendingStepException();
    }

    [Given("passport document is stored")]
    public void GivenPassportDocumentIsStored()
    {
        throw new PendingStepException();
    }

    [Then("passport document should be removed")]
    public void ThenPassportDocumentShouldBeRemoved()
    {
        throw new PendingStepException();
    }

    [Then("vehicle registration certificate should be stored")]
    public void ThenVehicleRegistrationCertificateShouldBeStored()
    {
        throw new PendingStepException();
    }

    [Given("vehicle registration document is stored")]
    public void GivenVehicleRegistrationDocumentIsStored()
    {
        throw new PendingStepException();
    }

    [Then("vehicle registration document should be removed")]
    public void ThenVehicleRegistrationDocumentShouldBeRemoved()
    {
        throw new PendingStepException();
    }

    [Then("request for generation of policy document should be registered")]
    public void ThenRequestForGenerationOfPolicyDocumentShouldBeRegistered()
    {
        throw new PendingStepException();
    }

    [When("policy document generation have failed")]
    public void WhenPolicyDocumentGenerationHaveFailed()
    {
        throw new PendingStepException();
    }

    [Then("insurance policy should be moved to failed status with provided reason")]
    public void ThenInsurancePolicyShouldBeMovedToFailedStatusWithProvidedReason()
    {
        throw new PendingStepException();
    }

    [When("policy document generation have succeeded")]
    public void WhenPolicyDocumentGenerationHaveSucceeded()
    {
        throw new PendingStepException();
    }

    [Then("insurance policy document should be removed")]
    public void ThenInsurancePolicyDocumentShouldBeRemoved()
    {
        throw new PendingStepException();
    }
}
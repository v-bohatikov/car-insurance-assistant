namespace AcceptanceTests.StepDefinitions;

[Binding]
public class PolicyStepDefinitions(
    FeatureContext featureContext,
    ScenarioContext scenarioContext)
    : StepDefinitionsBase(featureContext, scenarioContext)
{
    [Given("insurance plan is existed")]
    public void GivenInsurancePlanIsExisted()
    {
        throw new PendingStepException();
    }

    [Given("there are no active insurance policies for the vehicle")]
    public void GivenThereAreNoActiveInsurancePoliciesForTheVehicle()
    {
        throw new PendingStepException();
    }

    [When("request to issue insurance policy is received")]
    public void WhenRequestToIssueInsurancePolicyIsReceived()
    {
        throw new PendingStepException();
    }

    [Given("policy is pending")]
    public void GivenPolicyIsPending()
    {
        throw new PendingStepException();
    }

    [Then("insurance policy should be moved to issued state")]
    public void ThenInsurancePolicyShouldBeMovedToIssuedState()
    {
        throw new PendingStepException();
    }

    [Given("policy is issued")]
    public void GivenPolicyIsIssued()
    {
        throw new PendingStepException();
    }

    [When("period of validity has expired")]
    public void WhenPeriodOfValidityHasExpired()
    {
        throw new PendingStepException();
    }

    [Then("insurance policy should be moved to failed status with expiration reason")]
    public void ThenInsurancePolicyShouldBeMovedToFailedStatusWithExpirationReason()
    {
        throw new PendingStepException();
    }
}
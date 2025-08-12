namespace AcceptanceTests.StepDefinitions;

[Binding]
public class OrderStepDefinitions(
    FeatureContext featureContext,
    ScenarioContext scenarioContext)
    : StepDefinitionsBase(featureContext, scenarioContext)
{
    [Then("order should be created in pending status")]
    public void ThenOrderShouldBeCreatedInPendingStatus()
    {
        throw new PendingStepException();
    }

    [Then("request for approval of order from employee should be registered")]
    public void ThenRequestForApprovalOfOrderFromEmployeeShouldBeRegistered()
    {
        throw new PendingStepException();
    }

    [Given("order is in pending status")]
    public void GivenOrderIsInPendingStatus()
    {
        throw new PendingStepException();
    }

    [When("request for approval was declined by employee")]
    public void WhenRequestForApprovalWasDeclinedByEmployee()
    {
        throw new PendingStepException();
    }

    [Then("order should be moved to failed status with provided reason")]
    public void ThenOrderShouldBeMovedToFailedStatusWithProvidedReason()
    {
        throw new PendingStepException();
    }

    [Given("order is in approved status")]
    public void GivenOrderIsInApprovedStatus()
    {
        throw new PendingStepException();
    }

    [Then("order should be moved to completed status")]
    public void ThenOrderShouldBeMovedToCompletedStatus()
    {
        throw new PendingStepException();
    }

    [When("order is completed")]
    public void WhenOrderIsCompleted()
    {
        throw new PendingStepException();
    }

    [Then("insurance policy should be created in pending status")]
    public void ThenInsurancePolicyShouldBeCreatedInPendingStatus()
    {
        throw new PendingStepException();
    }
}
namespace AcceptanceTests.StepDefinitions;

[Binding]
public class BillingStepDefinitions(
    FeatureContext featureContext,
    ScenarioContext scenarioContext)
    : StepDefinitionsBase(featureContext, scenarioContext)
{
    [Then("request for payment should be registered")]
    public void ThenRequestForPaymentShouldBeRegistered()
    {
        throw new PendingStepException();
    }

    [When("payment has failed\\/declined")]
    public void WhenPaymentHasFailedDeclined()
    {
        throw new PendingStepException();
    }

    [When("payment was processed successfully")]
    public void WhenPaymentWasProcessedSuccessfully()
    {
        throw new PendingStepException();
    }
}
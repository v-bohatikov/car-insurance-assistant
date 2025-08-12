namespace AcceptanceTests.Support;

public static class ContextExtensions
{
    public static void SetApplicationInstance(
        this FeatureContext featureContext,
        TestApplicationInstance applicationInstance)
    {
        featureContext.FeatureContainer.RegisterInstanceAs(applicationInstance);
    }

    public static TestApplicationInstance GetApplicationInstance(
        this FeatureContext featureContext)
    {
        return featureContext.FeatureContainer.Resolve<TestApplicationInstance>();
    }
}
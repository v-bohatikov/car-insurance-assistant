using AcceptanceTests.Support;

namespace AcceptanceTests.Hooks;

[Binding]
public class DefaultHooks
{
    [BeforeFeature]
    public static async Task BeforeFeature(FeatureContext featureContext)
    {
        var applicationInstance = await TestApplicationRunner.RunApplicationAsync();
        featureContext.SetApplicationInstance(applicationInstance);
    }

    [AfterFeature]
    public static async Task AfterFeature(FeatureContext featureContext)
    {
        var applicationInstance = featureContext.GetApplicationInstance();

        await applicationInstance.DisposeAsync();
    }
}
using Aspire.Hosting;

namespace AcceptanceTests.Support;

public sealed class TestApplicationInstance(
    DistributedApplication application,
    IServiceProvider serviceProvider,
    CancellationTokenSource cancellationTokenSource)
    : IAsyncDisposable
{
    private bool _isDisposed = false;

    public DistributedApplication Application => application;

    public CancellationToken CancellationToken => cancellationTokenSource.Token;

    public async ValueTask DisposeAsync()
    {
        if (_isDisposed)
        {
            return;
        }

        await StopApplicationAsync();
        _isDisposed = true;
    }

    public TService GetService<TService>()
        where TService : notnull
    {
        return serviceProvider.GetRequiredService<TService>();
    }

    public TService GetKeyedService<TService>(string name)
        where TService : notnull
    {
        return serviceProvider.GetRequiredKeyedService<TService>(name);
    }

    private async ValueTask StopApplicationAsync()
    {
        await cancellationTokenSource.CancelAsync();
        cancellationTokenSource.Dispose();

        await application.StopAsync(CancellationToken.None);
    }
}

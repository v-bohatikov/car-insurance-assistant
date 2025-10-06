using Auditor.Infrastructure.Abstractions;
using Auditor.Infrastructure.Contracts.EventReceived;
using MassTransit.Mediator;
using SharedKernel.Results;

namespace Auditor.Application.Consumers;

public class ReceivedEventConsumer(
    IEventStorageService eventStorageService)
    : MediatorRequestHandler<ReceivedEventDto, Result>
{
    protected override async Task<Result> Handle(
        ReceivedEventDto request,
        CancellationToken cancellationToken)
    {
        return await eventStorageService.StoreReceivedEventAsync(
            request, cancellationToken);
    }
}
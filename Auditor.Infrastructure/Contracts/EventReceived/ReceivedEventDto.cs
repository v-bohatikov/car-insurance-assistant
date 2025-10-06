using MassTransit.Mediator;
using SharedKernel.BaseTypes;
using SharedKernel.Results;

namespace Auditor.Infrastructure.Contracts.EventReceived;

public record ReceivedEventDto(EventBase ReceivedEvent)
    : Request<Result>;
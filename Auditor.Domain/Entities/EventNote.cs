using SharedKernel;
using SharedKernel.BaseTypes;

namespace Auditor.Domain.Entities;

public class EventNote : ContainerEntity
{
    private EventNote(EventBase occuredEvent)
        : base(occuredEvent.Id, occuredEvent.UserId)
    {
        OccuredEvent = occuredEvent;
    }

    public EventBase OccuredEvent { get; }

    public static EventNote CreateEvent(EventBase occuredEvent)
    {
        return new EventNote(occuredEvent);
    }
}
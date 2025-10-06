using FluentAssertions;
using Newtonsoft.Json;
using SharedKernel.BaseTypes;
using Tests.Models;

namespace Tests.Serialization;

public class NewtonJsonSerializationTests
{
    [Test]
    public void GivenSerializedObjectDerivedFromEventBaseTypeWithObjectsTypeNameHandling_WhenDeserializedToEventBaseType_ThenDeserializedObjectIsTheSameAsOrigin()
    {
        // Arrange.
        var userId = Ulid.NewUlid();
        var eventDescription = "test";
        var originalEvent = new SpecificEvent(userId, eventDescription);

        var jsonSerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects
        };
        var serializedValue = JsonConvert
            .SerializeObject(originalEvent, originalEvent.GetType(), jsonSerializerSettings);

        // Act.
        var deserializedEvent = JsonConvert
            .DeserializeObject<EventBase>(serializedValue, jsonSerializerSettings)!;

        // Assert.
        deserializedEvent
            .Should()
            .BeOfType<SpecificEvent>();
        var deserializedSpecificEvent = (SpecificEvent)deserializedEvent;

        deserializedSpecificEvent.UserId
            .Should()
            .Be(userId);
        deserializedSpecificEvent.Description
            .Should()
            .Be(eventDescription);
    }

    [Test]
    public void GivenArrayOfSerializedObjectsDerivedFromEventBaseTypeWithObjectsTypeNameHandling_WhenDeserializedToEventBaseType_ThenDeserializedObjectsIsTheSameAsOrigins()
    {
        // Arrange.
        var userId1 = Ulid.NewUlid();
        var userId2 = Ulid.NewUlid();

        var eventDescription1 = "description1";
        var eventDescription2 = "description2";

        var eventNote = "note";

        var originalEvent1 = new SpecificEvent(userId1, eventDescription1);
        var originalEvent2 = new EvenMoreSpecificEvent(userId2, eventDescription2, eventNote);

        var array = new EventBase[] { originalEvent1, originalEvent2 };

        var jsonSerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects
        };
        var serializedEvents = JsonConvert
            .SerializeObject(array, array.GetType(), jsonSerializerSettings);

        // Act.
        var deserializedEvents = JsonConvert
            .DeserializeObject<EventBase[]>(serializedEvents, jsonSerializerSettings)!;

        // Assert.
        deserializedEvents
            .Should()
            .HaveCount(2);

        var deserializedEvent1 = deserializedEvents[0];
        var deserializedEvent2 = deserializedEvents[1];

        deserializedEvent1
            .Should()
            .BeOfType<SpecificEvent>();
        var deserializedSpecificEvent = (SpecificEvent)deserializedEvent1;

        deserializedSpecificEvent.UserId
            .Should()
            .Be(userId1);
        deserializedSpecificEvent.Description
            .Should()
            .Be(eventDescription1);

        deserializedEvent2
            .Should()
            .BeOfType<EvenMoreSpecificEvent>();
        var deserializedEvenMoreSpecificEvent = (EvenMoreSpecificEvent)deserializedEvent2;

        deserializedEvenMoreSpecificEvent.UserId
            .Should()
            .Be(userId2);
        deserializedEvenMoreSpecificEvent.Description
            .Should()
            .Be(eventDescription2);
        deserializedEvenMoreSpecificEvent.Note
            .Should()
            .Be(eventNote);
    }
}
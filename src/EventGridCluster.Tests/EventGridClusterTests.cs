using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Rest.Azure;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EventGridCluster.Tests
{
    public class EventGridClusterTests
    {
        private readonly Mock<IEventGridClient> client;
        private readonly EventGridCluster publisher;

        public EventGridClusterTests()
        {
            client = new Mock<IEventGridClient>();
            client.SetupGet(self => self.Credentials).Returns(new TopicCredentialSet(new Dictionary<string, string>() {
                { "endpoint", "a_key"}
            }));
            client.Setup(self => self.PublishEventsWithHttpMessagesAsync(It.IsAny<string>()
                , It.IsAny<IList<EventGridEvent>>()
                , It.IsAny<Dictionary<string, List<string>>>()
                , It.IsAny<CancellationToken>())).ReturnsAsync(new AzureOperationResponse());

            publisher = new EventGridCluster(client.Object);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        public async Task Should_publish_same_amount_of_events_as_it_is_given(int expected)
        {
            // arrange
            var events = Enumerable.Repeat(new EventGridEvent(), expected).ToList();
            // act
            await publisher.PublishAsync(events);

            // assert
            client.Verify(self => self.PublishEventsWithHttpMessagesAsync(It.IsAny<string>()
                , It.Is<IList<EventGridEvent>>(actual => actual.Count == expected)
                , It.IsAny<Dictionary<string, List<string>>>()
                , It.IsAny<CancellationToken>())
                , Times.Once);
        }

        //[Fact]
        //public async Task Should_add_all_properties_to_data_as_dictionary_when_a_publish_occurred() {
        //    // arrange

        //    var publisher = new CustomerJourneyEventPublisher(client.Object);

        //    // act
        //    await publisher.PublishAsync(new TestEvent());

        //    // assert
        //    client.Verify(async self => await self.PublishEventsAsync(It.IsAny<string>(),  ));
        //}

        //[Fact]
        //public async Task Should_publish_with_auto_generated_event_id_if_it_is_not_given()
        //{
        //    // arrange

        //    var publisher = new CustomerJourneyEventPublisher(client.Object);

        //    // act
        //    await publisher.PublishAsync(new TestEvent());

        //    // assert
        //    client.Verify(async self => await self.PublishEventsAsync(It.IsAny<string>(), ));
        //}



    }
}

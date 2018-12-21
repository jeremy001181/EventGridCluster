using Microsoft.Azure.KeyVault;
using Moq;
using EventGridCluster.ConfigStores;
using Xunit;

namespace EventGridCluster.Tests
{
    public class EventGridClusterBuilderTests
    {
        public EventGridClusterBuilderTests()
        {

        }

        [Fact]
        public void Should_create_a_publisher_with_secondary_eventgrid_endpoint()
        {
            // arrange
            var builder = new EventGridClusterBuilder(ConfigStore.FromValue("http://endpoint", "test"));

            // assert
            var actual = builder.AddSecondary(ConfigStore.FromValue("http://endpoint2", "test"))
                                .Build();

            Assert.NotNull(actual);
        }

    }
}

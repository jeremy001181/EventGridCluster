using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Rest.Azure;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EventGridCluster
{
    internal sealed class EventGridCluster : IEventGridCluster
    {
        private readonly IList<string> topicHosts = new List<string>();
        private IEventGridClient client;

        internal EventGridCluster(IEventGridClient client)
        {
            this.topicHosts = ((TopicCredentialSet) client.Credentials).TopicHosts;
            this.client = client;
        }

        public void Dispose()
        {
            client?.Dispose();
        }

        public async Task PublishAsync(IList<EventGridEvent> events, CancellationToken cancelationToken = default(CancellationToken))
        {
            await PublishRecusivelyAsync(0, events, cancelationToken);
        }

        private async Task PublishRecusivelyAsync(int index, IList<EventGridEvent> eventGridEvents, CancellationToken cancelationToken) {
            if (index >= topicHosts.Count) {
                return;
            }
            var next = index + 1;

            try
            {
                await client.PublishEventsAsync(topicHosts[index], eventGridEvents, cancelationToken);
            }
            catch (TaskCanceledException)
            {
                // Operation timeout
                //todo logging
                await PublishRecusivelyAsync(next, eventGridEvents, cancelationToken);
            }
            catch (CloudException)
            {
                // Operation returned an invalid status code(anything other than 200 OK), 
                // republish on secondary topic
                //todo logging
                await PublishRecusivelyAsync(next, eventGridEvents, cancelationToken);
            }
        }
    }
}

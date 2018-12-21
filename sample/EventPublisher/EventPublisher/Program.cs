using EventGridCluster;
using EventGridCluster.ConfigStores;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventPublisher
{
    // This captures the "Data" portion of an EventGridEvent on a custom topic
    class CustomerInteractionEvent
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            // TODO: Enter values for <topic-name> and <region>
            string topicEndpoint = "<topic-name>";

            // TODO: Enter value for <topic-key>
            string topicKey = "<topic-key>";


            string topicHostname = new Uri(topicEndpoint).Host;

            var cluster = new EventGridClusterBuilder(ConfigStore.FromValue(topicEndpoint, topicKey))
                .AddSecondary(ConfigStore.FromValue("<secondary1-topic-name>", "topic-key1"))
                .AddSecondary(ConfigStore.FromValue("<secondary2-topic-name>", "topic-key2"))
                .Build();
            for (int i = 0; i < 1; i++)
            {
                await cluster.PublishAsync(GetEventsList());
            }
            Console.Write($"Published events to Event Grid. at {DateTime.UtcNow.ToString("yyyyMMddHHmmssfff")}");
            Console.ReadLine();
        }

        static IList<EventGridEvent> GetEventsList()
        {
            List<EventGridEvent> eventsList = new List<EventGridEvent>();

            for (int i = 0; i < 1000; i++)
            {
                eventsList.Add(new EventGridEvent()
                {
                    Id = Guid.NewGuid().ToString(),
                    EventType = "Contoso.Items.ItemReceived",
                    Data = new CustomerInteractionEvent()
                    {
                        Id = Guid.NewGuid().ToString()
                    },
                    EventTime = DateTime.UtcNow,
                    Subject = "Door1",
                    DataVersion = "2.0"
                });
            }

            return eventsList;
        }
    }
}

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using EventGridCluster;
using EventGridCluster.ConfigStores;
using Microsoft.Azure.EventGrid.Models;

namespace FunctionApp1
{
    public static class Function1
    {
        private static readonly IEventGridCluster cluster;

        static Function1()
        {
            // TODO: Enter values for <topic-name> and <region>
            string topicEndpoint = "<topic-name>";

            // TODO: Enter value for <topic-key>
            string topicKey = "<topic-key>";
            
            cluster = new EventGridClusterBuilder(ConfigStore.FromValue(topicEndpoint, topicKey))                
                .Build();

        }
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            await cluster.PublishAsync(GetEventsList());
            
            return new OkResult();
        }
        static List<EventGridEvent> GetEventsList()
        {
            var eventsList = new List<EventGridEvent>();

            for (int i = 0; i < 1; i++)
            {
                eventsList.Add(new EventGridEvent()
                {
                    Data = "test",
                    Id = Guid.NewGuid().ToString(),
                    EventTime = DateTime.UtcNow,                   
                    
                });
            }

            return eventsList;
        }
    }
}

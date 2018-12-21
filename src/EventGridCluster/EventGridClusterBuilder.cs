using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.EventGrid;
using EventGridCluster.ConfigStores;

namespace EventGridCluster
{
    public class EventGridClusterBuilder : IEventGridClusterBuilder
    {
        private IList<IConfigStore> configStore = new List<IConfigStore>();

        public EventGridClusterBuilder(IConfigStore primary)
        {
            this.configStore.Add(primary);
        }

        public IEventGridClusterBuilder AddSecondary(IConfigStore secondary)
        {
            this.configStore.Add(secondary);
            return this;
        }

        public IEventGridCluster Build() {
            var topicCredentials = configStore
                .Select(store => store.GetConfig())                
                .ToDictionary(config => config.TopicHost, config => config.TopicKey);

            var topicCredientialSet = new TopicCredentialSet(topicCredentials);

            var publisher = new EventGridCluster(
                    new EventGridClient(topicCredientialSet));

            return publisher;
        }
    }
}

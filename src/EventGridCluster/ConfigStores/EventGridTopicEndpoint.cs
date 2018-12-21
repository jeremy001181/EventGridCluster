using System;

namespace EventGridCluster.ConfigStores
{
    public class EventGridTopicEndpoint
    {
        public EventGridTopicEndpoint(string topicEndpoint, string topicKey)
        {
            TopicHost = new Uri(topicEndpoint).Host;
            TopicKey = topicKey;
        }

        public string TopicHost { get; }
        public string TopicKey { get; }

    }
}

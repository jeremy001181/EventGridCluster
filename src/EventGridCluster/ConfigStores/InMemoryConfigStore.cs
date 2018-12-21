namespace EventGridCluster.ConfigStores
{
    class InMemoryConfigStore : IConfigStore
    {
        private readonly string topicEndpoint;
        private readonly string topicKey;

        public InMemoryConfigStore(string topicEndpoint, string topicKey)
        {
            this.topicEndpoint = topicEndpoint;
            this.topicKey = topicKey;
        }

        public EventGridTopicEndpoint GetConfig()
        {
            return new EventGridTopicEndpoint(topicEndpoint, topicKey);
        }

    }
}

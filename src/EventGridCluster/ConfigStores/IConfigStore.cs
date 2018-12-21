namespace EventGridCluster.ConfigStores
{
    public interface IConfigStore
    {
        EventGridTopicEndpoint GetConfig(); 
    }
}

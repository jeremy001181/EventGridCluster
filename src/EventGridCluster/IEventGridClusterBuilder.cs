using EventGridCluster.ConfigStores;

namespace EventGridCluster
{
    public interface IEventGridClusterBuilder
    {
        IEventGridClusterBuilder AddSecondary(IConfigStore configStore);
        IEventGridCluster Build();
    }
}
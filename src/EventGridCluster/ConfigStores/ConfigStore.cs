using Microsoft.Azure.KeyVault;

namespace EventGridCluster.ConfigStores
{
    public static class ConfigStore
    {
        public static IConfigStore FromKeyVault(IKeyVaultClient keyVaultClient)
        {
            return new KeyVaultConfigStore(keyVaultClient);
        }
        public static IConfigStore FromValue(string topic, string key)
        {
            return new InMemoryConfigStore(topic, key);
        }
    }
}

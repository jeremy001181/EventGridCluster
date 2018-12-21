using System;
using Microsoft.Azure.KeyVault;

namespace EventGridCluster.ConfigStores
{
    class KeyVaultConfigStore : IConfigStore
    {
        private IKeyVaultClient keyVaultClient;

        public KeyVaultConfigStore(IKeyVaultClient keyVaultClient)
        {
            this.keyVaultClient = keyVaultClient;
        }

        public EventGridTopicEndpoint GetConfig()
        {
            throw new NotImplementedException();
        }

    }
}

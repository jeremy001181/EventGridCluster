using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Rest;

namespace EventGridCluster
{
    internal class TopicCredentialSet : ServiceClientCredentials {
        private readonly IDictionary<string, string> topicCredentials;
        private const string TOPIC_KEY_HEADER_NAME = "aeg-sas-key";

        internal IList<string> TopicHosts => topicCredentials.Keys.ToList();

        internal TopicCredentialSet(IDictionary<string, string> topicCredentials)
        {
            if (topicCredentials == null || topicCredentials.Count == 0) {
                throw new ArgumentException("No topic endpoint is given", nameof(topicCredentials));
            }

            this.topicCredentials = topicCredentials;
        }
        public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        { 
            request.Headers.Add(TOPIC_KEY_HEADER_NAME, topicCredentials[request.RequestUri.Host]);

            return base.ProcessHttpRequestAsync(request, cancellationToken);
        }
    }
}

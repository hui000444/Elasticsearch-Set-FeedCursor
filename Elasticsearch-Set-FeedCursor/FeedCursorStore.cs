using System.Collections.Generic;
using System.Net;
using Nest;

namespace Elasticsearch.Set.FeedCursor
{
    public class FeedCursorStore
    {
        private readonly string indexName;
        private readonly string feedType;
        private readonly ElasticClient client;

        public FeedCursorStore(IEnumerable<string> baseAddresses, string indexName, string feedType)
        {
            this.indexName = indexName;
            this.feedType = feedType;
            client = ElasticClientFactory.Create(baseAddresses);
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        }

        public void Set(FeedCursor cursor)
        {
            client.Index(cursor, d => d.Index(indexName).Type("feed_cursor").Id(feedType).Refresh());
        }

        public FeedCursor Get()
        {
            return client.Get<FeedCursor>(g => g.Index(indexName).Type("feed_cursor").Id(feedType)).Source;
        }

        public void Delete()
        {
            client.Delete<FeedCursor>(d => d.Index(indexName).Type("feed_cursor").Id(feedType));
        }

    }
}

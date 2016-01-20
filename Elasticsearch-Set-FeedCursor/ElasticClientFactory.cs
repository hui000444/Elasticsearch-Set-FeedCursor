using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.ConnectionPool;
using Nest;

namespace Elasticsearch.Set.FeedCursor
{
    public class ElasticClientFactory
    {
        public static ElasticClient Create(IEnumerable<string> addresses)
        {
            var uris = addresses.Select(address => new Uri(address));
            var connectionPool = new StaticConnectionPool(uris);
            var settings = new ConnectionSettings(connectionPool);
            settings.MaximumRetries(3);
            settings.SetPingTimeout(500);
            return new ElasticClient(settings);
        }
    }
}

using System;
using System.Collections.Specialized;
using System.Configuration;

namespace Elasticsearch.Set.FeedCursor
{
    public static class EventStoreConfiugration
    {
        private static readonly NameValueCollection appSettings = ConfigurationManager.AppSettings;

        public static string EventStoreApiUri
        {
            get { return appSettings["EventStoreApiUri"]; }
        }

        public static long StreamPageSize
        {
            get { return Int64.Parse(appSettings["PageSize"]); }
        }

    }
}
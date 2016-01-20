using System;
using System.Configuration;

namespace Elasticsearch.Set.FeedCursor
{
    public static class Config
    {
        public static string[] SearchAddresses
        {
            get
            {
                var addresses = ConfigurationManager.AppSettings["SearchAddress"];
                if (string.IsNullOrWhiteSpace(addresses))
                    return new string[0];
                return addresses.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        public static string Env
        {
            get
            {
                return ConfigurationManager.AppSettings["Env"];
            }
        }
    }
}
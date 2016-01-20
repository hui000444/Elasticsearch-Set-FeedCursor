using System;
using System.Linq;

namespace Elasticsearch.Set.FeedCursor
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ElasticTypeExtAttribute : Attribute
    {
        private readonly int revision;
        private readonly string alias;

        public ElasticTypeExtAttribute(string alias, int revision)
        {
            this.revision = revision;
            this.alias = alias;
        }
        public string GetIndexFullName(string env)
        {
            var envSuffix = string.IsNullOrEmpty(env) ? "" : "_" + env;
            return alias + envSuffix + "_v" + revision;
        }

        public string GetAliasFullName(string env)
        {
            var envSuffix = string.IsNullOrEmpty(env) ? "" : "_" + env;
            return alias + envSuffix;
        }
    }

    public class Settings
    {
        private readonly string env;

        public Settings(string env)
        {
            this.env = env;
        }

        public string GetIndexFullName<T>()
        {
            var attr = (ElasticTypeExtAttribute)typeof(T).GetCustomAttributes(typeof(ElasticTypeExtAttribute), false).Single();
            return attr.GetIndexFullName(env);
        }

        public string GetAliasFullName<T>()
        {
            var attr = (ElasticTypeExtAttribute)typeof(T).GetCustomAttributes(typeof(ElasticTypeExtAttribute), false).Single();
            return attr.GetAliasFullName(env);
        }
    }
}
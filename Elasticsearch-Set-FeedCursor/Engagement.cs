using Nest;

namespace Elasticsearch.Set.FeedCursor
{
    [ElasticType(Name = "engagements")]
    [ElasticTypeExt("engagements", 12)]
    public class Engagement : IIndex
    {
        [ElasticProperty(IncludeInAll = false)]
        public long Id { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string Name { get; set; }
        
        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string ClientName { get; set; }
        
        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string ClientShortName { get; set; }
        
        // need ingorecase searching
        public string PracticeType { get; set; }

        public override string ToString()
        {
            return string.Format("Engagement[{0}]", Id);
        }

        public void AdditionalMapping<T>(PutMappingDescriptor<T> descriptor) where T : class
        {

        }
    }
}
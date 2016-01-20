using Nest;

namespace Elasticsearch.Set.FeedCursor
{
    public interface IIndex
    {
        long Id { set; get; }
        void AdditionalMapping<T>(PutMappingDescriptor<T> descriptor) where T : class;
    }


    [ElasticType(Name = "workrecords")]
    [ElasticTypeExt("workrecords", 12)]
    public class WorkRecord : IIndex
    {
        [ElasticProperty(IncludeInAll = false)]
        public long Id { get; set; }

        public long AssigneeId { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string AssigneeFirstName { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string AssigneeLastName { get; set; }

        [ElasticProperty(IncludeInAll = false)]
        public long EngagementId { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string EngagementName { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string Name { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string Country { get; set; }

        public int Year { get; set; }

        // need ingorecase searching
        public string PracticeType { get; set; }

        public long[] ResponsibleOfficeIds { get; set; }

        public void AdditionalMapping<T>(PutMappingDescriptor<T> descriptor) where T : class
        {
            (descriptor as PutMappingDescriptor<WorkRecord>)
                .Properties(props => props

                    // for assignee firstname, original field is not analyzed, and assignee firstname.sort field using lowercase analyzer
                    .MultiField(mf => mf
                        .Name(_ => _.AssigneeFirstName)
                        .Fields(fs => fs
                            .String(s => s.Name(_ => _.AssigneeFirstName).Index(FieldIndexOption.NotAnalyzed))
                            .String(s => s.Name(_ => _.AssigneeFirstName.Suffix("sort")).Analyzer("lowercase"))
                        ))

                    // for assignee lastname same rules with assignee firstname
                    .MultiField(mf => mf
                        .Name(_ => _.AssigneeLastName)
                        .Fields(fs => fs
                            .String(_ => _.Name(t => t.AssigneeLastName).Index(FieldIndexOption.NotAnalyzed))
                            .String(_ => _.Name(t => t.AssigneeLastName.Suffix("sort")).Analyzer("lowercase"))
                        ))
                );
        }
    }
}
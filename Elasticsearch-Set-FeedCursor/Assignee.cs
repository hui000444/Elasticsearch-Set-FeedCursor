using Nest;

namespace Elasticsearch.Set.FeedCursor
{
    [ElasticType(Name = "assignees")]
    [ElasticTypeExt("assignees", 17)]
    public class Assignee : IIndex
    {
        public long Id { set; get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PrimaryEmail { get; set; }
        public string SecondaryEmail { get; set; }

        public ClientAssociation[] ClientAssociations { get; set; }

        public Engagement[] Engagements { get; set; }

        [ElasticProperty(IncludeInAll = false, Type= FieldType.Integer)]
        public AssigneeStatus Status { get; set; }

        public class Engagement
        {
            [ElasticProperty(IncludeInAll = false)]
            public long Id { get; set; }
            [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
            public string Name { get; set; }
        }

        public class ClientAssociation
        {
            [ElasticProperty(IncludeInAll = false)]
            public long Id { get; set; }

            [ElasticProperty(IncludeInAll = false)]
            public string Name { get; set; }

            [ElasticProperty(IncludeInAll = false, Type = FieldType.String, IndexAnalyzer = "lowercase")]
            public string EmployeeId { get; set; }
        }

        public override string ToString()
        {
            return string.Format("Assignee[{0}]", Id);
        }

        public void AdditionalMapping<T>(PutMappingDescriptor<T> descriptor) where T : class
        {
            (descriptor as PutMappingDescriptor<Assignee>)
                .Properties(props => props

                    // for firstname, firstname.sort field using lowercase analyzer
                    .MultiField(mf => mf
                        .Name(_ => _.FirstName)
                        .Fields(fs => fs
                            .String(s => s.Name(_ => _.FirstName).Index(FieldIndexOption.Analyzed))
                            .String(s => s.Name(_ => _.FirstName.Suffix("sort")).Analyzer("lowercase"))
                        ))

                    // for lastname same rules with firstname
                    .MultiField(mf => mf
                        .Name(_ => _.LastName)
                        .Fields(fs => fs
                            .String(s => s.Name(_ => _.LastName).Index(FieldIndexOption.Analyzed))
                            .String(s => s.Name(_ => _.LastName.Suffix("sort")).Analyzer("lowercase"))
                        ))
                    );
        }

        public enum AssigneeStatus
        {
            Inactive = 0,
            Active = 1,
            InactiveGOMigration = 2,
            Pending = 3
        }
    }

   
}

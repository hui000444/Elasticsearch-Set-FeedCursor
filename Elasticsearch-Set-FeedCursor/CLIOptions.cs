using CommandLine;

namespace Elasticsearch.Set.FeedCursor
{
    public class CLIOptions
    {
        [Option('t', "optiontype", Required = true, HelpText = "read/update/delete")]
        public string Type { get; set; }

        [Option('i', "indextype", HelpText = "index type(assignee/engagement/workrecord)")]
        public string IndexType { get; set; }

        [Option('c', "cursortype", HelpText = "feed cursor type(engagement_feed_event_store/assignee_feed_event_store/workrecord_feed_event_store)")]
        public string CursorType { get; set; }

        [Option('u', "cursorurl", HelpText = "cursor url(streams/$ce-engagement/0/forward/500)")]
        public string CursorUrl { get; set; }

        [Option("debug", HelpText = "Enable debug mode")]
        public bool DebugMode { get; set; }


    }
}
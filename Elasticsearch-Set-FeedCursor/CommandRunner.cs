using System;
using System.Diagnostics;

namespace Elasticsearch.Set.FeedCursor
{
    internal class CommandRunner
    {
        private readonly CLIOptions options;
        private const string engagementFeedEventStore = "engagement_feed_event_store";
        private const string assigneeFeedEventStore = "assignee_feed_event_store";
        private const string workrecordFeedEventStore = "workrecord_feed_event_store";

        public CommandRunner(CLIOptions options)
        {
            this.options = options;
        }

        internal void Run()
        {
            if (options.DebugMode)
            {
                Debugger.Launch();
            }

            switch (options.Type.ToLower())
            {
                case "read":
                    ReadFeedCursor<Engagement>(engagementFeedEventStore);
                    ReadFeedCursor<Assignee>(assigneeFeedEventStore);
                    ReadFeedCursor<Assignee>(engagementFeedEventStore);
                    ReadFeedCursor<WorkRecord>(workrecordFeedEventStore);
                    ReadFeedCursor<WorkRecord>(assigneeFeedEventStore);
                    ReadFeedCursor<WorkRecord>(engagementFeedEventStore);
                    break;
                case "delete":
                    DeleteFeedCursor<Engagement>(engagementFeedEventStore);
                    DeleteFeedCursor<Assignee>(assigneeFeedEventStore);
                    DeleteFeedCursor<Assignee>(engagementFeedEventStore);
                    DeleteFeedCursor<WorkRecord>(assigneeFeedEventStore);
                    DeleteFeedCursor<WorkRecord>(engagementFeedEventStore);
                    DeleteFeedCursor<WorkRecord>(workrecordFeedEventStore);
                    break;
                case "update":
                    if (options.IndexType.ToLower() == "assignee")
                        SetFeedCursor<Assignee>(options.CursorType, options.CursorUrl);
                    else if (options.IndexType.ToLower() == "engagement")
                        SetFeedCursor<Engagement>(options.CursorType, options.CursorUrl);
                    else if (options.IndexType.ToLower() == "workrecord")
                        SetFeedCursor<WorkRecord>(options.CursorType, options.CursorUrl);
                    break;
            }

        }


        private static void DeleteFeedCursor<T>(string feedType)
        {
            var settings = new Settings(Config.Env);

            var indexFullName = settings.GetIndexFullName<T>();
            var feedCursorStore = new FeedCursorStore(
                Config.SearchAddresses,
                indexFullName,
                feedType);

            feedCursorStore.Delete();
            Console.WriteLine("**Successfully Delete Feed Cursor {0} -> {1}", indexFullName, feedType);
        }
        private static void SetFeedCursor<T>(string feedType, string initialFeedCursor)
        {
            var settings = new Settings(Config.Env);
            var indexFullName = settings.GetIndexFullName<T>();
            Console.WriteLine("**Change Feed Cursor {0} -> {1} ", indexFullName, feedType);
            Console.WriteLine("**Initial URL: {0} ", initialFeedCursor);

            var feedCursorStore = new FeedCursorStore(
                Config.SearchAddresses,
                indexFullName,
                feedType);
            var cursor = new FeedCursor
            {
                Uri = string.Format("{0}/{1}", EventStoreConfiugration.EventStoreApiUri, initialFeedCursor)
            };

            feedCursorStore.Set(cursor);
            Console.WriteLine("Successfully!!!");
        }
        private static void ReadFeedCursor<T>(string feedType)
        {
            var settings = new Settings(Config.Env);

            var indexFullName = settings.GetIndexFullName<T>();
            var feedCursorStore = new FeedCursorStore(
                Config.SearchAddresses,
                indexFullName,
                feedType);

            var feedCursor = feedCursorStore.Get();
            Console.WriteLine("{0} -> {1} : {2}", indexFullName, feedType, feedCursor == null ? "no" : feedCursor.Uri);
        }
    }


}
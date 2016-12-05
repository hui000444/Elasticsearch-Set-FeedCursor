# Elasticsearch-Set-FeedCursor
.\Elasticsearch.Set.FeedCursor.exe -t update -i engagement -c engagement_feed_event_store -u streams/$ce-engagement/XXX/forward/500

.\Elasticsearch.Set.FeedCursor.exe -t update -i assignee -c engagement_feed_event_store -u streams/$ce-engagement/XXX/forward/500

.\Elasticsearch.Set.FeedCursor.exe -t update -i assignee -c assignee_feed_event_store -u streams/$ce-assignee/XXX/forward/500

.\Elasticsearch.Set.FeedCursor.exe -t update -i workrecord -c engagement_feed_event_store -u streams/$ce-engagement/XXX/forward/500

.\Elasticsearch.Set.FeedCursor.exe -t update -i workrecord -c assignee_feed_event_store -u streams/$ce-assignee/XXX/forward/500

.\Elasticsearch.Set.FeedCursor.exe -t update -i workrecord -c workrecord_feed_event_store -u streams/$ce-workrecord/XXX/forward/500

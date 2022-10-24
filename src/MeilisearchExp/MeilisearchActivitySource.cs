using System;
using System.Diagnostics;
using MeilisearchExp.Internal;

namespace MeilisearchExp
{
    static class MeilisearchActivitySource
    {
        static readonly ActivitySource Source;

        static MeilisearchActivitySource()
        {
            Source = new ActivitySource(MeilisearchConstants.ClientName, MeilisearchConstants.ClientVersion);
        }
        
        internal static bool IsEnabled => Source.HasListeners();

        internal static Activity RequestStart(ClientConfig config)
        {
            var activity = Source.StartActivity("Meilisearch", ActivityKind.Client);
            
            activity.SetTag("db.system", "Meilisearch");
            activity.SetTag("db.connection_string", config.Client.BaseAddress);
            activity.SetTag("db.user", config.AuthenticationHeader == null ? "anonymous" : "apikey");
            activity.SetTag("db.meilisearch.useragent", config.UserAgentHeader.ToString());
            return activity;
        }

        internal static void ReceivedFirstResponse(Activity activity)
        {
            var activityEvent = new ActivityEvent("received-first-response");
            activity.AddEvent(activityEvent);
        }
        
        internal static void RequestStop(Activity activity)
        {
            activity.SetTag("otel.status_code", "OK");
            activity.Dispose();
        }
        
        internal static void SetException(Activity activity, Exception ex, bool escaped = true)
        {
            var tags = new ActivityTagsCollection
            {
                { "exception.type", ex.GetType().FullName },
                { "exception.message", ex.Message },
                { "exception.stacktrace", ex.ToString() },
                { "exception.escaped", escaped }
            };
            var activityEvent = new ActivityEvent("exception", tags: tags);
            activity.AddEvent(activityEvent);
            activity.SetTag("otel.status_code", "ERROR");
            // TODO: Improve with a test if Meilisearch exception
            activity.SetTag("otel.status_description", ex.Message);
            activity.Dispose();
        }
    }
}
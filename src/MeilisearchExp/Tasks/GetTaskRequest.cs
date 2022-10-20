using System.Diagnostics;
using System.Net.Http;
using MeilisearchExp.Indexes;
using MeilisearchExp.Internal;

namespace MeilisearchExp.Tasks
{
    public class GetTaskRequest : BaseRequest<TaskResource>
    {
        private readonly int _taskUid;

        internal GetTaskRequest(ClientConfig clientConfig, int taskUid) : base(clientConfig)
        {
            _taskUid = taskUid;
        }

        protected override HttpRequestMessage CreateHttpRequestMessage()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"tasks/{_taskUid}");
            return requestMessage;
        }

        protected override void AddOtelTags(Activity activity)
        {
            activity.SetTag("db.operation", "getTask");
            activity.SetTag("db.meilisearch.route", "/tasks/{task_uid}");
            activity.SetTag("db.meilisearch.verb", HttpMethod.Get);
            activity.SetTag("db.meilisearch.task_uid", _taskUid);
        }
    }
}
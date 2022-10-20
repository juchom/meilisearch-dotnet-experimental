using System.Diagnostics;
using System.Net.Http;
using MeilisearchExp.Internal;

namespace MeilisearchExp.Indexes
{
    public class DeleteIndexRequest : BaseRequest<TaskInfo>
    {
        private readonly string _indexUid;

        internal DeleteIndexRequest(ClientConfig clientConfig, string indexUid)
            : base(clientConfig)
        {
            _indexUid = indexUid;
        }

        protected override HttpRequestMessage CreateHttpRequestMessage()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, $"indexes/{_indexUid}");
            return requestMessage;
        }

        protected override void AddOtelTags(Activity activity)
        {
            activity.SetTag("db.operation", "deleteIndex");
            activity.SetTag("db.meilisearch.route", "/indexes/{index_uid}");
            activity.SetTag("db.meilisearch.index_uid", _indexUid);
            activity.SetTag("db.meilisearch.verb", HttpMethod.Delete);
        }
    }
}
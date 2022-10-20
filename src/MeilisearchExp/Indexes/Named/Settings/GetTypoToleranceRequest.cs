using System.Diagnostics;
using System.Net.Http;
using MeilisearchExp.Internal;

namespace MeilisearchExp.Indexes
{
    public class GetTypoToleranceRequest : BaseRequest<TypoTolerance>
    {
        private readonly string _indexUid;

        internal GetTypoToleranceRequest(ClientConfig clientConfig, string indexUid) : base(clientConfig)
        {
            _indexUid = indexUid;
        }

        protected override HttpRequestMessage CreateHttpRequestMessage()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"indexes/{_indexUid}/settings/typo-tolerance");
            return requestMessage;
        }

        protected override void AddOtelTags(Activity activity)
        {
            activity.SetTag("db.operation", "getTypoTolerance");
            activity.SetTag("db.meilisearch.route", "/indexes/{index_uid}/settings/typo-tolerance");
            activity.SetTag("db.meilisearch.index_uid", _indexUid);
            activity.SetTag("db.meilisearch.verb", HttpMethod.Get);
        }
    }
}
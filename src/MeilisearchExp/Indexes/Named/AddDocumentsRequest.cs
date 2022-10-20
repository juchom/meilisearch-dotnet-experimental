using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using MeilisearchExp.Internal;

namespace MeilisearchExp.Indexes
{
    public class AddDocumentsRequest<T> : BaseRequest<TaskInfo>
    {
        private readonly IEnumerable<T> _documents;
        private readonly string _indexUid;
        private readonly string _primaryKey;

        internal AddDocumentsRequest(ClientConfig clientConfig, IEnumerable<T> documents, string indexUid, string primaryKey = default)
            : base(clientConfig)
        {
            _documents = documents;
            _indexUid = indexUid;
            _primaryKey = primaryKey;
        }

        protected override HttpRequestMessage CreateHttpRequestMessage()
        {
            var uri = $"indexes/{_indexUid}/documents";
            
            if (_primaryKey != default)
            {
                uri = $"{uri}?primaryKey={_primaryKey}";
            }
            
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            var payload = JsonSerializer.Serialize(_documents, ClientConfig.JsonSerializerOptions);
            requestMessage.Content = new StringContent(payload);
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(ContentType.Json);

            return requestMessage;
        }

        protected override void AddOtelTags(Activity activity)
        {
            activity.SetTag("db.operation", "addDocuments");
            activity.SetTag("db.meilisearch.route", "/indexes/{index_uid}/documents");
            activity.SetTag("db.meilisearch.index_uid", _indexUid);
            activity.SetTag("db.meilisearch.verb", HttpMethod.Post);

            if (!string.IsNullOrEmpty(_primaryKey))
                activity.SetTag("db.meilisearch.index_primary_key", _primaryKey);
        }
    }
}
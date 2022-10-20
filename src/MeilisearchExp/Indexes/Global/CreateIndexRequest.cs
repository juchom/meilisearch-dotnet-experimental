using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using MeilisearchExp.Internal;

namespace MeilisearchExp.Indexes.Global
{
    public class CreateIndexRequest : BaseRequest<TaskInfo>
    {
        private readonly CreateIndexModel _createIndexModel;

        internal CreateIndexRequest(ClientConfig clientConfig, string indexUid) : base(clientConfig)
        {
            _createIndexModel = new CreateIndexModel(indexUid);
        }
        
        public CreateIndexRequest WithPrimaryKey(string primaryKey)
        {
            _createIndexModel.PrimaryKey = primaryKey;
            return this;
        }

        protected override HttpRequestMessage CreateHttpRequestMessage()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "indexes");
            var payload = JsonSerializer.Serialize(_createIndexModel);
            requestMessage.Content = new StringContent(payload);
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(ContentType.Json);

            return requestMessage;
        }

        protected override void AddOtelTags(Activity activity)
        {
            activity.SetTag("db.operation", "createIndex");
            activity.SetTag("db.meilisearch.route", "/indexes");
            activity.SetTag("db.meilisearch.index_uid", _createIndexModel.IndexUid);
            activity.SetTag("db.meilisearch.verb", HttpMethod.Post);

            if (!string.IsNullOrEmpty(_createIndexModel.PrimaryKey))
                activity.SetTag("db.meilisearch.index_primary_key", _createIndexModel.PrimaryKey);
        }
    }
    
    internal class CreateIndexModel
    {
        [JsonPropertyName("uid")]
        public string IndexUid { get; }
        
        [JsonPropertyName("primaryKey")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string PrimaryKey { get; internal set; }
        
        internal CreateIndexModel(string indexUid)
        {
            IndexUid = indexUid;
        }
    }
}
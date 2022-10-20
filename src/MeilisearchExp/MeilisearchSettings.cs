using System.Text.Json;
using System.Text.Json.Serialization;

namespace MeilisearchExp
{
    public class MeilisearchSettings
    {
        public string BaseUrl { get; }
        public string ApiKey { get; }
        public JsonSerializerOptions JsonSerializerOptions { get; }
        public MeilisearchSettings(string baseUrl, string apiKey = "", JsonSerializerOptions jsonSerializerOptions = null)
        {
            BaseUrl = baseUrl;
            ApiKey = apiKey;
            JsonSerializerOptions = jsonSerializerOptions;
        }
    }
}
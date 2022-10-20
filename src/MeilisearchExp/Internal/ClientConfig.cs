using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MeilisearchExp.Internal
{
    internal class ClientConfig
    {
        internal HttpClient Client { get; }
        internal AuthenticationHeaderValue AuthenticationHeader { get; }
        internal ProductInfoHeaderValue UserAgentHeader { get; }
        internal JsonSerializerOptions JsonSerializerOptions { get; }
        
        internal ClientConfig(HttpClient client, MeilisearchSettings settings)
        {
            client.BaseAddress = new Uri(settings.BaseUrl);
            Client = client;
            
            if (!string.IsNullOrEmpty(settings.ApiKey))
            {
                AuthenticationHeader = new AuthenticationHeaderValue("Bearer", settings.ApiKey);
            }

            UserAgentHeader = new ProductInfoHeaderValue(new ProductHeaderValue(MeilisearchConstants.ClientName, MeilisearchConstants.ClientVersion));
            
            JsonSerializerOptions = settings.JsonSerializerOptions ?? new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.Never,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }
    }
}
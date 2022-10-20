using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MeilisearchExp
{
    internal class SearchQuery
    {
        internal SearchQuery(string query)
        {
            Query = query;
        }
        
        [JsonPropertyName("q")]
        public string Query { get; }
        
        [JsonPropertyName("offset")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Offset { get; set; }

        [JsonPropertyName("limit")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Limit { get; set; }

        [JsonPropertyName("filter")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<string> Filter { get; set; }

        [JsonPropertyName("attributesToRetrieve")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<string> AttributesToRetrieve { get; set; }

        [JsonPropertyName("attributesToCrop")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<string> AttributesToCrop { get; set; }

        [JsonPropertyName("cropLength")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? CropLength { get; set; }

        [JsonPropertyName("attributesToHighlight")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<string> AttributesToHighlight { get; set; }

        [JsonPropertyName("cropMarker")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string CropMarker { get; set; }

        [JsonPropertyName("highlightPreTag")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string HighlightPreTag { get; set; }

        [JsonPropertyName("highlightPostTag")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string HighlightPostTag { get; set; }

        [JsonPropertyName("facets")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<string> Facets { get; set; }

        [JsonPropertyName("showMatchesPosition")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? ShowMatchesPosition { get; set; }

        [JsonPropertyName("sort")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<string> Sort { get; set; }

        [JsonPropertyName("matchingStrategy")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string MatchingStrategy { get; set; }
    }
}
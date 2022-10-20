using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MeilisearchExp
{
    public class SearchResult<T>
    {
        public SearchResult(IReadOnlyCollection<T> hits, int offset, int limit, int estimatedTotalHits,
            IReadOnlyDictionary<string, IReadOnlyDictionary<string, int>> facetDistribution,
            int processingTimeMs, string query,
            IReadOnlyDictionary<string, IReadOnlyCollection<MatchPosition>> matchesPosition)
        {
            Hits = hits;
            Offset = offset;
            Limit = limit;
            EstimatedTotalHits = estimatedTotalHits;
            FacetDistribution = facetDistribution;
            ProcessingTimeMs = processingTimeMs;
            Query = query;
            MatchesPosition = matchesPosition;
        }

        [JsonPropertyName("hits")]
        public IReadOnlyCollection<T> Hits { get; }

        [JsonPropertyName("offset")]
        public int Offset { get; }

        [JsonPropertyName("limit")]
        public int Limit { get; }

        [JsonPropertyName("estimatedTotalHits")]
        public int EstimatedTotalHits { get; }

        [JsonPropertyName("facetDistribution")]
        public IReadOnlyDictionary<string, IReadOnlyDictionary<string, int>> FacetDistribution { get; }

        [JsonPropertyName("processingTimeMs")]
        public int ProcessingTimeMs { get; }

        [JsonPropertyName("query")]
        public string Query { get; }

        [JsonPropertyName("_matchesPosition")]
        public IReadOnlyDictionary<string, IReadOnlyCollection<MatchPosition>> MatchesPosition { get; }
    }

    public class MatchPosition
    {
        public MatchPosition(int start, int length)
        {
            Start = start;
            Length = length;
        }

        [JsonPropertyName("start")]
        public int Start { get; }

        [JsonPropertyName("length")]
        public int Length { get; }
    }
}
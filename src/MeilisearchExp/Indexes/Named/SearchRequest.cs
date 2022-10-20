using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using MeilisearchExp.Internal;

namespace MeilisearchExp.Indexes
{
    public class SearchRequest<T> : BaseRequest<SearchResult<T>>
    {
        private readonly string _indexUid;
        private readonly SearchQuery _query;

        internal SearchRequest(ClientConfig clientConfig, string query, string indexUid) : base(clientConfig)
        {
            _indexUid = indexUid;
            _query = new SearchQuery(query);
        }

        public SearchRequest<T> WithOffset(int? offset)
        {
            _query.Offset = offset;
            return this;
        }
        
        public SearchRequest<T> WithLimit(int? limit)
        {
            _query.Limit = limit;
            return this;
        }
        
        public SearchRequest<T> WithFilters(IEnumerable<string> filters)
        {
            _query.Filter = filters;
            return this;
        }
        
        public SearchRequest<T> WithAttributesToRetrieve(IEnumerable<string> attributesToRetrieve)
        {
            _query.AttributesToRetrieve = attributesToRetrieve;
            return this;
        }
        
        public SearchRequest<T> WithAttributesToCrop(IEnumerable<string> attributesToCrop)
        {
            _query.AttributesToCrop = attributesToCrop;
            return this;
        }
        
        public SearchRequest<T> WithCropLength(int? cropLength)
        {
            _query.CropLength = cropLength;
            return this;
        }
        
        public SearchRequest<T> WithAttributesToHighlight(IEnumerable<string> attributesToHighlight)
        {
            _query.AttributesToHighlight = attributesToHighlight;
            return this;
        }
        
        public SearchRequest<T> WithCropMarker(string cropMarker)
        {
            _query.CropMarker = cropMarker;
            return this;
        }

        public SearchRequest<T> WithHighlightPreTag(string highlightPreTag)
        {
            _query.HighlightPreTag = highlightPreTag;
            return this;
        }
        
        public SearchRequest<T> WithHighlightPostTag(string highlightPostTag)
        {
            _query.HighlightPostTag = highlightPostTag;
            return this;
        }
        
        public SearchRequest<T> WithFacets(IEnumerable<string> facets)
        {
            _query.Facets = facets;
            return this;
        }
        
        public SearchRequest<T> WithShowMatchesPosition(bool? showMatchesPosition)
        {
            _query.ShowMatchesPosition = showMatchesPosition;
            return this;
        }
        
        public SearchRequest<T> WithSort(IEnumerable<string> sort)
        {
            _query.Sort = sort;
            return this;
        }
        
        public SearchRequest<T> WithMatchingStrategy(string matchingStrategy)
        {
            _query.MatchingStrategy = matchingStrategy;
            return this;
        }

        protected override HttpRequestMessage CreateHttpRequestMessage()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"indexes/{_indexUid}/search");
            var payload = JsonSerializer.Serialize(_query);
            requestMessage.Content = new StringContent(payload);
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(ContentType.Json);

            return requestMessage;
        }

        protected override void AddOtelTags(Activity activity)
        {
            activity.SetTag("db.operation", "searchIndex");
            activity.SetTag("db.meilisearch.route", "/indexes/{index_uid}/search");
            activity.SetTag("db.meilisearch.index_uid", _indexUid);
            activity.SetTag("db.meilisearch.verb", HttpMethod.Post);
            
            activity.SetTag("db.meilisearch.query", _query.Query);

            if (_query.Offset.HasValue)
                activity.SetTag("db.meilisearch.offset", _query.Offset);
            
            if (_query.Limit.HasValue)
                activity.SetTag("db.meilisearch.limit", _query.Limit);

            if (_query.Filter?.Any() ?? false)
                activity.SetTag("db.meilisearch.filter", _query.Filter);
            
            if (_query.AttributesToRetrieve?.Any() ?? false)
                activity.SetTag("db.meilisearch.attributes_to_retrieve", _query.AttributesToRetrieve);
            
            if (_query.AttributesToCrop?.Any() ?? false)
                activity.SetTag("db.meilisearch.attributes_to_crop", _query.AttributesToCrop);

            if (_query.CropLength.HasValue)
                activity.SetTag("db.meilisearch.crop_length", _query.CropLength);
            
            if (_query.AttributesToHighlight?.Any() ?? false)
                activity.SetTag("db.meilisearch.attributes_to_highlight", _query.AttributesToHighlight);
        
            if (!string.IsNullOrEmpty(_query.CropMarker))
                activity.SetTag("db.meilisearch.crop_marker", _query.CropMarker);

            if (!string.IsNullOrEmpty(_query.HighlightPreTag))
                activity.SetTag("db.meilisearch.highlight_pre_tag", _query.HighlightPreTag);
            
            if (!string.IsNullOrEmpty(_query.HighlightPostTag))
                activity.SetTag("db.meilisearch.highlight_post_tag", _query.HighlightPostTag);
            
            if (_query.Facets?.Any() ?? false)
                activity.SetTag("db.meilisearch.facets", _query.Facets);
            
            if (_query.ShowMatchesPosition.HasValue)
                activity.SetTag("db.meilisearch.show_matches_position", _query.ShowMatchesPosition);
        
            if (_query.Sort?.Any() ?? false)
                activity.SetTag("db.meilisearch.sort", _query.Sort);
        
            if (!string.IsNullOrEmpty(_query.MatchingStrategy))
                activity.SetTag("db.meilisearch.matching_strategy", _query.MatchingStrategy);
        }
    }
}
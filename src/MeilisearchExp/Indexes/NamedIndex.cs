using MeilisearchExp.Indexes;
using MeilisearchExp.Internal;

namespace MeilisearchExp
{
    public class NamedIndex
    {
        private readonly ClientConfig _clientConfig;
        private readonly string _indexUid;

        internal NamedIndex(ClientConfig clientConfigConfig, string indexUid)
        {
            _indexUid = indexUid;
            _clientConfig = clientConfigConfig;
        }

        public SearchRequest<T> Search<T>(string query)
        {
            return new SearchRequest<T>(_clientConfig, query, _indexUid);
        }

        private DocumentsManager _documents;
        public DocumentsManager Documents => _documents ?? (_documents = new DocumentsManager(_clientConfig, _indexUid));

        private SettingsManager _settings;
        public SettingsManager Settings => _settings ?? (_settings = new SettingsManager(_clientConfig, _indexUid));

        
        public DeleteIndexRequest Delete()
        {
            return new DeleteIndexRequest(_clientConfig, _indexUid);
        }
    }
}
using System.Collections.Generic;
using MeilisearchExp.Internal;

namespace MeilisearchExp.Indexes
{
    public class DocumentsManager
    {
        private readonly ClientConfig _clientConfig;
        private readonly string _indexUid;

        internal DocumentsManager(ClientConfig clientConfig, string indexUid)
        {
            _clientConfig = clientConfig;
            _indexUid = indexUid;
        }

        public AddDocumentsRequest<T> AddDocuments<T>(IEnumerable<T> documents, string primaryKey = default)
        {
            return new AddDocumentsRequest<T>(_clientConfig, documents, _indexUid, primaryKey);
        }
    }
}
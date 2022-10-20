using MeilisearchExp.Indexes.Global;
using MeilisearchExp.Internal;
using MeilisearchExp.Tasks;

namespace MeilisearchExp
{
    public class GlobalIndexes
    {
        private readonly ClientConfig _clientConfig;
        private readonly TasksManager _tasksManager;

        internal GlobalIndexes(ClientConfig clientConfig)
        {
            _clientConfig = clientConfig;
        }

        public CreateIndexRequest CreateIndex(string indexUid)
        {
            return new CreateIndexRequest(_clientConfig, indexUid);
        }
    }
}
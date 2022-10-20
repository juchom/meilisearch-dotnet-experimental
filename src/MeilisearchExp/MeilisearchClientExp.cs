using System;
using System.Net.Http;
using MeilisearchExp.Internal;
using MeilisearchExp.Tasks;

namespace MeilisearchExp
{
    public class MeilisearchClientExp
    {
        private ClientConfig _clientConfig;

        public MeilisearchClientExp(MeilisearchSettings settings) :
            this(new HttpClient(), settings)
        {
        }

        public MeilisearchClientExp(HttpClient client, MeilisearchSettings settings)
        {
            _ = client ?? throw new ArgumentNullException(nameof(client));
            _ = settings ?? throw new ArgumentNullException(nameof(settings));

            _clientConfig = new ClientConfig(client, settings);
        }

        private TasksManager _tasks;
        public TasksManager Tasks => _tasks ?? (_tasks = new TasksManager(_clientConfig));
        
        private GlobalIndexes _indexes;
        public GlobalIndexes Indexes => _indexes ?? (_indexes = new GlobalIndexes(_clientConfig));
        
        public NamedIndex Index(string uid)
        {
            return new NamedIndex(_clientConfig, uid);
        }
    }
}
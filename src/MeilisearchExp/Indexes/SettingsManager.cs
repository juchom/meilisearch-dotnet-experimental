using MeilisearchExp.Internal;

namespace MeilisearchExp.Indexes
{
    public class SettingsManager
    {
        private readonly ClientConfig _clientConfig;
        private readonly string _indexUid;

        internal SettingsManager(ClientConfig clientConfig, string indexUid)
        {
            _clientConfig = clientConfig;
            _indexUid = indexUid;
        }
        
        public GetTypoToleranceRequest GetTypoTolerance()
        {
            return new GetTypoToleranceRequest(_clientConfig, _indexUid);
        }
    }
}
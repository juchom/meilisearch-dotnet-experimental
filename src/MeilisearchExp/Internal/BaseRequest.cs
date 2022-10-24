using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MeilisearchExp.Exceptions;
using MeilisearchExp.Internal;

namespace MeilisearchExp.Indexes
{
    public abstract class BaseRequest<TResponse>
    {
        private Activity CurrentActivity;
        
        internal ClientConfig ClientConfig { get; }

        internal BaseRequest(ClientConfig clientConfig)
        {
            ClientConfig = clientConfig;
        }

        protected abstract HttpRequestMessage CreateHttpRequestMessage();
        protected abstract void AddOtelTags(Activity activity);

        public async ValueTask<TResponse> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                TraceRequestStart();
                var request = CreateHttpRequestMessage();
                if (ClientConfig.AuthenticationHeader != null)
                {
                    request.Headers.Authorization = ClientConfig.AuthenticationHeader;
                }

                request.Headers.UserAgent.Add(ClientConfig.UserAgentHeader);
                var response = await ClientConfig.Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead,
                    cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    TraceReceivedFirstResponse();
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        var results = await JsonSerializer.DeserializeAsync<TResponse>(stream,
                            ClientConfig.JsonSerializerOptions,
                            cancellationToken);
                        TraceRequestStop();

                        return results;
                    }
                }

                if (response.Content.Headers.ContentLength == 0)
                {
                    var statusCode = response.StatusCode;
                    var reasonPhrase = response.ReasonPhrase;
                    response.Dispose();
                    throw new MeilisearchApiError(statusCode, reasonPhrase);
                }

                var content = await response.Content
                    .ReadFromJsonAsync<MeilisearchApiErrorContent>(cancellationToken: cancellationToken)
                    .ConfigureAwait(false);
                response.Dispose();
                throw new MeilisearchApiError(content);
            }
            catch (Exception ex)
            {
                TraceSetException(ex);
                throw;
            }
        }

        private void TraceRequestStart()
        {
            if (MeilisearchActivitySource.IsEnabled)
            {
                CurrentActivity = MeilisearchActivitySource.RequestStart(ClientConfig);
                AddOtelTags(CurrentActivity);
            }
        }

        internal void TraceReceivedFirstResponse()
        {
            if (CurrentActivity != null)
            {
                MeilisearchActivitySource.ReceivedFirstResponse(CurrentActivity);
            }
        }
        
        private void TraceRequestStop()
        {
            if (CurrentActivity != null)
            {
                MeilisearchActivitySource.RequestStop(CurrentActivity);
                CurrentActivity = null;
            }
        }

        private void TraceSetException(Exception e)
        {
            if (CurrentActivity != null)
            {
                MeilisearchActivitySource.SetException(CurrentActivity, e);
                CurrentActivity = null;
            }
        }
    }
}
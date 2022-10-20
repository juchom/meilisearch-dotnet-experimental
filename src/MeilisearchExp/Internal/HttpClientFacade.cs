// using System;
// using System.Net.Http;
// using System.Net.Http.Headers;
// using System.Net.Http.Json;
// using System.Text.Json;
// using System.Threading;
// using System.Threading.Tasks;
// using MeilisearchExp.Exceptions;
// using MeilisearchExp.Indexes;
//
// namespace MeilisearchExp.Internal
// {
//     internal class HttpClientFacade
//     {
//         private readonly HttpClient _client;
//         private readonly MeilisearchSettings _settings;
//         private readonly AuthenticationHeaderValue _authenticationHeader;
//         private readonly ProductInfoHeaderValue _userAgentHeader;
//
//         public HttpClientFacade(HttpClient client, MeilisearchSettings settings)
//         {
//             _client = client;
//             _settings = settings;
//             _client.BaseAddress = new Uri(settings.BaseUrl);
//             
//             if (!string.IsNullOrEmpty(settings.ApiKey))
//             {
//                 _authenticationHeader = new AuthenticationHeaderValue("Bearer", settings.ApiKey);
//             }
//
//             _userAgentHeader = new ProductInfoHeaderValue(new ProductHeaderValue(MeilisearchConstants.ClientName, MeilisearchConstants.ClientVersion));
//         }
//
//         internal async ValueTask<T> SendAsync<T>(BaseRequest<T> request, HttpCompletionOption completionOption,
//             CancellationToken cancellationToken)
//         {
//             try
//             {
//                 // Check OTEL is enabled, if yes start span
//                 if (_authenticationHeader != null)
//                 {
//                     request.Headers.Authorization = _authenticationHeader;
//                 }
//
//                 request.Headers.UserAgent.Add(_userAgentHeader);
//                 var response = await _client.SendAsync(request, completionOption, cancellationToken);
//
//                 if (response.IsSuccessStatusCode)
//                 {
//                     using (var stream = await response.Content.ReadAsStreamAsync())
//                     {
//                         return await JsonSerializer.DeserializeAsync<T>(stream, _settings.JsonSerializerOptions,
//                             cancellationToken);
//                         // Otel End span
//                     }
//                 }
//
//                 if (response.Content.Headers.ContentLength == 0)
//                 {
//                     var statusCode = response.StatusCode;
//                     var reasonPhrase = response.ReasonPhrase;
//                     response.Dispose();
//                     throw new MeilisearchApiError(statusCode, reasonPhrase);
//                 }
//
//                 var content = await response.Content
//                     .ReadFromJsonAsync<MeilisearchApiErrorContent>(cancellationToken: cancellationToken)
//                     .ConfigureAwait(false);
//                 response.Dispose();
//                 throw new MeilisearchApiError(content);
//             }
//             catch (HttpRequestException ex)
//             {
//                 throw new MeilisearchCommunicationError("CommunicationError", ex);
//                 // Otel set exception 
//             }
//         }
//         
//         private void TraceRequestStart()
//         {
//             
//         }
//
//         private void TraceRequestStop()
//         {
//             
//         }
//
//         private void TraceSetException(Exception e)
//         {
//             
//         }
//     }
// }
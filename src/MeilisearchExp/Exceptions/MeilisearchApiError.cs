using System;
using System.Net;

namespace MeilisearchExp.Exceptions
{
    public class MeilisearchApiError : Exception
    {
        public string Code { get; }

        public MeilisearchApiError(MeilisearchApiErrorContent apiError)
            : base(
                $"MeilisearchApiError, Message: {apiError.Message}, Code: {apiError.Code}, Type: {apiError.Type}, Link: {apiError.Link}")
        {
            Code = apiError.Code;
        }
        
        public MeilisearchApiError(HttpStatusCode statusCode, string reasonPhrase)
            : base($"MeilisearchApiError, Message: {reasonPhrase}, Code: {(int)statusCode}")
        {}
    }
}
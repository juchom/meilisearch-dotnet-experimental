﻿using System.Text.Json.Serialization;

namespace MeilisearchExp.Exceptions
{
    public class MeilisearchApiErrorContent
    {
        public MeilisearchApiErrorContent(string message, string code, string type, string link)
        {
            Message = message;
            Code = code;
            Type = type;
            Link = link;
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; }

        /// <summary>
        /// Gets the code.
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; }

        /// <summary>
        /// Gets the link.
        /// </summary>
        [JsonPropertyName("link")]
        public string Link { get; }
    }
}
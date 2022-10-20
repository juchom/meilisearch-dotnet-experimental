using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MeilisearchExp
{
    public class TaskResource
    {
        public TaskResource(int uid, string indexUid, TaskInfoStatus status, TaskInfoType type,
            IReadOnlyDictionary<string, object> details, IReadOnlyDictionary<string, string> error, string duration, DateTime enqueuedAt,
            DateTime? startedAt, DateTime? finishedAt)
        {
            Uid = uid;
            IndexUid = indexUid;
            Status = status;
            Type = type;
            Details = details;
            Error = error;
            Duration = duration;
            EnqueuedAt = enqueuedAt;
            StartedAt = startedAt;
            FinishedAt = finishedAt;
        }

        [JsonPropertyName("uid")]
        public int Uid { get; }

        [JsonPropertyName("indexUid")]
        public string IndexUid { get; }

        [JsonPropertyName("status")]
        public TaskInfoStatus Status { get; }

        [JsonPropertyName("type")]
        public TaskInfoType Type { get; }

        [JsonPropertyName("details")]
        public IReadOnlyDictionary<string, dynamic> Details { get; }

        [JsonPropertyName("error")]
        public IReadOnlyDictionary<string, string> Error { get; }

        [JsonPropertyName("duration")]
        public string Duration { get; }

        [JsonPropertyName("enqueuedAt")]
        public DateTime EnqueuedAt { get; }

        [JsonPropertyName("startedAt")]
        public DateTime? StartedAt { get; }

        [JsonPropertyName("finishedAt")]
        public DateTime? FinishedAt { get; }
    }
}
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MeilisearchExp
{
    public class TaskInfo
    {
        public TaskInfo(int taskUid, string indexUid, TaskInfoStatus status, TaskInfoType type,
            IReadOnlyDictionary<string, object> details, IReadOnlyDictionary<string, string> error, string duration, DateTime enqueuedAt,
            DateTime? startedAt, DateTime? finishedAt)
        {
            TaskUid = taskUid;
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
        
        [JsonPropertyName("taskUid")]
        public int TaskUid { get; }
        
        [JsonPropertyName("indexUid")]
        public string IndexUid { get; }
        
        [JsonPropertyName("status")]
        public TaskInfoStatus Status { get; }
        
        [JsonPropertyName("type")]
        public TaskInfoType Type { get; }
        
        [JsonPropertyName("details")]
        public IReadOnlyDictionary<string, object> Details { get; }
        
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
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TaskInfoStatus
    {
        Enqueued,
        Processing,
        Succeeded,
        Failed
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TaskInfoType
    {
        IndexCreation,
        IndexUpdate,
        IndexDeletion,
        DocumentAdditionOrUpdate,
        DocumentDeletion,
        SettingsUpdate,
        DumpCreation
    }
}
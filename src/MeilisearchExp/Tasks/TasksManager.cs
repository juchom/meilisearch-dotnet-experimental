using MeilisearchExp.Internal;

namespace MeilisearchExp.Tasks
{
    public class TasksManager
    {
        private readonly ClientConfig _clientConfig;

        internal TasksManager(ClientConfig clientConfig)
        {
            _clientConfig = clientConfig;
        }

        public GetTaskRequest GetTask(int taskUid)
        {
            return new GetTaskRequest(_clientConfig, taskUid);
        }
        
        // public async ValueTask<TaskResource> WaitForTaskAsync(
        //     int taskUid,
        //     TimeSpan? timeoutMs = null,
        //     TimeSpan? intervalMs = null,
        //     bool throwOnError = false,
        //     CancellationToken cancellationToken = default)
        // {
        //     timeoutMs = timeoutMs ?? TimeSpan.FromMilliseconds(5000);
        //     intervalMs = intervalMs ?? TimeSpan.FromMilliseconds(50);
        //     using (var linkedCancellationTokenSource =
        //            CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
        //     {
        //         linkedCancellationTokenSource.CancelAfter(timeoutMs.Value);
        //
        //         try
        //         {
        //             while (!linkedCancellationTokenSource.IsCancellationRequested)
        //             {
        //                 var task = await GetTaskAsync(taskUid, cancellationToken);
        //                 if (task.Status != TaskInfoStatus.Enqueued && task.Status != TaskInfoStatus.Processing)
        //                 {
        //                     return task;
        //                 }
        //
        //                 await Task.Delay(intervalMs.Value, cancellationToken);
        //             }
        //             throw new MeilisearchTimeoutError("The task " + taskUid.ToString() + " timed out.");
        //         }
        //         catch (OperationCanceledException ex)
        //         {
        //             if (!cancellationToken.IsCancellationRequested)
        //             {
        //                 throw new TimeoutException("The request timed out.", ex);
        //             }
        //             throw;
        //         }
        //     }
        // }
    }
}
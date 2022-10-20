using System.Net;
using FluentAssertions;
using JustEat.HttpClientInterception;
using MeilisearchExp;
using MeilisearchExp.Exceptions;
using MeilisearchExp.Tests.Datasets;
using Xunit;

namespace Given_a_populated_index;

public class when_searching_through_documents
{
    [Fact]
    public async Task looking_for_shazam_should_return_1_hit()
    {
        var settings = new MeilisearchSettings("http://127.0.0.1:7700/");
        var client = new MeilisearchClientExp(settings);

        const string indexUid = nameof(looking_for_shazam_should_return_1_hit);

        var documents = DatasetsAccessor.SmallMovies;

        var task = await client.Index(indexUid).Delete().ExecuteAsync();
        
        var idx = await client.Indexes.CreateIndex(indexUid).ExecuteAsync();

        var task2 = await client.Index(indexUid).Documents.AddDocuments(documents).ExecuteAsync();
        var taskStatus = await client.WaitForTaskAsync(task2.TaskUid);
        
        var res = await client.Index(indexUid)
            .Search<SmallMovie>("shazam")
            .ExecuteAsync();

        res.Should().NotBeNull();
        res.Hits.Count.Should().Be(1);
    }

    [Fact]
    public async Task using_a_fake_httpclient_should_throw()
    {
        const string indexUid = nameof(using_a_fake_httpclient_should_throw);
            
        var settings = new MeilisearchSettings("http://127.0.0.1:7700/");
        var realClient = new MeilisearchClientExp(settings);

        var documents = DatasetsAccessor.SmallMovies;

        var task = await realClient.Index(indexUid).Delete().ExecuteAsync();
        
        var idx = await realClient.Indexes.CreateIndex(indexUid).ExecuteAsync();

        var task2 = await realClient.Index(indexUid).Documents.AddDocuments(documents).ExecuteAsync();
        var taskStatus = await realClient.WaitForTaskAsync(task2.TaskUid);
        
        var options = new HttpClientInterceptorOptions();
        var builder = new HttpRequestInterceptionBuilder();

        builder
            .Requests()
            .ForPost()
            .ForHttp()
            .ForUri(new Uri("http://127.0.0.1:7700/"))
            .ForPath($"indexes/{indexUid}/search")
            .WithStatus(HttpStatusCode.NotFound)
            .Responds()
            .WithSystemTextJsonContent(new MeilisearchApiErrorContent(message: "Index `123` not found.",
                code: "index_not_found",
                type: "invalid_request",
                link: "https://docs.meilisearch.com/errors#index_not_found"))
            .RegisterWith(options);
        
        var fakeClient = new MeilisearchClientExp(options.CreateHttpClient(), settings);
        await fakeClient.Invoking(async client =>
                await client.Index(indexUid).Search<SmallMovie>("shazam").ExecuteAsync())
            .Should().ThrowAsync<MeilisearchApiError>();
    }
}

public static class AsyncHelper
{
    public static async ValueTask<TaskResource> WaitForTaskAsync(this MeilisearchClientExp client,
        int taskUid,
        TimeSpan? timeoutMs = null,
        TimeSpan? intervalMs = null,
        bool throwOnError = false,
    CancellationToken cancellationToken = default)
    {
        timeoutMs = timeoutMs ?? TimeSpan.FromMilliseconds(5000);
        intervalMs = intervalMs ?? TimeSpan.FromMilliseconds(50);
        using (var linkedCancellationTokenSource =
               CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
        {
            linkedCancellationTokenSource.CancelAfter(timeoutMs.Value);
        
            try
            {
                while (!linkedCancellationTokenSource.IsCancellationRequested)
                {
                    var task = await client.Tasks.GetTask(taskUid).ExecuteAsync(cancellationToken);
                    if (task.Status != TaskInfoStatus.Enqueued && task.Status != TaskInfoStatus.Processing)
                    {
                        return task;
                    }
        
                    await Task.Delay(intervalMs.Value, cancellationToken);
                }
                throw new MeilisearchTimeoutError("The task " + taskUid.ToString() + " timed out.");
            }
            catch (OperationCanceledException ex)
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    throw new TimeoutException("The request timed out.", ex);
                }
                throw;
            }
        }
    }
}
using MeilisearchExp;
using MeilisearchExp.OpenTelemetry;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

using var tracerProvider = Sdk.CreateTracerProviderBuilder()
    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("meilisearch-tester"))
    .SetSampler(new AlwaysOnSampler())
    // This activates up Meilisearch's tracing:
    .AddMeilisearch()
    // This prints tracing data to the console:
    .AddConsoleExporter()
    .Build();
    
var settings = new MeilisearchSettings("http://127.0.0.1:7700/");
var client = new MeilisearchClientExp(settings);

await client.Indexes
    .CreateIndex("index").ExecuteAsync();

await Task.Delay(TimeSpan.FromMilliseconds(100));

var documents = new Movie[] {
    new Movie { Id = "1", Title = "Carol", Genres = new string[] { "Romance", "Drama" }  },
    new Movie { Id = "2", Title = "Wonder Woman", Genres = new string[] { "Action", "Adventure" } },
    new Movie { Id = "3", Title = "Life of Pi", Genres = new string[] { "Adventure", "Drama" } },
    new Movie { Id = "4", Title = "Mad Max: Fury Road", Genres = new string[] { "Adventure", "Science Fiction"} },
    new Movie { Id = "5", Title = "Moana", Genres = new string[] { "Fantasy", "Action" } },
    new Movie { Id = "6", Title = "Philadelphia", Genres = new string[] { "Drama" } }
};

await client.Index("index")
    .Documents.AddDocuments(documents)
    .ExecuteAsync();

await Task.Delay(TimeSpan.FromMilliseconds(100));

var typoSettings = await client.Index("index")
    .Settings.GetTypoTolerance()
    .ExecuteAsync();

var res = await client.Index("index")
    .Search<Movie>("philadelphia")
    .WithOffset(10)
    .ExecuteAsync();

await client.Index("index").Delete().ExecuteAsync();

public class Movie
{
    public string Id { get; set; }
    public string Title { get; set; }
    public IEnumerable<string> Genres { get; set; }
}
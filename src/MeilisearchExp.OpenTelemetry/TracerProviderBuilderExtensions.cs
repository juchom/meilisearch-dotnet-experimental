using OpenTelemetry.Trace;

namespace MeilisearchExp.OpenTelemetry;

public static class TracerProviderBuilderExtensions
{
    public static TracerProviderBuilder AddMeilisearch(
        this TracerProviderBuilder builder)
        => builder.AddSource("Meilisearch.NET");
}
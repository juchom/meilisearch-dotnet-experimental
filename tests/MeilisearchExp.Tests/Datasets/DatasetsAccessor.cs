using System.Text.Json;
using System.Text.Json.Serialization;

namespace MeilisearchExp.Tests.Datasets;

internal static class DatasetsAccessor
{
    private static readonly string BasePath = Path.Combine(Directory.GetCurrentDirectory(), "Datasets");
    public static readonly string SmallMoviesJsonPath = Path.Combine(BasePath, "small_movies.json");
    public static readonly string SongsCsvPath = Path.Combine(BasePath, "songs.csv");
    public static readonly string SongsNdjsonPath = Path.Combine(BasePath, "songs.ndjson");

    public static readonly List<SmallMovie> SmallMovies;
    
    static DatasetsAccessor()
    {
        var options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.Never,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        
        var json = File.ReadAllText(SmallMoviesJsonPath);
        SmallMovies = JsonSerializer.Deserialize<List<SmallMovie>>(json, options)!;
    }
}
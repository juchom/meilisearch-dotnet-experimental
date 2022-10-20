using System.Text.Json;
using System.Text.Json.Serialization;

namespace MeilisearchExp.Tests.Datasets;

public class SmallMovie
{
        public string Id { get; set; }
        public string Title { get; set; }
        public string Poster { get; set; }
        public string Overview { get; set; }
        [JsonPropertyName("release_date")]
        [JsonConverter(typeof(UnixEpochDateTimeConverter))]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
}

sealed class UnixEpochDateTimeConverter : JsonConverter<DateTime>
{
        static readonly DateTime s_epoch = new DateTime(1970, 1, 1, 0, 0, 0);

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {

                var unixTime = reader.GetInt64();
                return s_epoch.AddMilliseconds(unixTime);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
                var unixTime = Convert.ToInt64((value - s_epoch).TotalMilliseconds);
                writer.WriteNumberValue(unixTime);
        }
}
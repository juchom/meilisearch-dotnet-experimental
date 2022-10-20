using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MeilisearchExp.Indexes
{
    public class TypoTolerance
    {
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("disableOnAttributes")]
        public IEnumerable<string> DisableOnAttributes { get; set; }

        [JsonPropertyName("disableOnWords")]
        public IEnumerable<string> DisableOnWords { get; set; }

        [JsonPropertyName("minWordSizeForTypos")]
        public TypoSize MinWordSizeForTypos { get; set; }

        public class TypoSize
        {
            [JsonPropertyName("oneTypo")]
            public int? OneTypo { get; set; }

            [JsonPropertyName("twoTypos")]
            public int? TwoTypos { get; set; }
        }
    }
}
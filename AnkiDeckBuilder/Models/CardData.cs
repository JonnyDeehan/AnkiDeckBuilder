using Newtonsoft.Json;

namespace AnkiDeckBuilder.Models
{
    public class CardData
    {
        [JsonProperty("word")]
        public string Word { get; set; }

        [JsonProperty("translation")]
        public string Translation { get; set; }

        [JsonProperty("phonetic")]
        public string Phonetic { get; set; }

        [JsonProperty("sentence")]
        public string Sentence { get; set; }

        [JsonProperty("phoneticSentence")]
        public string TargetSentence { get; set; }

        [JsonProperty("phoneticSentence")]
        public string PhoneticSentence { get; set; }
    }
}

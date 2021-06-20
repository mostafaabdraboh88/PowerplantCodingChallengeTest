using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PowerplantCodingChallengeTest.Models
{
    public partial class Powerplant
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("type")]
        public PowerplantType Type { get; set; }
        [JsonPropertyName("efficiency")]
        public decimal Efficiency { get; set; }
        [JsonPropertyName("pmin")]
        public decimal Pmin { get; set; }
        [JsonPropertyName("pmax")]
        public decimal Pmax { get; set; }

    }
}

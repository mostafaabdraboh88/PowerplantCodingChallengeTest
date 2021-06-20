using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PowerplantCodingChallengeTest.Models
{
    public class Payload
    {
        [JsonPropertyName("load")]
        public int Load { get; set; }
        [JsonPropertyName("fuels")]
        public Fuel fuel { get; set; }
        [JsonPropertyName("powerplants")]
        public List<Powerplant> Powerplants { get; set; }
    }
}

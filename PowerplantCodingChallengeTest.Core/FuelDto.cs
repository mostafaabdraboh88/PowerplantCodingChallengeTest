using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PowerplantCodingChallengeTest.Core
{
    public class FuelDto
    {
        [JsonPropertyName("gas(euro/MWh)")]
        public decimal Gas { get; set; }
        [JsonPropertyName("kerosine(euro/MWh)")]
        public decimal kerosine { get; set; }
        [JsonPropertyName("co2(euro/ton)")]
        public decimal co2 { get; set; }
        [JsonPropertyName("wind(%)")]
        public decimal wind { get; set; }
    }
}

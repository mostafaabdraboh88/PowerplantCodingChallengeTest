using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PowerplantCodingChallengeTest.Models
{
    public partial class Powerplant
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum PowerplantType
        {
            gasfired,
            turbojet,
            windturbine
        }
    }
}

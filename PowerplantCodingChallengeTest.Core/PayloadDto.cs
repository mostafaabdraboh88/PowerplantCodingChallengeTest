using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PowerplantCodingChallengeTest.Core
{
    public class PayloadDto
    {
        public int Load { get; set; }
        public FuelDto fuel { get; set; }
        public List<PowerplantDto> Powerplants { get; set; }
    }
}

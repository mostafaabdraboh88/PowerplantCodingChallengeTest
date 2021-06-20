using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PowerplantCodingChallengeTest.Core
{
    public partial class PowerplantDto
    {
        public enum PowerplantType
        {
            gasfired,
            turbojet,
            windturbine
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerplantCodingChallengeTest.Core
{
    public class ResponseDto
    {
        public List<UnitCommitmentDto> UnitCommitmentDtoList { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsRequestSucceeded { get; set; }
    }
}

using PowerplantCodingChallengeTest.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerplantCodingChallengeTest.Bussiness
{
    public interface IProductionPlanBussiness
    {
        public ResponseDto CalculateUnitCommitment(PayloadDto payload);
    }
}

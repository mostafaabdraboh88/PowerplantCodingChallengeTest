using PowerplantCodingChallengeTest.Core;
using PowerplantCodingChallengeTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerplantCodingChallengeTest.Mappers
{
    public static class UnitCommitmentMapper
    {
        public static List<UnitCommitment> MapUnitCommitmentListToDto(List<UnitCommitmentDto> unitCommitmentDtoList)
        {
            return unitCommitmentDtoList.Select(u => new UnitCommitment() {Name = u.Name , Power = u.Power }).ToList();
        }
    }
}

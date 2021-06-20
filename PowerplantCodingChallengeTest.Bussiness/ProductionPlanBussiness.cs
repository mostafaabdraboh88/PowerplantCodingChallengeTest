using PowerplantCodingChallengeTest.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerplantCodingChallengeTest.Bussiness
{
    public class ProductionPlanBussiness : IProductionPlanBussiness
    {
        /// <summary>
        /// Calculate Powerplants Needed Power
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>List<PayloadResponse></returns>
        public ResponseDto CalculateUnitCommitment(PayloadDto payload)
        {
            ResponseDto responseDto = new ResponseDto();
            List<UnitCommitmentDto> unitCommitmentList = new List<UnitCommitmentDto>();
            if (payload != null && payload.Powerplants.Count > new int())
            {
                foreach (PowerplantDto powerplant in payload.Powerplants.OrderBy(p => p.MwhPrice).ThenBy(p => p.Pmin))
                {
                    UnitCommitmentDto unitCommitment = new UnitCommitmentDto() { Name = powerplant.Name, Power = powerplant.Pmax, MwhPrice = powerplant.MwhPrice, Pmin = powerplant.Pmin, Pmax = powerplant.Pmax };
                    unitCommitmentList.Add(unitCommitment); // add UnitCommitment after setting the power to the max 
                    decimal unitsPowerSum = unitCommitmentList.Sum(u => u.Power);
                    if (unitsPowerSum == payload.Load) break; // check if load is reached
                    if (unitsPowerSum > payload.Load) // check if the load is exceeded to start decreasing the power till reach the required load
                    {
                        unitCommitmentList = DecreaseUnitCommitmentMaxPowerTillReachLoad(unitCommitmentList, payload.Load);
                        break;
                    }
                }
                if (unitCommitmentList.Sum(u => u.Power) == payload.Load)
                {
                    responseDto.UnitCommitmentDtoList = unitCommitmentList;
                    responseDto.IsRequestSucceeded = true;
                    responseDto.ErrorMessage = "No error";
                }
                else
                {
                    responseDto.UnitCommitmentDtoList = new List<UnitCommitmentDto>();
                    responseDto.IsRequestSucceeded = false;
                    responseDto.ErrorMessage = "Error Happens , Could not reach required load";
                }

            }
            return responseDto;
        }
        /// <summary>
        /// DecreaseUnit Commitment Max Power Till Reach Load
        /// </summary>
        /// <param name="unitCommitmentList"></param>
        /// <param name="load"></param>
        /// <returns>List<UnitCommitmentDto></returns>
        private List<UnitCommitmentDto> DecreaseUnitCommitmentMaxPowerTillReachLoad(List<UnitCommitmentDto> unitCommitmentList, int load)
        {
            List<UnitCommitmentDto> unitCommitmentDtoList = new List<UnitCommitmentDto>();
            decimal overPowerValue = unitCommitmentList.Sum(u => u.Power) - load;
            foreach (UnitCommitmentDto unitCommitmentDto in unitCommitmentList.OrderByDescending(u => u.MwhPrice).ThenBy(u => u.Pmin))
            {
                unitCommitmentList.Remove(unitCommitmentDto);
                if (unitCommitmentDto.Pmax - unitCommitmentDto.Pmin > new int()) unitCommitmentDto.Power = unitCommitmentDto.Pmin; // Check if the min power can be used instead of the max power
                decimal unitCommitmenPowerAfterDecrease = unitCommitmentList.Sum(u => u.Power) + unitCommitmentDto.Power; // Check the power sum before adding the decreased  UnitCommitment to the list
                if (unitCommitmenPowerAfterDecrease == load)// Check if the power after decrease is matching the load
                {
                    unitCommitmentDtoList.Add(unitCommitmentDto);
                    break;
                }
                if (unitCommitmenPowerAfterDecrease < load)
                {
                    unitCommitmentDto.Power += load - unitCommitmenPowerAfterDecrease; // add the reamining power to the decreased unit to match the load
                    unitCommitmentDtoList.Add(unitCommitmentDto);
                    break;
                }
            }
            unitCommitmentDtoList.AddRange(unitCommitmentList);
            return unitCommitmentDtoList.OrderBy(u => u.MwhPrice).ThenBy(u => u.Pmin).ToList();
        }
    }
}

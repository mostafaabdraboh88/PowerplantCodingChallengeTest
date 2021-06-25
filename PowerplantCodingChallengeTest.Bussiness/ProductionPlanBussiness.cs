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
                unitCommitmentList = GetUnitsWhilePowerIsInMinValue(payload.Powerplants);
                decimal remainingPower = payload.Load - unitCommitmentList.Sum(u => u.Power);
                if (remainingPower > new decimal())
                {
                    // try to increase the unit power taking in count the pmax and the price
                    foreach (UnitCommitmentDto unitCommitmentItem in unitCommitmentList.OrderBy(p => p.MwhPrice).ThenBy(p => p.Pmin))
                    {
                        if (remainingPower > new decimal())
                        {
                            decimal differnceBetweenMaxAndMin = unitCommitmentItem.Pmax - unitCommitmentItem.Pmin;
                            if (differnceBetweenMaxAndMin > remainingPower)
                            {
                                unitCommitmentItem.Power = unitCommitmentItem.Pmin + remainingPower;
                                remainingPower = new decimal();
                            }
                            else if (differnceBetweenMaxAndMin == remainingPower)
                            {
                                unitCommitmentItem.Power = unitCommitmentItem.Pmax;
                                remainingPower = unitCommitmentItem.Pmin;
                            }
                            else
                            {
                                unitCommitmentItem.Power = unitCommitmentItem.Pmax;
                                remainingPower = remainingPower - differnceBetweenMaxAndMin;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            responseDto = SetTheResponse(unitCommitmentList, payload.Load);
            return responseDto;
        }

        #region Private Methods
        private ResponseDto SetTheResponse(List<UnitCommitmentDto> unitCommitmentList, int load)
        {
            ResponseDto responseDto = new ResponseDto();
            if (unitCommitmentList.Sum(u => u.Power) == load)
            {
                responseDto.UnitCommitmentDtoList = unitCommitmentList.OrderBy(u => u.MwhPrice).ThenBy(u => u.Pmin).ToList();
                responseDto.IsRequestSucceeded = true;
                responseDto.ErrorMessage = "No error";
            }
            else
            {
                responseDto.UnitCommitmentDtoList = new List<UnitCommitmentDto>();
                responseDto.IsRequestSucceeded = false;
                responseDto.ErrorMessage = "Error Happens , Could not reach required load";
            }
            return responseDto;
        }

        private List<UnitCommitmentDto> GetUnitsWhilePowerIsInMinValue(List<PowerplantDto> powerplants)
        {
            List<UnitCommitmentDto> unitCommitmentDtos = powerplants.Select(p => new UnitCommitmentDto()
            {
                Name = p.Name,
                MwhPrice = p.MwhPrice,
                Power = p.Pmin,
                Pmax = p.Pmax,
                Pmin = p.Pmin
            }).ToList();

            return unitCommitmentDtos;
        } 
        #endregion
    }
}

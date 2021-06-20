using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PowerplantCodingChallengeTest.Bussiness;
using PowerplantCodingChallengeTest.Core;
using PowerplantCodingChallengeTest.Mappers;
using PowerplantCodingChallengeTest.Models;
using System.Collections.Generic;

namespace PowerplantCodingChallengeTest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductionPlanController : ControllerBase
    {
        #region Prop
        private readonly ILogger<ProductionPlanController> _logger;
        public IProductionPlanBussiness ProductionPlanBussiness { get; }

        #endregion
        #region CTOR
        public ProductionPlanController(IProductionPlanBussiness productionPlanBussiness, ILogger<ProductionPlanController> logger)
        {
            _logger = logger;
            ProductionPlanBussiness = productionPlanBussiness;
        }

        #endregion

        #region Actions
        // POST: api/TodoItems
        [HttpPost]
        [ProducesResponseType(typeof(List<UnitCommitment>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public ActionResult<List<UnitCommitment>> PostPayload([FromBody] Payload payload)
        {
            try
            {
                PayloadDto payloadDto = PayloadMapper.MapPayloadToDto(payload);
                ResponseDto responseDto = ProductionPlanBussiness.CalculateUnitCommitment(payloadDto);
                if (responseDto.IsRequestSucceeded)
                {
                    List<UnitCommitment> unitCommitmentList = UnitCommitmentMapper.MapUnitCommitmentListToDto(responseDto.UnitCommitmentDtoList);
                    return Ok(unitCommitmentList);
                }
                else
                {
                    return BadRequest(responseDto.ErrorMessage);
                }
            }
            catch (System.Exception ex)
            {

                _logger.LogError("Exception Message : " + ex.InnerException.ToString());
                return BadRequest("Error Happens!!!");
            }
        }
        #endregion

    }
}

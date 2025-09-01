using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DTOs.DTO;
using DTOs.Models;
using BusinessLayer.BusinessBL;
using BusinessLayer.Abstraction;
namespace ShipAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalarySummaryController : ControllerBase
    {
        ILogger<SalarySummaryController> _logger;
        IMapper _mapper;
        ShipBL<SalarySummaryDTO> _ShipBL;
        private readonly ISalarySummaryBL _SalarySummaryBl;

        public SalarySummaryController(ILogger<SalarySummaryController> logger,
                                            ISalarySummaryBL salarySummaryBL,
            IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _SalarySummaryBl = salarySummaryBL;
        }


        [HttpGet]
        [Route("All")]
        public ActionResult<SalarySummaryDTO> GetAllSalarySummary(string crewId)
        {
            if (string.IsNullOrEmpty(crewId))
            {
                return NoContent();
            }
            var SalarySummary = _SalarySummaryBl.GetAll(crewId);
            return Ok(SalarySummary);
        }

        [HttpGet]
        [Route("{salarySummaryId:int}", Name = "GetSalarySummaryById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SalarySummaryDTO>> GetSalarySummaryById(int salarySummaryId)
        {
            if (salarySummaryId < 0)
            {
                _logger.LogError("Salary Summary Id Is not Valid!");
                return BadRequest();
            }

            //var salarySummary = await _dbContext.CrewSalarySummary.Where(salarySummary => salarySummary.SalarySummaryId == salarySummaryId).FirstOrDefaultAsync();
            var salarySummary = await _ShipBL.Get(salarySummary => salarySummary.SalarySummaryId == salarySummaryId);
            if (salarySummary == null)
            {
                _logger.LogError("Salary summary Info not Available");
                return NotFound($"Salary summary info with ID:{salarySummaryId} Not Found!");
            }
            var leaveDTO = _mapper.Map<SalarySummaryDTO>(salarySummary);

            return Ok(leaveDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("Create")]
        public async Task<ActionResult<SalarySummaryDTO>> AddSalarySummary([FromBody] SalarySummaryDTO salarySummaryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (salarySummaryDTO == null || salarySummaryDTO.SalarySummaryId < 0)
            {
                _logger.LogError("Email Id Id not Valid!");
                return BadRequest();
            }
            if(salarySummaryDTO.SalaryId == -1)
            {
                return NotFound(new { Message = " Salary Record Not Added. Please Add Total Salary First!" });
            }
            SalarySummaryDTO ?salary= null;
            if (salarySummaryDTO.SalarySummaryId == 0)
            {
                 salary = _SalarySummaryBl.Add(salarySummaryDTO);
            }
            else
            {
                _logger.LogInformation("Salary Info Already Present!");
                return BadRequest("Salary Info Already Present!");
            }

            //SalaryInformation obj = _mapper.Map<SalaryInformation>(salarySummaryDTO);

            _logger.LogInformation($"Recorde Added Successfully.ID {salarySummaryDTO.SalarySummaryId}");
            return CreatedAtRoute("GetSalarySummaryById", new { salarySummaryId= salary.SalarySummaryId }, salarySummaryDTO);
        }

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateSalarySummary(SalarySummaryDTO SalarySummaryDTO)
        {
            if (SalarySummaryDTO == null || SalarySummaryDTO.SalarySummaryId <= 0)
            {
                _logger.LogError("Salary Summary Id is not Valid!");
                return BadRequest();
            }
            var salarySumobj = _SalarySummaryBl.Update(SalarySummaryDTO);
            if (salarySumobj == null)
            {
                return BadRequest();
            }

            return NoContent();
        }


        [HttpPatch]
        [Route("{salarySummaryId:int}/PartialUpdate")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PartialUpdateSalarySummary([FromRoute] int salarySummaryId, [FromBody] JsonPatchDocument<SalarySummaryDTO> patchdocument)
        {
            if (patchdocument == null || salarySummaryId < 0)
            {
                return BadRequest();
            }
            //SalarySummary? salarySumobj = await _dbContext.CrewSalarySummary.Where(salarySummary => salarySummary.SalarySummaryId == salarySummaryId).FirstOrDefaultAsync();
            var salarySumobj = await _ShipBL.Get(salarySummary => salarySummary.SalarySummaryId == salarySummaryId);
            if (salarySumobj == null)
            {
                return NotFound();
            }

            var salarySummaryDTo = _mapper.Map<SalarySummaryDTO>(salarySumobj);

            // need to apply from patch document
            patchdocument.ApplyTo(salarySummaryDTo, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //salarySumobj = _mapper.Map<SalarySummary>(salarySummaryDTo);
            //await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("Delete/{salarySummaryId:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteSalarySummary(int salarySummaryId)
        {
            if (salarySummaryId < 0)
            {
                return BadRequest();
            }

           // SalarySummary? salarySummaryobj = await _dbContext.CrewSalarySummary.FirstOrDefaultAsync(salarySummary => salarySummary.SalarySummaryId == salarySummaryId);
            var salarySummaryobj =  _SalarySummaryBl.Get(salarySummaryId);
            if (salarySummaryobj == null)
            {
                return NotFound($"Salary Summary Info With Id:{salarySummaryId} Not Found!!");
            }

            return Ok(true);
        }
    }
}

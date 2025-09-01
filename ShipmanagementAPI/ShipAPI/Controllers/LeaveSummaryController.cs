using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using DTOs.DTO;
using BusinessLayer.BusinessBL;
using BusinessLayer.Abstraction;
namespace ShipAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveSummaryController : ControllerBase
    {
        ILogger<LeaveSummaryController> _logger;
        IMapper _mapper;
        ILeaveSummaryBL _leaveSummaryBL;
        public LeaveSummaryController(ILogger<LeaveSummaryController> logger,
                        IMapper mapper,
                        ILeaveSummaryBL leaveSummaryBL)
        {
            _logger = logger;
            //_dbContext = dbContext;
            _leaveSummaryBL = leaveSummaryBL;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("All")]
        public async Task<ActionResult<LeaveSummaryDTO>> GetAllLeaveSummary(string crewId)
        {
            if (string.IsNullOrEmpty(crewId))
            {
                return NoContent();
            }
            var SalarySummary = _leaveSummaryBL.GetAll(crewId);
            return Ok(SalarySummary);
        }

        [HttpGet]
        [Route("{leaveSummaryId:int}", Name = "GetLeaveSummaryById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LeaveSummaryDTO>> GetLeaveSummaryById(int leaveSummaryId)
        {
            if (leaveSummaryId < 0)
            {
                _logger.LogError("Leave Summary Id Is not Valid!");
                return BadRequest();
            }

            //var leaveSummary = await _dbContext.CrewLeaveSummary.Where(leave => leave.LeaveSummaryId == leaveSummaryId).FirstOrDefaultAsync();
            var leaveSummary =  _leaveSummaryBL.Get(leaveSummaryId);
            if (leaveSummary == null)
            {
                _logger.LogError("Leave summary Info not Available");
                return NotFound($"Leave summary info with ID:{leaveSummaryId} Not Found!");
            }
            var leaveDTO = _mapper.Map<LeaveSummaryDTO>(leaveSummary);

            return Ok(leaveDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("Create")]
        public async Task<ActionResult<LeaveSummaryDTO>> AddLeaveSummary([FromBody] LeaveSummaryDTO leaveSummaryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (leaveSummaryDTO == null)
            {
                _logger.LogError("Leave Id is not Valid!");
                return BadRequest();
            }
            //var leave = await _dbContext.CrewLeaveSummary.Where(leave => leave.LeaveSummaryId == leaveSummaryDTO.LeaveSummaryId).FirstOrDefaultAsync();
            var leave = _leaveSummaryBL.Add(leaveSummaryDTO);

            if (leave == null)
            {
                _logger.LogInformation("Leave summary Info Already Present!");
                return BadRequest("Leave summary Info Already Present!");
            }

            

            _logger.LogInformation($"Recorde Added Successfully.Leave Summary ID {leaveSummaryDTO.LeaveSummaryId}");
            // Return a 201 Created response

            return CreatedAtRoute("GetLeaveSummaryById", new { leaveId = leaveSummaryDTO.LeaveSummaryId }, leaveSummaryDTO);
        }

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateLeaveSummary(LeaveSummaryDTO leaveSummaryDTO)
        {
            if (leaveSummaryDTO == null || leaveSummaryDTO.LeaveSummaryId <= 0)
            {
                _logger.LogError("Leave Summary Id is not Valid!");
                return BadRequest();
            }
            // LeaveSummary? leaveobj = await _dbContext.CrewLeaveSummary.FirstOrDefaultAsync(leaveSummary => leaveSummary.LeaveSummaryId == leaveSummaryDTO.LeaveSummaryId);
            var leaveobj = _leaveSummaryBL.Update(leaveSummaryDTO);
            if (leaveobj == null)
            {
                return BadRequest();
            }

            //leaveobj = _mapper.Map<LeaveSummary>(leaveSummaryDTO);
            //await _dbContext.SaveChangesAsync();
            return NoContent();
        }


        [HttpPatch]
        [Route("{leaveSummaryId:int}/PartialUpdate")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PartialUpdateLeaveSummaryAsync([FromRoute] int leaveSummaryId, [FromBody] JsonPatchDocument<LeaveSummaryDTO> patchdocument)
        {
            if (patchdocument == null || leaveSummaryId < 0)
            {
                return BadRequest();
            }
            //LeaveSummary? leaveobj = await _dbContext.CrewLeaveSummary.Where(leaveSummary => leaveSummary.LeaveSummaryId == leaveSummaryId).FirstOrDefaultAsync();
            var leaveobj = _leaveSummaryBL.Get(leaveSummaryId);
            if (leaveobj == null)
            {
                return NotFound();
            }

            var userDTO = _mapper.Map<LeaveSummaryDTO>(leaveobj);

            // need to apply from patch document
            patchdocument.ApplyTo(userDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //leaveobj = _mapper.Map<LeaveSummary>(userDTO);
            //await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("Delete/{leaveSummaryId:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteLeaveSummary(int leaveSummaryId)
        {
            if (leaveSummaryId < 0)
            {
                return BadRequest();
            }

            //LeaveSummary? leaverSummaryobj = await _dbContext.CrewLeaveSummary.FirstOrDefaultAsync(leaveSummary => leaveSummary.LeaveSummaryId == leaveSummaryId);
            var leaverSummaryobj =  _leaveSummaryBL.Remove(leaveSummaryId);
            if (leaverSummaryobj == null)
            {
                return NotFound($"Leave Info With leave Id:{leaveSummaryId} Not Found!!");
            }

            //_dbContext.CrewLeaveSummary.Remove(leaverSummaryobj);
            //_dbContext.SaveChanges();

            return Ok(true);
        }
    }
}

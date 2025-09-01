using AutoMapper;
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
    public class LeaveController : ControllerBase
    {
        ILogger<LeaveController> _logger;
        IMapper _mapper;
        private readonly ILeavesBL _leavesBL;

        public LeaveController(ILogger<LeaveController> logger,
                ILeavesBL leavesBL, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _leavesBL = leavesBL;
        }

        [HttpGet]
        [Route("All")]
        public ActionResult<LeaveInfoDTO> GetAllLeaveInfo()
        {
            var userId = User.FindFirst("UserID")?.Value;
            var leavesDto = _leavesBL.GetAll(userId);
            return Ok(leavesDto);
        }

        [HttpGet]
        [Route("{leaveId:int}", Name = "GetLeaveInfoById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<LeaveInfoDTO> GetLeaveInfoById(int leaveId)
        {
            if (leaveId < 0)
            {
                _logger.LogError("Email Id Id not Valid!");
                return BadRequest();
            }

            var leave = _leavesBL.Get(leaveId);
            if (leave == null)
            {
                _logger.LogError("leave Info not Available");
                return NotFound($"leave info with ID:{leaveId} Not Found!");
            }
            var leaveDTO = _mapper.Map<LeaveInfoDTO>(leave);

            return Ok(leaveDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("Create")]
        public ActionResult<LeaveInfoDTO> AddLeaveInfo([FromBody] LeaveInfoDTO leaveDTO)
        {
            if (!ModelState.IsValid || leaveDTO == null)
            {
                return BadRequest();
            }

            var leave = _leavesBL.Add(leaveDTO);

            if (leave != null)
            {
                return BadRequest("Leave Info Already Present!");
            }

            _logger.LogInformation($"Recorde Added Successfully.Leave ID {leaveDTO.LeaveId}");

            // Return a 201 Created response
            return Ok(StatusCodes.Status200OK);
        }

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateLeaveInfo(LeaveInfoDTO leaveDTO)
        {
            if (leaveDTO == null || leaveDTO.LeaveId <= 0)
            {
                _logger.LogError("Leave Id is not Valid!");
                return BadRequest();
            }
            var leaveobj = _leavesBL.Update(leaveDTO);
            if (leaveobj == null)
            {
                return BadRequest();
            }

            return NoContent();
        }


        [HttpPatch]
        [Route("{leaveId:int}/PartialUpdate")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PartialUpdateLeaveInfoAsync([FromRoute] int leaveId, [FromBody] JsonPatchDocument<LeaveInfoDTO> patchdocument)
        {
            if (patchdocument == null || leaveId < 0)
            {
                return BadRequest();
            }
            //LeaveInformation? leaveobj = await _shipBL.Get(leave => leave.LeaveId == leaveId);
            var leaveobj = _leavesBL.Get(leaveId);
            if (leaveobj == null)
            {
                return NotFound();
            }

            var leaveDTO = _mapper.Map<LeaveInfoDTO>(leaveobj);

            // need to apply from patch document
            patchdocument.ApplyTo(leaveDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //leaveobj = _mapper.Map<LeaveInformation>(leaveDTO);
            //await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("Delete/{leaveId:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> DeleteLeaveInfo(int leaveId)
        {
            if (leaveId < 0)
            {
                return BadRequest();
            }

            var leaverobj = _leavesBL.Remove(leaveId);
            if (leaverobj == null)
            {
                return NotFound($"Leave Info With leave Id:{leaveId} Not Found!!");
            }

            return Ok(true);
        }
    }
}

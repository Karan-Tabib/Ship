using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using DTOs.DTO;
using BusinessLayer.Abstraction;
using Microsoft.AspNetCore.Authorization;
namespace ShipAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrewController : ControllerBase
    {
        ILogger<CrewController> _logger;
        //private IShipBL<CrewInfoDTO> _shipBL;
        IMapper _mapper;
        ICrewBL _crewBL;

        public CrewController(ILogger<CrewController> logger,
                                     //IShipBL<CrewInfoDTO> shipRepository,
                                     ICrewBL crewBL,
                                    IMapper mapper)
        {
            _logger = logger;
            //_shipBL = shipRepository;
            _mapper = mapper;
            _crewBL = crewBL;
        }

        [HttpGet]
        [Route("All")]
        [Authorize]
        public async Task<ActionResult<CrewInfoDTO>> GetAllCrewMember()
        {
            _logger.LogInformation(" GetAllUser Execution Started");

            var userId = User.FindFirst("UserID")?.Value;
            var crews = _crewBL.GetAll(userId);

            //var crewsDTO = _mapper.Map<List<CrewInfoDTO>>(crews);
            _logger.LogInformation("Record Retrived " + crews.Count());
            return Ok(crews);
        }

        [HttpGet]
        [Route("{crewId:int}", Name = "GetCrewMemberById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<CrewInfoDTO> GetCrewMemberById(int crewId)
        {
            if (crewId < 0)
            {
                _logger.LogError("crewId Id is not Valid!");
                return BadRequest();
            }

            var crewMember = _crewBL.Get(crewId);
            if (crewMember == null)
            {
                _logger.LogError("crewMember Not Available");
                return NotFound($"crewMember with ID:{crewId} Not Found!");
            }
            //var crewMemberDTO = _mapper.Map<CrewInfoDTO>(crewMember);

            return Ok(crewMember);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("Create")]
        public ActionResult<CrewInfoDTO> AddCrewMember([FromBody] CrewInfoDTO crewMemberDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (crewMemberDTO == null || crewMemberDTO.CrewID < 0)
            {
                _logger.LogError("CrewID Id is not Valid!");
                return BadRequest();
            }
            CrewInfoDTO crew = null;
            if (crewMemberDTO.CrewID == 0)
            {
                crew = _crewBL.Add(crewMemberDTO);
            }
            else
            {
                _logger.LogInformation("Crew Member Already Present!");
                return BadRequest("Crew Member Already Present!");
            }
            // CrewInformation obj = _mapper.Map<CrewInformation>(crewMemberDTO);

            //await _shipBL.Add(obj);

            _logger.LogInformation($"Recorde Added Successfully.CrewID:{crewMemberDTO.CrewID}");

            // Return a 201 Created response
            return CreatedAtRoute("GetCrewMemberById", new { crewId = crewMemberDTO.CrewID }, crewMemberDTO);
        }

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateCrewMember(CrewInfoDTO crewMemberDTO)
        {
            if (crewMemberDTO == null || crewMemberDTO.CrewID < 0)
            {
                _logger.LogError("CrewID Id is not Valid!");
                return BadRequest();
            }
            CrewInfoDTO? crewobj = _crewBL.Update(crewMemberDTO);
            if (crewobj == null)
            {
                return BadRequest();
            }

            crewobj = _mapper.Map<CrewInfoDTO>(crewMemberDTO);
            //await _crewBL.Update(crewobj);
            return NoContent();
        }


        [HttpPatch]
        [Route("{crewId:alpha}/PartialUpdate")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PartialUpdateCrewMemberAsync([FromRoute] int crewId, [FromBody] JsonPatchDocument<CrewInfoDTO> patchdocument)
        {
            if (patchdocument == null || crewId < 0)
            {
                return BadRequest();
            }
            CrewInfoDTO? crewobj = _crewBL.Get(crewId);
            if (crewobj == null)
            {
                return NotFound();
            }

            var crewDTO = _mapper.Map<CrewInfoDTO>(crewobj);

            // need to apply from patch document
            patchdocument.ApplyTo(crewDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            crewobj = _mapper.Map<CrewInfoDTO>(crewDTO);

            //await _crewBL.Update(crewobj);

            return NoContent();
        }

        [HttpDelete]
        [Route("Delete/{crewId:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteCrewMemberAsync(int crewId)
        {
            if (crewId < 0)
            {
                return BadRequest();
            }

            var crewObj = _crewBL.Remove(crewId);
            if (crewObj == null)
            {
                return NotFound($"User With Email Id:{crewId} Not Found!!");
            }

            return Ok(true);
        }
    }
}

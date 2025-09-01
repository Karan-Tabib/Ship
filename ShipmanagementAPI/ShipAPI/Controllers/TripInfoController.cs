using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DTOs.DTO;
using DTOs.Models;
using BusinessLayer.BusinessBL;

namespace ShipAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripInfoController : ControllerBase
    {
        ILogger<TripInfoController> _logger;
        //ShipManagemntDbContext _dbContext;
        IMapper _mapper;
        ShipBL<TripInfoDTO> _shipBL;
        public TripInfoController(ILogger<TripInfoController> logger,
                    //ShipManagemntDbContext dbContext,
                    ShipBL<TripInfoDTO> shipBL,
            IMapper mapper)
        {
            _logger = logger;
            //_dbContext = dbContext;
            _shipBL = shipBL;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All")]
        public async Task<ActionResult<TripInfoDTO>> GetAllTripInfo()
        {
            _logger.LogInformation(" GetAllTripInfo Execution Started");
            //var tripinfos = await _dbContext.TripInformation.ToListAsync();
            var tripinfos = await _shipBL.GetAll();
            var tripInfoDTO = _mapper.Map<List<SalaryInfoDTO>>(tripinfos);
            _logger.LogInformation("Record Retrived " + tripinfos.Count());
            return Ok(tripInfoDTO);
        }

        [HttpGet]
        [Route("{Id:int}", Name = "GetTripInfoById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TripInfoDTO>> GetTripInfoById(int Id)
        {
            if (Id < 0)
            {
                _logger.LogError("Trip Id Is not Valid!");
                return BadRequest();
            }

            //var tripinfo = await _dbContext.TripInformation.Where(salary => salary.TripId == Id).FirstOrDefaultAsync();
            var tripinfo = await _shipBL.Get(tripinfo => tripinfo.TripId == Id);
            if (tripinfo == null)
            {
                _logger.LogError($"TripInformation Not Available");
                return NotFound($"TripInformation id:{Id} Not Found!");
            }
            var tripInfoDTO = _mapper.Map<TripInfoDTO>(tripinfo);

            return Ok(tripInfoDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("Create")]
        public async Task<ActionResult<TripInfoDTO>> AddTripInformation([FromBody] TripInfoDTO tripInfoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (tripInfoDTO == null || tripInfoDTO.TripId < 0)
            {
                _logger.LogError("Email Id Id not Valid!");
                return BadRequest();
            }
            //var tripinfo = await _dbContext.TripInformation.Where(salary => salary.TripId == tripInfoDTO.TripId).FirstOrDefaultAsync();
            var tripinfo = await _shipBL.Get(tripinfo => tripinfo.TripId == tripInfoDTO.TripId);

            if (tripinfo != null)
            {
                _logger.LogInformation("TripInformation Already Present!");
                return BadRequest("TripInformation Already Present!");
            }

            TripInformation tripInformation = _mapper.Map<TripInformation>(tripInfoDTO);

            //await _dbContext.TripInformation.AddAsync(tripInformation);
            //await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Recorde Added Successfully.ID {tripInfoDTO.TripId}");

            // Return a 201 Created response
            return CreatedAtRoute("GetTripInfoById", new { emailid = tripInfoDTO.TripId }, tripInfoDTO);
        }

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateTripInformation(TripInfoDTO tripInfoDTO)
        {
            if (tripInfoDTO == null || tripInfoDTO.TripId < 0)
            {
                _logger.LogError("Trip Id Is not Valid!");
                return BadRequest();
            }
            //TripInformation? tripInformation = await _dbContext.TripInformation.FirstOrDefaultAsync(trip => trip.TripId == tripInfoDTO.TripId);
            var tripInformation = await _shipBL.Get(trip => trip.TripId == tripInfoDTO.TripId);
            if (tripInformation == null)
            {
                return BadRequest();
            }

            //tripInformation = _mapper.Map<TripInformation>(tripInfoDTO);
            //await _dbContext.SaveChangesAsync();
            return NoContent();
        }


        [HttpPatch]
        [Route("{Id:alpha}/PartialUpdate")]
        //PartialUpdate")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PartialUpdateTripInfomation([FromRoute] int id, [FromBody] JsonPatchDocument<TripInfoDTO> patchdocument)
        {
            if (patchdocument == null || id < 0)
            {
                return BadRequest();
            }
            //TripInformation? tripInformation = await _dbContext.TripInformation.Where(trip => trip.TripId == id).FirstOrDefaultAsync();
            var tripInformation = await _shipBL.Get(trip => trip.TripId == id);
            if (tripInformation == null)
            {
                return NotFound();
            }

            var tripInfoDTO = _mapper.Map<TripInfoDTO>(tripInformation);

            // need to apply from patch document
            patchdocument.ApplyTo(tripInfoDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //tripInformation = _mapper.Map<TripInformation>(tripInfoDTO);
            //await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("Delete/{Id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteTripInformation(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            //TripInformation? tripInformation = await _dbContext.TripInformation.FirstOrDefaultAsync(trip => trip.TripId == id);
            var tripInformation = await _shipBL.Get(trip => trip.TripId == id);
            if (tripInformation == null)
            {
                return NotFound($"Trip info With Id:{id} Not Found!!");
            }

            //_dbContext.TripInformation.Remove(tripInformation);
            //_dbContext.SaveChanges();

            return Ok(true);
        }
    }
}

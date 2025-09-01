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
    public class TripExpenditureController : ControllerBase
    {
        ILogger<TripExpenditureController> _logger;
        //ShipManagemntDbContext _dbContext;
        ShipBL<TripExpenditureDTO> _shipBL;
        IMapper _mapper;

        public TripExpenditureController(ILogger<TripExpenditureController> logger,
                                        //ShipManagemntDbContext dbContext,
                                        ShipBL<TripExpenditureDTO> shipBL,
            IMapper mapper)
        {
            _logger = logger;
            //_dbContext = dbContext;
            _shipBL = shipBL;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All")]
        public async Task<ActionResult<TripExpenditureDTO>> GetAllTripExpenditures()
        {
            _logger.LogInformation(" GetAllTripExpenditures Execution Started");
            //var tripExpenditure = await _dbContext.TripExpenditures.ToListAsync();
            var tripExpenditure = await _shipBL.GetAll();
            var tripExpenditureDTO = _mapper.Map<List<TripExpenditureDTO>>(tripExpenditure);
            _logger.LogInformation("Record Retrived " + tripExpenditure.Count());
            return Ok(tripExpenditureDTO);
        }

        [HttpGet]
        [Route("{Id:int}", Name = "GetTripExpenditureById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TripExpenditureDTO>> GetTripExpenditureById(int Id)
        {
            if (Id < 0)
            {
                _logger.LogError("Salary Id Is not Valid!");
                return BadRequest();
            }

            //var tripExpenditure = await _dbContext.TripExpenditures.Where(trip => trip.TripExpenditureId == Id).FirstOrDefaultAsync();
            var tripExpenditure = await _shipBL.Get(trip => trip.TripExpenditureId == Id);
            if (tripExpenditure == null)
            {
                _logger.LogError($"TripExpenditure info Not Available");
                return NotFound($"TripExpenditure info wiht id:{Id} Not Found!");
            }
            var tripExpenditureDTO = _mapper.Map<TripExpenditureDTO>(tripExpenditure);

            return Ok(tripExpenditureDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("Create")]
        public async Task<ActionResult<TripExpenditureDTO>> AddTripExpenditure([FromBody] TripExpenditureDTO tripExpenditureDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (tripExpenditureDTO == null || tripExpenditureDTO.TripExpenditureId < 0)
            {
                _logger.LogError("Email Id Id not Valid!");
                return BadRequest();
            }
            //var tripExpenditure = await _dbContext.TripExpenditures.Where(trip => trip.TripExpenditureId == tripExpenditureDTO.TripExpenditureId).FirstOrDefaultAsync();
            var tripExpenditure = await _shipBL.Get(trip => trip.TripExpenditureId == tripExpenditureDTO.TripExpenditureId);

            if (tripExpenditure != null)
            {
                _logger.LogInformation("tripExpenditure Info Already Present!");
                return BadRequest("tripExpenditure Info Already Present!");
            }

            TripExpenditure obj = _mapper.Map<TripExpenditure>(tripExpenditureDTO);

            //await _dbContext.TripExpenditures.AddAsync(obj);
            //await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Recorde Added Successfully.ID {tripExpenditureDTO.TripExpenditureId}");

            // Return a 201 Created response
            return CreatedAtRoute("GetTripExpenditureById", new { emailid = tripExpenditureDTO.TripExpenditureId }, tripExpenditureDTO);
        }

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateTripExpenditure(TripExpenditureDTO tripExpenditureDTO)
        {
            if (tripExpenditureDTO == null || tripExpenditureDTO.TripExpenditureId < 0)
            {
                _logger.LogError("TripExpenditure Id is not Valid!");
                return BadRequest();
            }
            //TripExpenditure? tripExpenditure = await _dbContext.TripExpenditures.FirstOrDefaultAsync(trip => trip.TripExpenditureId == tripExpenditureDTO.TripExpenditureId);
            var tripExpenditure = await _shipBL.Get(trip => trip.TripExpenditureId == tripExpenditureDTO.TripExpenditureId);
            if (tripExpenditure == null)
            {
                return BadRequest();
            }

            //tripExpenditure = _mapper.Map<TripExpenditure>(tripExpenditureDTO);
            //await _dbContext.SaveChangesAsync();
            return NoContent();
        }


        [HttpPatch]
        [Route("{Id:alpha}/PartialUpdate")]
        //PartialUpdate")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PartialUpdateTripExpenditure([FromRoute] int id, [FromBody] JsonPatchDocument<TripExpenditure> patchdocument)
        {
            if (patchdocument == null || id < 0)
            {
                return BadRequest();
            }
            //TripExpenditure? tripExpenditure = await _dbContext.TripExpenditures.Where(tripExpenditure => tripExpenditure.TripExpenditureId == id).FirstOrDefaultAsync();
            var tripExpenditure = await _shipBL.Get(tripExpenditure => tripExpenditure.TripExpenditureId == id);
            if (tripExpenditure == null)
            {
                return NotFound();
            }

            var tripExpenditureDTO = _mapper.Map<TripExpenditure>(tripExpenditure);

            // need to apply from patch document
            patchdocument.ApplyTo(tripExpenditureDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //tripExpenditure = _mapper.Map<TripExpenditure>(tripExpenditureDTO);

            //await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("Delete/{Id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteTripExpenditure(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            //TripExpenditure? tripExpenditure = await _dbContext.TripExpenditures.FirstOrDefaultAsync(tripExpense => tripExpense.TripExpenditureId == id);
            var tripExpenditure = await _shipBL.Get(tripExpense => tripExpense.TripExpenditureId == id);
            if (tripExpenditure == null)
            {
                return NotFound($"TripExpenditure With Id:{id} Not Found!!");
            }

            //_dbContext.TripExpenditures.Remove(tripExpenditure);
            //_dbContext.SaveChanges();

            return Ok(true);
        }
    }
}

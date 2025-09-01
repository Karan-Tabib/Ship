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
    public class TripParticularController : ControllerBase
    {
        ILogger<TripParticularController> _logger;
        IShipBL<TripParticular> _shipBL;
        IMapper _mapper;

        public TripParticularController(ILogger<TripParticularController> logger, IShipBL<TripParticular> shipBL,
            IMapper mapper)
        {
            _logger = logger;
            _shipBL = shipBL;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All")]
        public async Task<ActionResult<TripParticularDTO>> GetAllTripParticulars()
        {
            _logger.LogInformation(" GetAllTripParticulars Execution Started");
            //var tripParticulars = await _dbContext.TripParticulars.ToListAsync();
            var tripParticulars = _shipBL.GetAll();
            var tripparticularDTO = _mapper.Map<List<SalaryInfoDTO>>(tripParticulars);
            _logger.LogInformation("Record Retrived " + tripParticulars.Result.Count());
            return Ok(tripparticularDTO);
        }

        [HttpGet]
        [Route("{Id:int}", Name = "GetTripParticularById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TripParticularDTO>> GetTripParticularById(int Id)
        {
            if (Id < 0)
            {
                _logger.LogError("TripParticular Id Is not Valid!");
                return BadRequest();
            }

            //var tripParticular = await _dbContext.TripParticulars.Where(tripParticular => tripParticular.TripParticularId == Id).FirstOrDefaultAsync();
            var tripParticular = _shipBL.Get(tripParticular => tripParticular.TripParticularId == Id);


            if (tripParticular == null)
            {
                _logger.LogError($"TripParticular info Not Available");
                return NotFound($"TripParticular info id:{Id} Not Found!");
            }
            var tripParticularDTO = _mapper.Map<TripParticularDTO>(tripParticular);

            return Ok(tripParticularDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("Create")]
        public async Task<ActionResult<TripParticularDTO>> AddTripParticular([FromBody] TripParticularDTO tripParticular)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (tripParticular == null || tripParticular.TripParticularId < 0)
            {
                _logger.LogError("TripParticular Id is not Valid!");
                return BadRequest();
            }
            //var tripdetails = await _dbContext.TripParticulars.Where(salary => salary.TripParticularId == tripParticular.TripParticularId).FirstOrDefaultAsync();

            var tripdetails = await _shipBL.Get(salary => salary.TripParticularId == tripParticular.TripParticularId);
            if (tripdetails != null)
            {
                _logger.LogInformation("TripParticular Info Already Present!");
                return BadRequest("TripParticular Info Already Present!");
            }

            TripParticular obj = _mapper.Map<TripParticular>(tripParticular);

            //await _dbContext.TripParticulars.AddAsync(obj);
            //await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Recorde Added Successfully.ID {tripParticular.TripParticularId}");

            // Return a 201 Created response
            return CreatedAtRoute("GetTripParticularById", new { emailid = tripParticular.TripParticularId }, tripParticular);
        }

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateTripParticular(TripParticularDTO tripParticularDTo)
        {
            if (tripParticularDTo == null || tripParticularDTo.TripParticularId < 0)
            {
                _logger.LogError("TripParticular Id is not Valid!");
                return BadRequest();
            }
            //TripParticular? tripParticualartobj = await _dbContext.TripParticulars.FirstOrDefaultAsync(trip => trip.TripParticularId == tripParticularDTo.TripParticularId);
            var tripParticualartobj = _shipBL.Get(trip => trip.TripParticularId == tripParticularDTo.TripParticularId);
            if (tripParticualartobj == null)
            {
                return BadRequest();
            }

            //tripParticualartobj = _mapper.Map<TripParticular>(tripParticularDTo);
            //await _dbContext.SaveChangesAsync();
            return NoContent();
        }


        [HttpPatch]
        [Route("{Id:alpha}/PartialUpdate")]
        //PartialUpdate")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PartialUpdateTripParticular([FromRoute] int id, [FromBody] JsonPatchDocument<TripParticularDTO> patchdocument)
        {
            if (patchdocument == null || id < 0)
            {
                return BadRequest();
            }
            //TripParticular? salaryobj = await _dbContext.TripParticulars.Where(salary => salary.TripParticularId == id).FirstOrDefaultAsync();
            var salaryobj = await _shipBL.Get(salary => salary.TripParticularId == id);
            if (salaryobj == null)
            {
                return NotFound();
            }

            var userDTO = _mapper.Map<TripParticularDTO>(salaryobj);

            // need to apply from patch document
            patchdocument.ApplyTo(userDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            salaryobj = _mapper.Map<TripParticular>(userDTO);

            //await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("Delete/{Id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteTripParticular(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            //TripParticular? salaryObj = await _dbContext.TripParticulars.FirstOrDefaultAsync(trip => trip.TripParticularId == id);
            var salaryObj = await _shipBL.Get(trip => trip.TripParticularId == id);
            if (salaryObj == null)
            {
                return NotFound($"TripParticular With Id:{id} Not Found!!");
            }

            //_dbContext.TripParticulars.Remove(salaryObj);
            //_dbContext.SaveChanges();

            return Ok(true);
        }
    }
}

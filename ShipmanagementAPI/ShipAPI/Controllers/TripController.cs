using BusinessLayer.Abstraction;
using DTOs.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShipAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TripController : ControllerBase
    {
        private readonly ITripBL _tripBL;
        public TripController(ITripBL tripBL)
        {
            _tripBL = tripBL;
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddTripData([FromBody] TripDataDTO data)
        {
            if (data == null)
            {
                return BadRequest("Invalid data.");
            }
            Console.WriteLine($"Received TripInfo: {data.TripInfo.TripName}");
            Console.WriteLine($"Number of Trip Particulars: {data.TripParticulars.Count}");
            Console.WriteLine($"Number of Trip Expenditures: {data.TripExpenditures.Count}");
            data = _tripBL.Add(data);
            return Ok(new { message = "Trip data received successfully!" });
        }

        [HttpGet]
        [Route("Get/TripDetails/{tripId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TripDataDTO> GetTripDataById(int tripId)
        {
            if (tripId <=0)
            {
                return BadRequest("Invalid data.");
            }
            TripDataDTO data = _tripBL.GetTripData(tripId);
            return Ok(data);
        }

        [HttpGet]
        [Route("Get/Trip/Boat/{BoatId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<TripInfoDTO>> GetTripInformationByBoatId(int boatId)
        {
            if (boatId <= 0)
            {
                return BadRequest("Invalid data.");
            }
            List<TripInfoDTO> data = _tripBL.GetTripInformation(boatId);
            return Ok(data);
        }
        [HttpGet]
        [Route("Particulars/{tripId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<TripParticularDTO>> GetTripParticularsByTripId(int tripId)
        {
            if (tripId <= 0)
            {
                return BadRequest("Invalid data.");
            }
            List<TripParticularDTO> data = _tripBL.GetTripParticulars(tripId);
            return Ok(data);
        }
        [HttpGet]
        [Route("Expenditures/{tripId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<TripExpenditureDTO>> GetTripExpendituresByTripId(int tripId)
        {
            if (tripId <= 0)
            {
                return BadRequest("Invalid data.");
            }
            List<TripExpenditureDTO> data = _tripBL.GetTripExpenditures(tripId);
            return Ok(data);
        }

        [HttpPatch]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> UpdateTripData(TripDataDTO tripData)
        {
            if(tripData ==null || tripData.TripInfo.TripId < 0)
            {
                return BadRequest("Trip id Not valid");
            }

            return await _tripBL.UpdateTripData(tripData);
        }

        [HttpDelete]
        [Route("Delete/{tripId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> DeleteTripData(int tripId)
        {
            if (tripId  < 0)
            {
                return BadRequest("Trip id Not valid");
            }

            return await _tripBL.DeleteTripData(tripId);
        }
    }
}

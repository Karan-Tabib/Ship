using BusinessLayer.Abstraction;
using BusinessLayer.BusinessBL;
using DTOs.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShipAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FishController : ControllerBase
    {
        private readonly IFishBL _fishBL;

        public FishController(IFishBL fishBL)
        {
            _fishBL = fishBL;
        }

        [HttpGet]
        [Route("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<FishInfoDTO> GetAllFishInfo()
        {
            var fishinfo = _fishBL.GetAll();

            return Ok(fishinfo);
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BoatInfoDTO> AddFishInfo([FromBody] FishInfoDTO fishinfoDTO)
        {
            if (fishinfoDTO == null)
                return BadRequest();

            fishinfoDTO = _fishBL.Add(fishinfoDTO);

            return Ok(fishinfoDTO);
        }

        [HttpDelete]
        [Route("Delete/{fishId:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteFish(int fishId)
        {
            if (fishId < 0)
                return BadRequest();

            bool result = _fishBL.Remove(fishId);

            return result ? Ok(true) : Ok(false);
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<FishInfoDTO> UpdateBoat(FishInfoDTO fishInfoDTO)
        {
            if (fishInfoDTO.FishId < 0)
            {
                return BadRequest($"Boat with BoatId :{fishInfoDTO.FishId} is not valid");
            }
            fishInfoDTO = _fishBL.Update(fishInfoDTO);
            return fishInfoDTO;
        }

        [HttpGet("search")]
        public IActionResult SearchBoats(string query)
        {
            var fish = _fishBL.Search(query);
                
            return Ok(fish);
        }

    }
}

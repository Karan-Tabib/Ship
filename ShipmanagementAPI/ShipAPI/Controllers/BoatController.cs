using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DTOs.DTO;
using DTOs.Models;
using ShipAPI.Utility;
using BusinessLayer.Abstraction;

namespace ShipAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoatController : ControllerBase
    {
        private ILogger<BoatController> _logger;
        private IMapper _mapper;
        private IShipBL<User> _userRepository;
        private IBoatBL _boatBL;

        public BoatController(
                                IBoatBL boatBL,
                                 IShipBL<User> userRepository,
                                 ILogger<BoatController> logger,
                                 IMapper mapper)
        {
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
            _boatBL = boatBL;

        }

        [HttpGet]
        [Route("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<ActionResult<BoatInfoDTO>> GetAllBoatInfo()   //TODO: This will get all info from boat. Every user is accessing all boat info
        {
            var userId = User.FindFirst("UserID")?.Value;
            //User user = await _userRepository.Get(user => user.Email == email);
            //var boatInfo = await _boatRepository.GetAll(boat =>boat.UserId == user.UserId);

            var boatInfo = _boatBL.GetAll(userId);

            //var BoatInfoDTO = _mapper.Map<List<BoatInfoDTO>>(boatInfo);
            _logger.LogInformation("Boat information Records :" + boatInfo.Count());
            return Ok(boatInfo);
        }

        [HttpGet]
        [Route("{boatId:int}", Name = "GetBoatById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BoatInfoDTO>> GetBoatById(int boatId)
        {
            if (boatId <= 0)
            {
                _logger.LogError("Boat Id Is not Valid.");
                return BadRequest();
            }
            BoatInfoDTO boatInfo = _boatBL.Get(boatId);
            if (boatInfo == null)
            {
                _logger.LogError($"Boat Record Not Exist with Boat Id:{boatId}.");
                return NotFound($"Boat Record Not Exist with Boat Id :{boatId}");
            }

            //  BoatInfoDTO boatinfoDTO = _mapper.Map<BoatInfoDTO>(boatInfo);
            return Ok(boatInfo);
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BoatInfoDTO>> AddBoatInfo([FromBody] BoatInfoDTO boatinfoDTO)
        {
            if (boatinfoDTO == null)
            {
                _logger.LogError("Boat info cannot be added. Boat Info is null or ID is not valid");
                return BadRequest();
            }
            var userId = User.FindFirst("UserID")?.Value;
            if (userId != null)
            {
                boatinfoDTO.UserId = Convert.ToInt32(userId);
            }
            //BoatInformation boatInfo = _mapper.Map<BoatInformation>(boatinfoDTO);

            boatinfoDTO = _boatBL.Add(boatinfoDTO);

            _logger.LogInformation($"Recorde Added Successfully.Boat Name: {boatinfoDTO.BoatName}");

            return CreatedAtRoute("GetBoatById", new { boatinfoDTO.BoatId }, boatinfoDTO);
        }


        [HttpDelete]
        [Route("Delete/{boatId:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteBoat(int boatId)
        {
            if (boatId < 0)
            {
                _logger.LogError("Boat info cannot be added. Boat Info is null or ID is not valid");
                return BadRequest();
            }
            //BoatInfoDTO? boatInfo = _boatBL.Remove(boatId);
            //if (boatInfo == null)
            //{
            //    _logger.LogError($"Boat Record not exist.Boat Id :{boatId}");
            //    NotFound($"Boat Record not exist.Boat Id :{boatId}");
            //}

            bool result = _boatBL.Remove(boatId);
            
            return  result ? Ok(true) :Ok(false);
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BoatInfoDTO> UpdateBoat(BoatInfoDTO boatInfoDTO)
        {
            var userId = User.FindFirst("UserID")?.Value;
            if (userId != null)
            {
                boatInfoDTO.UserId = Convert.ToInt32(userId);
            }
            if (boatInfoDTO.BoatId < 0)
            {
                return BadRequest($"Boat with BoatId :{boatInfoDTO.BoatId} is not valid");
            }
            boatInfoDTO = _boatBL.Update(boatInfoDTO);
            return boatInfoDTO;
        }
    }
}

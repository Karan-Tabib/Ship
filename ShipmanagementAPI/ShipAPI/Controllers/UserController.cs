using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using DTOs.DTO;
using DTOs.Models;
using BusinessLayer.Abstraction;

namespace ShipAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        ILogger<UserController> _logger;
        //ShipManagemntDbContext _dbContext;
        IMapper _mapper;
        IShipBL<User> _shipRepository;

        public UserController(ILogger<UserController> logger,
                              // ShipManagemntDbContext dbContext,
                              IShipBL<User> shipRepository,
                              IMapper mapper)
        {
            _logger = logger;
            // _dbContext = dbContext; // implemented common repository pattern 
            _shipRepository = shipRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<UserDTO>> GetAllUser()
        {
            _logger.LogInformation(" GetAllUser Execution Started");
            //var users = await _dbContext.UserDefinition.ToListAsync();
            //fetch from repository now
            var users = await _shipRepository.GetAll();
            //var users = _dbContext.userdefinitiontbl.Select(user => new UserDTO()
            //{
            //    Firstname = user.Firstname,
            //    Middlename = user.Middlename,
            //    Lastname = user.Lastname,
            //    Email = user.Email,
            //    Password = user.Password,
            //    Phone = user.Phone,
            //    Address = user.Address,
            //}).ToList();  // this is need when you want to support multiple format.
            var usersDTO = _mapper.Map<List<UserDTO>>(users);
            _logger.LogInformation("Record Retrived" + users.Count());
            return Ok(usersDTO);
        }

        [HttpGet]
        [Route("{emailid}", Name = "GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDTO>> GetUserById(string emailid)
        {
            if (string.IsNullOrEmpty(emailid))
            {
                _logger.LogError("Email Id Id not Valid!");
                return BadRequest();
            }

            //var user = await _dbContext.UserDefinition.Where(user => user.Email == emailid).FirstOrDefaultAsync();
            var user = await _shipRepository.Get(user => user.Email == emailid);
            if (user == null)
            {
                _logger.LogError("User Not Available");
                return NotFound($"User :{emailid} Not Found!");
            }
            var userDTO = _mapper.Map<UserDTO>(user);
            //var userDTO = new UserDTO()
            //{
            //    Firstname = user.Firstname,
            //    Middlename = user.Middlename,
            //    Lastname = user.Lastname,
            //    Email = user.Email,
            //    Password = user.Password,
            //    Phone = user.Phone,
            //    Address = user.Address
            //};
            return Ok(userDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("Create")]
        public async Task<ActionResult<UserDTO>> AddUser([FromBody] UserDTO userDTo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (userDTo == null || string.IsNullOrEmpty(userDTo.Email))
            {
                _logger.LogError("Email Id is not Valid!");
                return BadRequest();
            }
            var usr = await _shipRepository.Get(user => user.Email == userDTo.Email);

            if (usr != null)
            {
                _logger.LogInformation("User Already Present!");
                return BadRequest("User Already Present!");
            }
            // Proceed to add the user
            //User obj = new User()
            //{
            //    Firstname = userDTo.Firstname,
            //    Middlename = userDTo.Middlename,
            //    Lastname = userDTo.Lastname,
            //    Email = userDTo.Email,
            //    Password = userDTo.Password,
            //    Phone = userDTo.Phone,
            //    Address = userDTo.Address,
            //};

            User obj = _mapper.Map<User>(userDTo);

            //await _dbContext.UserDefinition.AddAsync(obj);
            //await _dbContext.SaveChangesAsync();

            await _shipRepository.Add(obj);

            _logger.LogInformation($"Recorde Added Successfully.Email ID {userDTo.Email}");
            
            // Return a 201 Created response
            return CreatedAtRoute("GetUserById", new { emailid = userDTo.Email }, userDTo);
        }

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateUser(UserDTO userDto)
        {
            if (userDto == null || string.IsNullOrEmpty(userDto.Email))
            {
                _logger.LogError("Email Id Id not Valid!");
                return BadRequest();
            }
            User? userobj = await _shipRepository.Get(user => user.Email == userDto.Email);
            if (userobj == null)
            {
                return BadRequest();
            }
            //userobj.Firstname = userDto.Firstname;
            //userobj.Lastname = userDto.Lastname;
            //userobj.Email = userDto.Email;
            //userobj.Password = userDto.Password;
            //userobj.Phone = userDto.Phone;

            userobj = _mapper.Map<User>(userDto);
            await _shipRepository.Update(userobj);
            return NoContent();
        }


        [HttpPatch]
        [Route("{email:alpha}/PartialUpdate")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PartialUpdateUserAsync([FromRoute] string email, [FromBody] JsonPatchDocument<UserDTO> patchdocument)
        {
            if (patchdocument == null || string.IsNullOrEmpty(email))
            {
                return BadRequest();
            }
            User? userobj = await _shipRepository.Get(user => user.Email == email);
            if (userobj == null)
            {
                return NotFound();
            }

            //var userDTO = new UserDTO()
            //{
            //    Firstname = userobj.Firstname,
            //    Middlename = userobj.Middlename,
            //    Lastname = userobj.Lastname,
            //    Phone = userobj.Phone,
            //    Address = userobj.Address,
            //    Email = userobj.Email,
            //};

            var userDTO = _mapper.Map<UserDTO>(userobj);

            // need to apply from patch document
            patchdocument.ApplyTo(userDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            /*what if something goes wrong when it is applying for that we need to send model state
             * 
             */
            //userobj.Firstname = userDTO.Firstname;
            //userobj.Lastname = userDTO.Lastname;
            //userobj.Email = userDTO.Email;
            //userobj.Password = userDTO.Password;
            //userobj.Phone = userDTO.Phone;

            userobj = _mapper.Map<User>(userDTO);

            await _shipRepository.Update(userobj);

            return NoContent();
        }

        [HttpDelete]
        [Route("Delete/{email:alpha}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteUserAsync(string email)
        {
            if (email == null || string.IsNullOrEmpty(email))
            {
                return BadRequest();
            }

            User? userobj = await _shipRepository.Get(user => user.Email == email);
            if (userobj == null)
            {
                return NotFound($"User With Email Id:{email} Not Found!!");
            }

            //_dbContext.UserDefinition.Remove(userobj);
            //_dbContext.SaveChanges();

            await _shipRepository.Remove(userobj);
            return Ok(true);
        }
    }
}

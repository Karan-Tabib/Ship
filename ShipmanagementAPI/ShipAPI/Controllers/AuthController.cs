using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using DTOs.DTO;
using DTOs.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BusinessLayer.Abstraction;

namespace ShipAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        ILogger<AuthController> _logger;
        //IShipBL<UserDTO> _shipBL;
        IUserBL _userBL;
        private readonly IConfiguration _configuration;

        public AuthController(ILogger<AuthController> logger,
                               //IShipBL<UserDTO> shipRepository,
                               IConfiguration configuration,
                               IUserBL userBL)

        {
            _logger = logger;
           // _shipBL = shipRepository;
            _configuration = configuration;
            _userBL = userBL;       
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponseDTO>> Login(LoginDTO loginDTO)
        {
            try
            {
                if (!ModelState.IsValid || loginDTO == null)
                {
                    _logger.LogError("Please provide Username and Password!");
                    return BadRequest();
                }
                UserDTO user =  _userBL.GetUser(loginDTO);
                if (user == null)
                {
                    _logger.LogError("User does not Exist!");
                    return NotFound();
                }
                LoginResponseDTO response = new LoginResponseDTO() { Username = loginDTO.Username };
                if (user.Email.Equals(loginDTO.Username) && user.Password.Equals(loginDTO.Password))
                {
                    GenerateToken(response,user.UserId.ToString());
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("Invalid Username and Password");
                    return Ok("Invalid Username and Password");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Invalid Username and Password!");
                return BadRequest("Invalid Username and Password!");
            }

        }

        private void GenerateToken(LoginResponseDTO loginResponseDTO,string id)
        {
            var secretekey = _configuration.GetValue<string>("JWTSecretekey");
            var key = Encoding.ASCII.GetBytes(secretekey);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                   new Claim("UserID", id),
                   new Claim("Emailid", loginResponseDTO.Username) 
                    //new Claim(ClaimTypes.Role,"Admin")
                }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            loginResponseDTO.Token = tokenHandler.WriteToken(token);
        }
    }
}

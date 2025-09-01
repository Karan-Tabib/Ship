using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using DTOs.DTO;
using DTOs.Models;
using BusinessLayer.BusinessBL;
using BusinessLayer.Abstraction;
namespace ShipAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        ILogger<SalaryController> _logger;
        IMapper _mapper;
        ShipBL<SalaryInfoDTO> _ShipBL;
        private ISalaryBL _salaryBl;


        public SalaryController(ILogger<SalaryController> logger,
                            ISalaryBL salaryBL,
                            IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _salaryBl = salaryBL;
        }

        [HttpGet]
        [Route("All")]
        public async Task<ActionResult<SalaryInfoDTO>> GetAllSalary()
        {
            var userId = User.FindFirst("UserID")?.Value;
            var salaryInfo = _salaryBl.GetAll(userId);
            return Ok(salaryInfo);
        }

        [HttpGet]
        [Route("{Id:int}", Name = "Salary/Id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SalaryInfoDTO>> GetSalaryById(int Id)
        {
            if (Id < 0)
            {
                _logger.LogError("Salary Id Is not Valid!");
                return BadRequest();
            }

            //var salary = await _dbContext.CrewSalaryInformation.Where(salary => salary.Id == Id).FirstOrDefaultAsync();
            var salary = await _ShipBL.Get(salary => salary.Id == Id);
            if (salary == null)
            {
                _logger.LogError($"Salary info Not Available");
                return NotFound($"Salary info id:{Id} Not Found!");
            }
            var salaryDTO = _mapper.Map<SalaryInfoDTO>(salary);

            return Ok(salaryDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("Create")]
        public async Task<ActionResult<SalaryInfoDTO>> AddSalary([FromBody] SalaryInfoDTO salaryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (salaryDto == null || salaryDto.Id < 0)
            {
                _logger.LogError("Email Id Id not Valid!");
                return BadRequest();
            }

            if (salaryDto.Id == 0)
            {
                var salary = _salaryBl.Add(salaryDto);
            }
            else
            {
                _logger.LogInformation("Salary Info Already Present!");
                return BadRequest("Salary Info Already Present!");
            }

            //SalaryInformation obj = _mapper.Map<SalaryInformation>(salaryDto);

            _logger.LogInformation($"Recorde Added Successfully.ID {salaryDto.Id}");

            // Return a 201 Created response
            return Ok();
        }

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateSalary(SalaryInfoDTO salaryDTo)
        {
            if (salaryDTo == null || salaryDTo.Id < 0)
            {
                _logger.LogError("Email Id Id not Valid!");
                return BadRequest();
            }
            //SalaryInformation? salaryobj = await _dbContext.CrewSalaryInformation.FirstOrDefaultAsync(salary => salary.Id == salaryDTo.Id);
            var salaryobj = _salaryBl.Update(salaryDTo);
            if (salaryobj == null)
            {
                return BadRequest();
            }

            //salaryobj = _mapper.Map<SalaryInformation>(salaryDTo);
            //await _dbContext.SaveChangesAsync();
            return NoContent();
        }


        [HttpPatch]
        [Route("{Id:alpha}/PartialUpdate")]
        //PartialUpdate")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PartialUpdateSalary([FromRoute] int id, [FromBody] JsonPatchDocument<SalaryInfoDTO> patchdocument)
        {
            if (patchdocument == null || id < 0)
            {
                return BadRequest();
            }
            //SalaryInformation? salaryobj = await _dbContext.CrewSalaryInformation.Where(salary => salary.Id == id).FirstOrDefaultAsync();
            var salaryobj = await _ShipBL.Get(salary => salary.Id == id);
            if (salaryobj == null)
            {
                return NotFound();
            }

            var userDTO = _mapper.Map<SalaryInfoDTO>(salaryobj);

            // need to apply from patch document
            patchdocument.ApplyTo(userDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //salaryobj = _mapper.Map<SalaryInformation>(userDTO);

            //await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("Delete/{Id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteSalary(int Id)
        {
            if (Id < 0)
            {
                return BadRequest();
            }

            var salaryObj =  _salaryBl.Remove(Id);
            if (salaryObj == null)
            {
                return NotFound($"Salary With Id:{Id} Not Found!!");
            }


            return Ok(true);
        }
    }
}

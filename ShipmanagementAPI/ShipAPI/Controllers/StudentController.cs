using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShipAPI.DTO;
using ShipAPI.Models;
using ShipAPI.Repository;

namespace ShipAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<StudentDTO>> getStudents()
        {
            var students = StudentRepostiory.Students.Select(stu => new StudentDTO
            {
                Id = stu.Id,
                Name = stu.Name,
                Address = stu.Address,
            });
            return Ok(students);
        }

        [HttpGet]
        [Route("{id:int}",Name = "getStudentById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<StudentDTO> getStudentById(int id)
        {
            //Status Code = 400 -OK
            if (id <= 0)
                return BadRequest();
            var student = StudentRepostiory.Students.Where(s => s.Id == id).FirstOrDefault();

            // Status code 404 - Not Fount
            if (student == null)
                return NotFound($"Student With ID {id} Not Found!");

            var studentDTO = new StudentDTO()
            {
                Id = student.Id,
                Name = student.Name,
                Address = student.Address,
            };
            return Ok(studentDTO);
        }

        [HttpGet]
        [Route("{name:alpha}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> getStudentByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            var student = StudentRepostiory.Students.Where(s => s.Name == name).FirstOrDefault();
            if (student == null)
                return NotFound($"Student with name {name} not found!");

            var studentDTO = new StudentDTO()
            {
                Id = student.Id,
                Name = student.Name,
                Address = student.Address,
            };
            return Ok(studentDTO);
        }

        [HttpDelete]
        [Route("{id}",Name ="remove")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> deleteStudent(int id)
        {
            if (id <= 0)
                return BadRequest();

            var student = StudentRepostiory.Students.Where(s => s.Id == id).FirstOrDefault();
            if (student == null)
                return NotFound($"Student with Id:{id} Not Found");

            StudentRepostiory.Students.Remove(student);
            return true;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> AddStudent([FromBody] StudentDTO studentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //if(studentDTO.AdmissionDate < DateTime.Now)
            //{
            //    ModelState.AddModelError("AdmissionDateError", "Admission date is not valid");
            //    return BadRequest(ModelState);
            //}
            Student student = new Student()
            {
                Id = studentDTO.Id,
                Name = studentDTO.Name,
                Address = studentDTO.Address,
                AdmissionDate = studentDTO.AdmissionDate,
            };

            StudentRepostiory.Students.Add(student);
            return CreatedAtRoute("getStudentById", new { studentDTO.Id}, studentDTO);

        }
    }
}

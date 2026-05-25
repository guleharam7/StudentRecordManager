using Microsoft.AspNetCore.Mvc;
using StudentRecordManagerAPI.Data;
using StudentRecordManagerAPI.Models;
using System.Text.RegularExpressions;

namespace StudentRecordManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentService _service;

        public StudentsController(StudentService service)
        {
            _service = service;
        }

        // GET: api/students
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.GetAll());
        }

        // POST: api/students
        [HttpPost]
        public IActionResult Create([FromBody] Student student)
        {
            if (string.IsNullOrWhiteSpace(student.Name) ||
                string.IsNullOrWhiteSpace(student.RollNumber) ||
                string.IsNullOrWhiteSpace(student.Department))
            {
                return BadRequest("All fields are required");
            }

            if (!Regex.IsMatch(student.RollNumber, "^[0-9]+$"))
            {
                return BadRequest("Roll Number must contain only numbers");
            }

            if (student.Marks < 0 || student.Marks > 100)
            {
                return BadRequest("Marks must be between 0 and 100");
            }

            if (_service.RollNumberExists(student.RollNumber))
            {
                return BadRequest("This Roll Number is already assigned to another student.");
            }

            _service.Create(student);
            return Ok(student);
        }

        // PUT: api/students/{id}
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Student student)
        {
            var existing = _service.GetById(id);

            if (existing == null)
                return NotFound("Student not found");

            if (string.IsNullOrWhiteSpace(student.Name) ||
                string.IsNullOrWhiteSpace(student.RollNumber) ||
                string.IsNullOrWhiteSpace(student.Department))
            {
                return BadRequest("All fields are required");
            }

            if (!Regex.IsMatch(student.RollNumber, "^[0-9]+$"))
            {
                return BadRequest("Roll Number must contain only numbers");
            }

            if (student.Marks < 0 || student.Marks > 100)
            {
                return BadRequest("Marks must be between 0 and 100");
            }

            if (_service.RollNumberExistsForOther(student.RollNumber, id))
            {
                return BadRequest("This Roll Number is already assigned to another student.");
            }

            student.Id = id;
            _service.Update(id, student);

            return Ok("Updated successfully");
        }

        // DELETE: api/students/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var existing = _service.GetById(id);

            if (existing == null)
                return NotFound("Student not found");

            _service.Delete(id);

            return Ok("Student deleted successfully");
        }
    }
}
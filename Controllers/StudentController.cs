using Microsoft.AspNetCore.Mvc;
using StudentRecordManager.Data;
using StudentRecordManager.Models;

namespace StudentRecordManager.Controllers
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

        // GET
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.GetAll());
        }

        // POST 
        [HttpPost]
        public IActionResult Create([FromBody] Student student)
        {
            if (string.IsNullOrWhiteSpace(student.Name) ||
                string.IsNullOrWhiteSpace(student.RollNumber) ||
                string.IsNullOrWhiteSpace(student.Department))
            {
                return BadRequest("All fields are required");
            }

            if (student.Marks < 0 || student.Marks > 100)
            {
                return BadRequest("Marks must be between 0 and 100");
            }

            var result = _service.Create(student);
            return Ok(result);
        }

        // PUT
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

            if (student.Marks < 0 || student.Marks > 100)
            {
                return BadRequest("Marks must be between 0 and 100");
            }

            student.Id = id;
            _service.Update(id, student);

            return Ok("Updated successfully");
        }

        // DELETE
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
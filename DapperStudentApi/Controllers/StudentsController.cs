using DapperStudentApi.Models;
using DapperStudentApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperStudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentRepository _repo;
        public StudentsController(StudentRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _repo.GetAllAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var student = await _repo.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Student student)
        {
            if (student == null || string.IsNullOrWhiteSpace(student.FullName) || student.Age <= 0)
            {
                return BadRequest("Invalid student data.");
            }
            var result = await _repo.CreateAsync(student);
            if (result > 0)
            {
                return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "Error creating student.");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Student student)
        {
            if (student == null || string.IsNullOrWhiteSpace(student.FullName) || student.Age <= 0)
            {
                return BadRequest("Invalid student data.");
            }
            student.Id = id;
            var result = await _repo.UpdateAsync(student);
            if (result > 0)
            {
                return NoContent();
            }
            return NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _repo.DeleteAsync(id);
            if (result > 0)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}

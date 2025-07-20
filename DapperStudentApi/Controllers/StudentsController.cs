using AutoMapper;
using DapperStudentApi.Dtos;
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
        private readonly ILogger<StudentsController> _logger;
        private readonly IMapper _mapper;

        public StudentsController(StudentRepository repo, ILogger<StudentsController> logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _repo.GetAllAsync();
            _logger.LogInformation("Student list retrievet at {Time}", DateTime.Now);
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
        public async Task<IActionResult> Create([FromBody] StudentDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.FullName) || dto.Age <= 0)
            {
                return BadRequest("Invalid student data.");
            }
            var student = _mapper.Map<Student>(dto);
            var result = await _repo.CreateAsync(student);
            if (result > 0)
            {
                return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "Error creating student.");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] StudentDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.FullName) || dto.Age <= 0)
            {
                return BadRequest("Invalid student data.");
            }
            var student = _mapper.Map<Student>(dto);
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

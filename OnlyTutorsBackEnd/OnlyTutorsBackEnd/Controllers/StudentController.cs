using Microsoft.AspNetCore.Mvc;
using OnlyTutorsBackEnd.Contracts;
using OnlyTutorsBackEnd.Models;
using OnlyTutorsBackEnd.ModelsViews;

namespace OnlyTutorsBackEnd.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private IStudentRepository _studentRepository;
        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }


        [HttpGet("")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var students = await _studentRepository.GetStudents();
                if (students.Count() <= 0)
                    return NotFound();

                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{studentid}", Name = "GetStudentById")]
        public async Task<IActionResult> GetStudentById(int studentid)
        {
            try
            {
                var student = await _studentRepository.GetStudentById(studentid);
                if (student == null)
                    return NotFound();

                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("/lesson/{lessondId}", Name ="GetStudentByLessonId")]
        public async Task<IActionResult> GetStudentsByLesson(int lessondId)
        {
            try
            {
                var students = await _studentRepository.GetStudentsByLesson(lessondId);
                if (students.Count() <= 0)
                    return NotFound();

                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostStudent(Student student)
        {
            try
            {
                if (await _studentRepository.InsertStudent(student) == -1)
                    throw new Exception("Cannot insert student");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutStudent(UpdateStudent student)
        {
            try
            {
                if (await _studentRepository.UpdateStudent(student, student.Id) == -1)
                    throw new Exception("Cannot update student");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

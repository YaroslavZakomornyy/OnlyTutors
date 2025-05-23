using Microsoft.AspNetCore.Mvc;
using OnlyTutorsBackEnd.Contracts;
using OnlyTutorsBackEnd.Models;

namespace OnlyTutorsBackEnd.Controllers
{
    [Route("api/lessons")]
    [ApiController]

    public class LessonController : ControllerBase
    {
        private ILessonRepository _lessonRepository;

        public LessonController(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;   
        }

        [HttpGet("")]
        public async Task<IActionResult> GetLessons()
        {
            try
            {
                var lessons= await _lessonRepository.GetLessons();
                if (lessons.Count() <= 0)
                    return NotFound();

                return Ok(lessons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("/api/lessons/search/{searchString}/{openOnly}", Name = "SearchForLesson")]
        public async Task<IActionResult> SearchForLesson(string searchString, int openOnly)
        {
            try
            {
                var lessons = await _lessonRepository.SearchForLessons(searchString, openOnly);
                if (lessons == null)
                    return NotFound();

                return Ok(lessons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("/api/lessons/addlesson")]
        public async Task<IActionResult> CreateLesson(Lesson lesson)
        {
            try
            {
                if (await _lessonRepository.InsertLesson(lesson) == -1)
                    throw new Exception("Cannot insert lesson");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("/api/lessons/addstudent")]
        public async Task<IActionResult> CreateStundetLesson(int studentid, int lessonid)
        {
            try
            {
                if (await _lessonRepository.InsertStudentLessons(studentid, lessonid)== -1)
                    throw new Exception("Cannot insert lesson");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("/api/lessons/{lessonid}", Name = "GetLessonStudents")]
        public async Task<IActionResult> GetLessonStudents(int lessonid)
        {
            try
            {
                var students = await _lessonRepository.GetAllLessonStudents(lessonid);
                if (students == null)
                    return NotFound();

                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("/api/lessons/removestudent")]
        public async Task<IActionResult> DeleteStudentLesson(int studentid, int lessonid)
        {
            try
            {
                if (await _lessonRepository.RemoveStudentLessons(studentid, lessonid) == -1)
                    throw new Exception("Cannot remove student from lesson");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

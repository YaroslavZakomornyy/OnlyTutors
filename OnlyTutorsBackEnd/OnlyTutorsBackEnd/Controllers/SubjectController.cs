using Microsoft.AspNetCore.Mvc;
using OnlyTutorsBackEnd.Contracts;
using OnlyTutorsBackEnd.Models;

namespace OnlyTutorsBackEnd.Controllers
{
    [Route("api/subjects")]
    [ApiController]

    public class SubjectController : ControllerBase
    {
        private ISubjectRepository _subjectRepository;

        public SubjectController(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetSubjects()
        {
            try
            {
                var subjects = await _subjectRepository.GetSubjects();
                if (subjects.Count() <= 0)
                    return NotFound();

                return Ok(subjects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{lessonid}", Name = "GetSubjectByLessonId")]
        public async Task<IActionResult> GetSubject(int lessonid)
        {
            try
            {
                var subject= await _subjectRepository.GetSubjectByLessonId(lessonid);
                if (subject == null)
                    return NotFound();

                return Ok(subject);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostSubject(Subject subject)
        {
            try
            {
                if (await _subjectRepository.InsertSubject(subject) == -1)
                    throw new Exception("Cannot insert subject");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentEnrolment.Core.Models;
using StudentEnrolment.Core.Services;
using StudentEnrolment.Core.Services.Interfaces;

namespace StudentEnrolment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICoursesService _coursesService;

        public CoursesController(ICoursesService coursesService)
        {
            _coursesService = coursesService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<CourseModel>> GetCourses()
        {
            List<CourseModel>? courses = _coursesService.GetCourses().ToList();
            return courses == null ? NotFound() : Ok(courses);
        }
    }
}

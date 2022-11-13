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
            List<CourseModel> courses = _coursesService.GetAllCourses().ToList();
            return courses.Count() == 0 ? Problem(detail: "Either the db is empty or some error occurred", statusCode: 404) : Ok(courses);
        }
    }
}

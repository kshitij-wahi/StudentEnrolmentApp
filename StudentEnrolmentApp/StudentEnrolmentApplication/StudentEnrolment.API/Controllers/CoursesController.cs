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
        public async Task<ActionResult<List<CourseModel>>> GetCourses()
        {
            List<CourseModel> courses = await _coursesService.GetCoursesAsync();
            return courses;
        }
    }
}

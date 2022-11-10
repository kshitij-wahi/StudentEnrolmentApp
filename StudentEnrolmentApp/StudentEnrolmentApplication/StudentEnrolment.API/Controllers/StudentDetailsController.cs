using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentEnrolment.Core.Models;
using StudentEnrolment.Core.Services.Interfaces;
using StudentEnrolment.Infrastructure.Services;
using StudentEnrolment.Infrastructure.Services.Interfaces;
using System.Text.Json;

namespace StudentEnrolment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentDetailsController : ControllerBase
    {
        private readonly IStudentDetailsService _studentDetailsService;

        public StudentDetailsController(IStudentDetailsService studentDetailsService)
        {
            _studentDetailsService = studentDetailsService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<StudentDetailsModel>> GetStudentDetails()
        {
            List<StudentDetailsModel>? studentDetails = _studentDetailsService.GetStudentDetails();
            return studentDetails == null ? NotFound() : Ok(studentDetails);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<StudentDetailsModel>> AddStudentDetails(StudentDetailsModel newStudentDetails)
        {
            List<StudentDetailsModel> studentDetails = _studentDetailsService.AddStudentDetails(newStudentDetails);
            return studentDetails == null ? NotFound() : Ok(studentDetails); ;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<StudentDetailsModel>> UpdateStudentDetails(StudentDetailsModel updatedStudentDetails)
        {
            List<StudentDetailsModel> studentDetails = _studentDetailsService.UpdateStudentDetails(updatedStudentDetails);
            return studentDetails;
        }
    }
}

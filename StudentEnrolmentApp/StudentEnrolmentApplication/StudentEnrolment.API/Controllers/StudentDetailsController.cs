using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentEnrolment.Core.Models;
using StudentEnrolment.Core.Services.Interfaces;
using StudentEnrolment.Infrastructure.Services;
using StudentEnrolment.Infrastructure.Services.Interfaces;
using System.Collections.Generic;
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
        public ActionResult<IEnumerable<StudentDetailsModel>> GetStudentDetails()
        {
            List<StudentDetailsModel> studentDetails = _studentDetailsService.GetAllStudentDetails().ToList();
            return studentDetails.Count() == 0 ? Problem(detail: "Either the db is empty or some error occurred", statusCode: 404) : Ok(studentDetails);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult AddStudentDetails(StudentDetailsModel newStudentDetails)
        {
            bool isAdded = _studentDetailsService.AddStudentDetails(newStudentDetails);
            return isAdded ? Ok() : Problem("Some error occured while adding student details", statusCode: 500);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateStudentDetails(StudentDetailsModel updatedStudentDetails)
        {
            bool isUpdated = _studentDetailsService.UpdateStudentDetails(updatedStudentDetails);
            return isUpdated ? Ok() : Problem("Some error occured while updating student details", statusCode: 500); ;
        }
    }
}

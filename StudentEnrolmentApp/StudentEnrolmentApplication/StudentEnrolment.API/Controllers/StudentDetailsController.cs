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
        public async Task<ActionResult<List<StudentDetailsModel>>> GetStudentDetails()
        {
            List<StudentDetailsModel> studentDetails = await _studentDetailsService.GetStudentDetailsAsync();
            return studentDetails;
        }

        [HttpPost]
        public async Task<List<StudentDetailsModel>> AddStudentDetails(StudentDetailsModel newStudentDetails)
        {
            List<StudentDetailsModel> studentDetails = await _studentDetailsService.AddStudentDetailsAsync(newStudentDetails);
            return studentDetails;
        }

        [HttpPut]
        public async Task<List<StudentDetailsModel>> UpdateStudentDetails(StudentDetailsModel updatedStudentDetails)
        {
            List<StudentDetailsModel> studentDetails = await _studentDetailsService.UpdateStudentDetailsAsync(updatedStudentDetails);
            return studentDetails;
        }
    }
}

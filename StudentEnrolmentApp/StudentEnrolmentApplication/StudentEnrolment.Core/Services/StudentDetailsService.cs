using StudentEnrolment.Core.Models;
using StudentEnrolment.Core.Services.Interfaces;
using StudentEnrolment.Infrastructure.Services.Interfaces;  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrolment.Core.Services
{
    public class StudentDetailsService : IStudentDetailsService
    {
        private readonly IRepositoryService<StudentDetailsModel> _repositoryService;

        public StudentDetailsService(IRepositoryService<StudentDetailsModel> repositoryService)
        {
            _repositoryService = repositoryService;
        }
        public async Task<List<StudentDetailsModel>> GetStudentDetailsAsync()
        {
            // the file name logic is there till we have json files as db
            List<StudentDetailsModel> studentDetails = await _repositoryService.GetAsync("Students");
            return studentDetails;
        }

        public async Task<List<StudentDetailsModel>> AddStudentDetailsAsync(StudentDetailsModel newStudentDetails)
        {
            await _repositoryService.AddAsync("Students", newStudentDetails);
            List<StudentDetailsModel> studentDetails = await _repositoryService.GetAsync("Students");
            return studentDetails;
        }

        public async Task<List<StudentDetailsModel>> UpdateStudentDetailsAsync(StudentDetailsModel updatedStudentDetails)
        {
            List<StudentDetailsModel> studentDetails = await _repositoryService.GetAsync("Students");
            var studentDetailsToUpdate = studentDetails.FirstOrDefault(item => item.StudentId == updatedStudentDetails.StudentId);
            var index = studentDetails.IndexOf(studentDetailsToUpdate);
            if (index != -1)
                studentDetails[index] = updatedStudentDetails;
            await _repositoryService.UpdateAsync("Students", studentDetails);
            studentDetails = await _repositoryService.GetAsync("Students");
            return studentDetails;
        }
    }
}

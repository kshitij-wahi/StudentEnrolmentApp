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
        public List<StudentDetailsModel> GetStudentDetails()
        {
            // the file name logic is there till we have json files as db
            List<StudentDetailsModel> studentDetails = _repositoryService.Get("Students");
            return studentDetails;
        }

        public List<StudentDetailsModel> AddStudentDetails(StudentDetailsModel newStudentDetails)
        {

            _repositoryService.Add("Students", newStudentDetails);
            List<StudentDetailsModel> studentDetails = _repositoryService.Get("Students");
            return studentDetails;
        }

        // We have kept the firstordefault logic here in the
        // service. Ideally it should be repository but finding
        // id in generic repository is tricky. In dbset based 
        // repo we make a baseentity class and define an id in it.
        // Here if we will try to that, there would be a mapping
        // required between our model class ids and baseentity class ids.
        public List<StudentDetailsModel> UpdateStudentDetails(StudentDetailsModel updatedStudentDetails)
        {
            List<StudentDetailsModel> studentDetails = _repositoryService.Get("Students");
            var studentDetailsToUpdate = studentDetails.FirstOrDefault(item => item.StudentId == updatedStudentDetails.StudentId);
            var index = studentDetails.IndexOf(studentDetailsToUpdate);
            if (index != -1)
                studentDetails[index] = updatedStudentDetails;
            _repositoryService.Update("Students", studentDetails);
            studentDetails = _repositoryService.Get("Students");
            return studentDetails;
        }
    }
}

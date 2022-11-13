using StudentEnrolment.Core.Models;
using StudentEnrolment.Core.Services.Interfaces;
using StudentEnrolment.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace StudentEnrolment.Core.Services
{
    public class StudentDetailsService : IStudentDetailsService
    {
        private readonly IRepositoryService<StudentDetailsModel> _repositoryService;
        private readonly ILogger<StudentDetailsService> _logger;

        public StudentDetailsService(IRepositoryService<StudentDetailsModel> repositoryService, ILogger<StudentDetailsService> logger)
        {
            _repositoryService = repositoryService;
            _logger = logger;
        }
        public IEnumerable<StudentDetailsModel> GetStudentDetails()
        {
            try
            {
                // the file name logic is there till we have json files as db
                IEnumerable<StudentDetailsModel> studentDetails = _repositoryService.Get("Students");
                return studentDetails;

            }
            catch (Exception e)
            {
                _logger.LogError($"Error while trying to call GetStudentDetails in StudentDetailsService. Error Message - {e}");
                throw;
            }

        }

        public bool AddStudentDetails(StudentDetailsModel newStudentDetails)
        {
            bool isAdded = false;
            try
            {
                //GenerateIndices(newStudentDetails);
                _repositoryService.Add("Students", newStudentDetails);
                isAdded = true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while trying to call AddStudentDetails in StudentDetailsService. Error Message - {e}");
                throw;
            }
            return isAdded;
        }

        // We have kept the firstordefault logic here in the
        // service. Ideally it should be repository but finding
        // id in generic repository is tricky. In dbset based 
        // repo we make a baseentity class and define an id in it.
        // Here if we will try to that, there would be a mapping
        // required between our model class ids and baseentity class ids.
        public bool UpdateStudentDetails(StudentDetailsModel updatedStudentDetails)
        {
            bool isUpdated = false;
            try
            {
                List<StudentDetailsModel> studentDetails = _repositoryService.Get("Students").ToList();
                var studentDetailsToUpdate = studentDetails.FirstOrDefault(item => item.StudentId == updatedStudentDetails.StudentId);
                var index = studentDetails.IndexOf(studentDetailsToUpdate);
                if (index != -1)
                    studentDetails[index] = updatedStudentDetails;
                 _repositoryService.Update("Students", studentDetails);
                isUpdated = true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while trying to call UpdateStudentDetails in StudentDetailsService. Error Message - {e}");
                throw;
            }
            return isUpdated;
        }

        //private GenerateIndices(newStudentDetails)
        //{

        //}
    }
}

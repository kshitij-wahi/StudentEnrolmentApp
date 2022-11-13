using Microsoft.Extensions.Logging;
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
    public class CoursesService : ICoursesService
    {
        private readonly IRepositoryService<CourseModel> _repositoryService;
        private readonly ILogger<CoursesService> _logger;

        public CoursesService(IRepositoryService<CourseModel> repositoryService, ILogger<CoursesService> logger)
        {
            _repositoryService = repositoryService;
            _logger = logger;

        }
        public IEnumerable<CourseModel> GetAllCourses()
        {
            IEnumerable<CourseModel> courses = new List<CourseModel>();
            try
            {
                // the file name logic is there till we have json files as db
                courses = _repositoryService.Get("Courses");
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while trying to call GetCourses in CoursesService. Error Message - {e}");
                throw;
            }
            return courses;

        }
    }
}

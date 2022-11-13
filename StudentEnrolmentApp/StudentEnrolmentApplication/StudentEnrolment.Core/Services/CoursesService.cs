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

        public CoursesService(IRepositoryService<CourseModel> repositoryService)
        {
            _repositoryService = repositoryService;
        }
        public IEnumerable<CourseModel> GetCourses()
        {
            // the file name logic is there till we have json files as db
            IEnumerable<CourseModel> courses = _repositoryService.Get("Courses");
            return courses;
        }
    }
}

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
        public async Task<List<CourseModel>> GetCoursesAsync()
        {
            // the file name logic is there till we have json files as db
            List<CourseModel> courses = await _repositoryService.GetAsync("Courses");
            return courses;
        }
    }
}

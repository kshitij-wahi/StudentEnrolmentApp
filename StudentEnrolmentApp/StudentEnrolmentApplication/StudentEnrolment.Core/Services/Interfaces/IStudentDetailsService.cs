using StudentEnrolment.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrolment.Core.Services.Interfaces
{
    public interface IStudentDetailsService
    {
        Task<List<StudentDetailsModel>> GetStudentDetailsAsync();
        Task<List<StudentDetailsModel>> AddStudentDetailsAsync(StudentDetailsModel newStudentDetails);
        Task<List<StudentDetailsModel>> UpdateStudentDetailsAsync(StudentDetailsModel updatedStudentDetails);
    }
}

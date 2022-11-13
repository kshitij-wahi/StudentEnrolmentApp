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
        IEnumerable<StudentDetailsModel>? GetStudentDetails();
        bool AddStudentDetails(StudentDetailsModel newStudentDetails);
        bool UpdateStudentDetails(StudentDetailsModel updatedStudentDetails);
    }
}

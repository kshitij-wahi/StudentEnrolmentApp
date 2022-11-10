using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrolment.Core.Models
{
    public class StudentDetailsModel
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string KnownAs { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string DateOfBirth { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string UniversityEmail { get; set; } = string.Empty;
        public string NetworkId { get; set; } = string.Empty;
        public string HomeOrOverseas { get; set; } = string.Empty;
        public List<EnrolmentDetailsModel>? CourseEnrolment { get; set; }
    }
}

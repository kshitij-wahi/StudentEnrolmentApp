using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrolment.Core.Models
{
    public class EnrolmentDetailsModel
    {
        public string EnrolmentId { get; set; } = string.Empty;
        public string AcademicYear { get; set; } = string.Empty;
        public string YearOfStudy { get; set; } = string.Empty;
        public string Occurrence { get; set; } = string.Empty;
        public string ModeOfAttendance { get; set; } = string.Empty;
        public string EnrolmentStatus { get; set; } = string.Empty;
        public string CourseEntryDate { get; set; } = string.Empty;
        public string ExpectedEndDate { get; set; } = string.Empty;
        public CourseModel Course { get; set; } = new CourseModel();

    }
}

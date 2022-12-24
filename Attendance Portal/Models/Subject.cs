using System.ComponentModel.DataAnnotations;

namespace Attendance_Portal.Models
{
    public class Subject
    {
        [Key]
        public short SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public short SemesterCode { get; set; }
        public string CourseCode { get; set; }
    }
}

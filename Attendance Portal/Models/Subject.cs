using System.ComponentModel.DataAnnotations;

namespace Attendance_Portal.Models
{
    public class Subject
    {
        [Key]
        public string SubCode { get; set; }
        public string SubName { get; set; }
        public int SemCode { get; set; }
        public string CourseCode { get; set; }
    }
}

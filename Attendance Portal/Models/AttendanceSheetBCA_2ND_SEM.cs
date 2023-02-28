using System.ComponentModel.DataAnnotations;

namespace Attendance_Portal.Models
{
    public class AttendanceSheetBCA_2ND_SEM
    {
        [Key]
        public int StudentCode { get; set; }
        public string? AEC2 { get; set; }
        public string? CC3 { get; set; }
        public string? CC4 { get; set; }
        public string? GEIC2 { get; set; }
    }
}

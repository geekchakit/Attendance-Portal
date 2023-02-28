using System.ComponentModel.DataAnnotations;

namespace Attendance_Portal.Models
{
    public class AttendanceSheetBCA_6TH_SEM
    {
        [Key]
        public int StudentCode { get; set; }
        public string? CC13 { get; set; }
        public string? CC14 { get; set; }
        public string? DSEC3 { get; set; }
        public string? DSEC4 { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Attendance_Portal.Models
{
    public class AttendanceSheetBCA_3RD_SEM
    {
        [Key]
        public int StudentCode { get; set; }

        public string? CC5 { get; set; }
        public string? CC6 { get; set; }
        public string? CC7 { get; set; }
        public string? SEC1 { get; set; }
        public string? GEIC3 { get; set; }
    }
}

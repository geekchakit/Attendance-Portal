using System.ComponentModel.DataAnnotations;

namespace Attendance_Portal.Models
{
    public class AttendanceSheetBBA_1ST_SEM
    {
        [Key]
        public int StudentCode { get; set; }

        public string? AEC1 { get; set; }
        public string? CC1 { get; set; }
        public string? CC2 { get; set; }
        public string? GEIC1 { get; set; }
        public string? AEC2 { get; set; }
        public string? CC3 { get; set; }
        public string? CC4 { get; set; }
        public string? GEIC2 { get; set; }
    }
}

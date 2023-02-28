using System.ComponentModel.DataAnnotations;

namespace Attendance_Portal.Models
{
    public class AttendanceSheetBCA_4TH_SEM
    {
        [Key]
        public int StudentCode { get; set; }
        public string? CC8 { get; set; }
        public string? CC9 { get; set; }
        public string? CC10 { get; set; }
        public string? SEC2 { get; set; }
        public string? GEIC4 { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Attendance_Portal.Models
{
    public class AttendanceSheetBBA_6TH_SEM
    {
        [Key]
        public int StudentCode { get; set; }

        public string? B601 { get; set; }
        public string? B602 { get; set; }
        public string? B603 { get; set; }
        public string? B604 { get; set; }
        public string? B605 { get; set; }
        public string? B606 { get; set; }

    }
}

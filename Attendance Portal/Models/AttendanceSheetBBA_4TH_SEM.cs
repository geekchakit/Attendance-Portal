using System.ComponentModel.DataAnnotations;

namespace Attendance_Portal.Models
{
    public class AttendanceSheetBBA_4TH_SEM
    {
        [Key]
        public int StudentCode { get; set; }

        public string? B401 { get; set; }
        public string? B402 { get; set; }
        public string? B403 { get; set; }
        public string? B404 { get; set; }
        public string? B405 { get; set; }

    }
}

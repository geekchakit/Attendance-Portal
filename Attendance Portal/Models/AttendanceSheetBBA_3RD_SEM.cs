using System.ComponentModel.DataAnnotations;

namespace Attendance_Portal.Models
{
    public class AttendanceSheetBBA_3RD_SEM
    {
        [Key]
        public int StudentCode { get; set; }

        public string? B301 { get; set; }
        public string? B302 { get; set; }
        public string? B303 { get; set; }
        public string? B304 { get; set; }
        public string? B305 { get; set; }

    }
}

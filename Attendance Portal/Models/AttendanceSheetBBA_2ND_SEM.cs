using System.ComponentModel.DataAnnotations;

namespace Attendance_Portal.Models
{
    public class AttendanceSheetBBA_2ND_SEM
    {
        [Key]
        public int StudentCode { get; set; }

        public string? B201 { get; set; }
        public string? B202 { get; set; }
        public string? B203 { get; set; }
        public string? B204 { get; set; }

    }
}

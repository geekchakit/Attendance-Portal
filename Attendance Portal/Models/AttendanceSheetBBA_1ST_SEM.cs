using System.ComponentModel.DataAnnotations;

namespace Attendance_Portal.Models
{
    public class AttendanceSheetBBA_1ST_SEM
    {
        [Key]
        public int StudentCode { get; set; }

        public string? B101 { get; set; }
        public string? B102 { get; set; }
        public string? B103 { get; set; }
        //public string? GEIC1 { get; set; }

    }
}

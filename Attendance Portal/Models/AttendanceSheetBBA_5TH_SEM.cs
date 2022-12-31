using System.ComponentModel.DataAnnotations;

namespace Attendance_Portal.Models
{
    public class AttendanceSheetBBA_5TH_SEM
    {
        [Key]
        public int StudentCode { get; set; }

        public string? B501 { get; set; }
        public string? B502 { get; set; }
        //public string? CC2 { get; set; }
       // public string? GEIC1 { get; set; }

    }
}

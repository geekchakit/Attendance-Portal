using System.ComponentModel.DataAnnotations;

namespace Attendance_Portal.Models
{
    public class AttendanceSheetBCA_5TH_SEM
    {
        [Key]
        public int StudentCode { get; set; }

        public string? CC11 { get; set; }
        public string? CC12 { get; set; }
        public string? DSE1 { get; set; }
        public string? DSE2 { get; set; }

    }
}

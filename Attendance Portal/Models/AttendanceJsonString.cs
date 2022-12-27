namespace Attendance_Portal.Models
{
    public class AttendanceJsonString
    {
        public string AttendanceCode { get; set; }
        public string AttedanceDateTime { get; set; }
        public string TimeSlot { get; set; }
        public string? Remarks { get; set; }
    }
}

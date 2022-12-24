namespace Attendance_Portal.ViewModels
{
    public class AttendanceRecord
    {
        public int StudentCode { get; set; }
        public string StudentName { get; set; }
        public double ParentsNumber { get; set; }
        public double ClassRoll { get; set; }
        public int Present { get; set; }
        public int Absent { get; set; }
        public int Late { get; set; }
        public int TotalClass { get; set; }
        public int Percentage { get; set; }
    }
}

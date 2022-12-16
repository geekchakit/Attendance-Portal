namespace Attendance_Portal.Models
{
    public class Studentinfo
    {
        public string StudentName { get; set; }
        public long StudentCode { get; set; }
        public DateTime DOB { get; set; }
        public bool Gender { get; set; }
        public bool Course { get; set; }
        public int Year { get; set; }
        public string StudentEmail { get; set; }
        public string ClassRollno { get; set; }
        public string UniversityRollno { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public long ParentsMobileno { get; set; }
        public long StudentsMobileno{ get; set; }
        public string PassPortPhoto { get; set; }

    }
}

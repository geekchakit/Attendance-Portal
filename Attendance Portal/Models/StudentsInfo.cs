using System.ComponentModel.DataAnnotations;

namespace Attendance_Portal.Models
{
    public class StudentsInfo
    {
        public string StudentName { get; set; }
        [Key]
        public int StudentCode { get; set; }
       // public string DOB { get; set; }
        public string Gender { get; set; }
        public string Course { get; set; }
        public short Year { get; set; }
        public string StudentEmail { get; set; }
        public string ClassRollno { get; set; }
        public string? UniversityRollno { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string ParentsMobileno { get; set; }
        public string StudentsMobileno{ get; set; }
        //public string PassPortPhoto { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;
namespace Attendance_Portal.Models
{
    public class Teacherinfo
    {
        [Key]
        public string EmployeeID { get; set; }
        public string Name { get; set; }
        //public bool Gender { get; set; }
        public string DOB { get; set; }
        public long Phonenumber { get; set; }
        public String Email{ get; set; }
        public string Subject { get; set; }
        //public string SubjectName { get; set; }
        public string BloodGroup { get; set; }
        public short Nature { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public string Password { get; set; }

    }
}

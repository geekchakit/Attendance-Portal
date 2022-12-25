using Attendance_Portal.ViewModels;

namespace Attendance_Portal.Models
{
    public class TeacherSidebarVM
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public List<SubjectSideBar> SubjectSideBar { get; set; }
    }
}

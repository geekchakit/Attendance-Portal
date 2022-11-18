using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Attendance_Portal.Controllers
{
    public class StudentAttendanceController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult _studentlist()
        {
            return PartialView();
        }

        public IActionResult AttendanceRecord()
        {
            return View();
        }

        public IActionResult _AttendanceRecord()
        {
            return PartialView();
        }
    }
}

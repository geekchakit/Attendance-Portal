using Attendance_Portal.Models;
using Attendance_Portal.ViewModels;
using freecode.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;

namespace Attendance_Portal.Controllers
{
    [Authorize]
    public class StudentAttendanceController : Controller
    {
        private readonly ApplicationDbContext _db;
        public StudentAttendanceController(ApplicationDbContext db)
        {
            _db = db;
        }
             
        public IActionResult Index(string EmployeeId)
        {
            ViewBag.EmployeeCode = EmployeeId;
            return View();
        }
        public IActionResult _SideBar(string EmployeeCode)
        {
            Teacherinfo teacherinfos = _db.Teacherinfo.Where(x => x.EmployeeID == EmployeeCode).FirstOrDefault();
            TeacherSidebarVM teacherSidebar = new TeacherSidebarVM();
            List<SubjectSideBar> SubjectList = new();
            List<SubjectSideBar> list = new();
            list = list = JsonConvert.DeserializeObject<List<SubjectSideBar>>(teacherinfos.Subject);
            foreach (var obj in list)
            {
                SubjectSideBar subject = new SubjectSideBar();
                subject.SubName= obj.SubName;
                SubjectList.Add(subject);
            }
            teacherSidebar.Name = teacherinfos.Name;
            teacherSidebar.Image = teacherinfos.Image;
            teacherSidebar.SubjectSideBar = SubjectList;
            return PartialView(teacherSidebar);
        }

		public IActionResult _studentlist(string? SubjectCode,string? CurrentDate,string TimeSlot)
        {           
            List<StudentsInfo> StudentsInfo = _db.StudentsInfo.ToList();
            List<StudentList> StudentList = new();
            foreach (var obj in StudentsInfo)
            {
                List<AttendanceJsonString> list = new();
                string CurrentAttendanceCode=null;
                StudentList students = new StudentList();
                AttendanceSheetBCA_1ST_SEM RecordCurrent = _db.AttendanceSheetBCA_1ST_SEM.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();

                if (SubjectCode == "AEC1" && RecordCurrent.AEC1!=null)
                {
                    list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.AEC1);
                }
                else if(SubjectCode == "CC1" && RecordCurrent.CC1 != null)
                {
                    list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.CC1);
                }
                else if(SubjectCode == "CC2" && RecordCurrent.CC2 != null)
                {
                    list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.CC2);
                }
                else
                {
                    if(RecordCurrent.GEIC1!= null)
                    {
                        list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.GEIC1);
                    }
                }

                foreach (var li in list)
                {
                    if(li.AttedanceDateTime==CurrentDate && li.TimeSlot == TimeSlot)
                    {
                        CurrentAttendanceCode = li.AttendanceCode;
                        break;
                    }
                }
                students.StudentName = obj.StudentName;
                students.StudentCode = obj.StudentCode;
                students.ParentsNumber = obj.ParentsMobileno;
                students.CurrentAttendanceCode = CurrentAttendanceCode;
                StudentList.Add(students);
            }
            return PartialView(StudentList);
        }

        public IActionResult MarkAttendance(int StudentCode,string TimeSlot, string Semester,string CourseCode, string SubjectCode, string AttendaceDateTime, string AttendanceCode)
        {
            AttendanceSheetBCA_1ST_SEM? AttendaceData = _db.AttendanceSheetBCA_1ST_SEM.Where(x => x.StudentCode == StudentCode).First();
            List<AttendanceJsonString>? list = new();

            if (AttendaceData != null)
            {
                if (SubjectCode == "AEC1")
                {
                    if (AttendaceData.AEC1 != null)
                    {
                        list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.AEC1);
                    }
                    AttendanceJsonString NewData = new()
                    {
                        AttendanceCode = AttendanceCode,
                        AttedanceDateTime = AttendaceDateTime,
                        TimeSlot=TimeSlot
                    };
                    list.Add(NewData);
                    AttendaceData.AEC1 = JsonConvert.SerializeObject(list);
                    _db.AttendanceSheetBCA_1ST_SEM.Update(AttendaceData);
                    _db.SaveChanges();
                    return Json("Sucess");
                }
                else if (SubjectCode == "CC1")
                {
                    if (AttendaceData.CC1 != null)
                    {
                        list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.CC1);
                    }
                    AttendanceJsonString NewData = new()
                    {
                        AttendanceCode = AttendanceCode,
                        AttedanceDateTime = AttendaceDateTime,
                        TimeSlot = TimeSlot
                    };
                    list.Add(NewData);
                    AttendaceData.CC1 = JsonConvert.SerializeObject(list);
                    _db.AttendanceSheetBCA_1ST_SEM.Update(AttendaceData);
                    _db.SaveChanges();
                    return Json("Sucess");
                }
                else if (SubjectCode == "CC2")
                {
                    if (AttendaceData.CC2 != null)
                    {
                        list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.CC2);
                    }
                    AttendanceJsonString NewData = new()
                    {
                        AttendanceCode = AttendanceCode,
                        AttedanceDateTime = AttendaceDateTime,
                        TimeSlot = TimeSlot
                    };
                    list.Add(NewData);
                    AttendaceData.CC2 = JsonConvert.SerializeObject(list);
                    _db.AttendanceSheetBCA_1ST_SEM.Update(AttendaceData);
                    _db.SaveChanges();
                    return Json("Sucess");
                }
                else if (SubjectCode == "GEIC1")
                {
                    if (AttendaceData.GEIC1 != null)
                    {
                        list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.GEIC1);
                    }
                    AttendanceJsonString NewData = new()
                    {
                        AttendanceCode = AttendanceCode,
                        AttedanceDateTime = AttendaceDateTime,
                        TimeSlot= TimeSlot
                    };
                    list.Add(NewData);
                    AttendaceData.GEIC1 = JsonConvert.SerializeObject(list);
                    _db.AttendanceSheetBCA_1ST_SEM.Update(AttendaceData);
                    _db.SaveChanges();
                    return Json("Sucess");
                }
                else if (SubjectCode == "AEC2")
                {
                    if (AttendaceData.AEC2 != null)
                    {
                        list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.AEC2);
                    }
                    AttendanceJsonString NewData = new()
                    {
                        AttendanceCode = AttendanceCode,
                        AttedanceDateTime = AttendaceDateTime,
                        TimeSlot = TimeSlot
                    };
                    list.Add(NewData);
                    AttendaceData.AEC2 = JsonConvert.SerializeObject(list);
                    _db.AttendanceSheetBCA_1ST_SEM.Update(AttendaceData);
                    _db.SaveChanges();
                    return Json("Sucess");
                }
                else if (SubjectCode == "CC3")
                {
                    if (AttendaceData.CC3 != null)
                    {
                        list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.CC3);
                    }
                    AttendanceJsonString NewData = new()
                    {
                        AttendanceCode = AttendanceCode,
                        AttedanceDateTime = AttendaceDateTime,
                        TimeSlot = TimeSlot
                    };
                    list.Add(NewData);
                    AttendaceData.CC3 = JsonConvert.SerializeObject(list);
                    _db.AttendanceSheetBCA_1ST_SEM.Update(AttendaceData);
                    _db.SaveChanges();
                    return Json("Sucess");
                }
                else if (SubjectCode == "CC4")
                {
                    if (AttendaceData.CC4 != null)
                    {
                        list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.CC4);
                    }
                    AttendanceJsonString NewData = new()
                    {
                        AttendanceCode = AttendanceCode,
                        AttedanceDateTime = AttendaceDateTime,
                        TimeSlot = TimeSlot
                    };
                    list.Add(NewData);
                    AttendaceData.CC4 = JsonConvert.SerializeObject(list);
                    _db.AttendanceSheetBCA_1ST_SEM.Update(AttendaceData);
                    _db.SaveChanges();
                    return Json("Sucess");
                }
                else
                {
                    list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.GEIC2);
                    AttendanceJsonString NewData = new()
                    {
                        AttendanceCode = AttendanceCode,
                        AttedanceDateTime = AttendaceDateTime,
                        TimeSlot = TimeSlot
                    };
                    list.Add(NewData);
                    AttendaceData.GEIC2 = JsonConvert.SerializeObject(list);
                    _db.AttendanceSheetBCA_1ST_SEM.Update(AttendaceData);
                    _db.SaveChanges();
                    return Json("Sucess");
                }

            }
            else
            {
                AttendanceSheetBCA_1ST_SEM AttendanceRecord = new AttendanceSheetBCA_1ST_SEM();
                AttendanceRecord.StudentCode = StudentCode;
                AttendanceJsonString NewData = new()
                {
                    AttendanceCode = AttendanceCode,
                    AttedanceDateTime = AttendaceDateTime,
                };
                list.Add(NewData);
                if (CourseCode == "AEC1") { AttendanceRecord.AEC1 = JsonConvert.SerializeObject(list); }
                else if (CourseCode == "CC1") {AttendanceRecord.CC1 = JsonConvert.SerializeObject(list); }
                else if (CourseCode == "CC2") {AttendanceRecord.CC2 = JsonConvert.SerializeObject(list); }
                else if (CourseCode == "GEIC1") {AttendanceRecord.GEIC1 = JsonConvert.SerializeObject(list); }
                else if (CourseCode == "AEC2") {AttendanceRecord.AEC2 = JsonConvert.SerializeObject(list); }
                else if (CourseCode == "CC3") {AttendanceRecord.CC3 = JsonConvert.SerializeObject(list); }
                else if (CourseCode == "CC4") {AttendanceRecord.CC4 = JsonConvert.SerializeObject(list); }
                else { AttendanceRecord.GEIC2 = JsonConvert.SerializeObject(list); }
                
                _db.AttendanceSheetBCA_1ST_SEM.Add(AttendanceRecord);
                _db.SaveChanges();
                return Json("Sucess");
            }

        }
        public IActionResult AttendanceRecord()
        {

            return View();
        }

        public IActionResult _AttendanceRecord()
        {
            List<StudentsInfo> StudentsInfo = _db.StudentsInfo.ToList();
            List<AttendanceRecord> AttendanceRecordList= new();
            foreach (var obj in StudentsInfo)
            {
                AttendanceRecord AttendanceRecord = new AttendanceRecord();
                AttendanceRecord.StudentName = obj.StudentName;
                AttendanceRecord.StudentCode = obj.StudentCode;
                AttendanceRecord.ParentsNumber = obj.ParentsMobileno;
                AttendanceRecord.ClassRoll = obj.ClassRollno;
                AttendanceRecordList.Add(AttendanceRecord);
            }
            return PartialView(AttendanceRecordList);
        }

        #region Get Exams List

        public async Task<SelectList> GetSubject(short SemCode,string CourseCode)
        {
            var fun = _db.Subject.Where(x=>x.SemesterCode==SemCode && x.CourseCode==CourseCode).ToList();
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var row in fun)
            {
                list.Add(new SelectListItem()
                {
                    Text = row.SubjectName,
                    Value = row.SubjectCode.ToString()
                });
            }

            return new SelectList(list, "Value", "Text");
        }

        #endregion

        #region Select List

        [NonAction]
        public SelectList ToSelectList(DataTable table, string valueField, string textField)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new SelectListItem()
                {
                    Text = row[textField].ToString(),
                    Value = row[valueField].ToString()
                });
            }

            return new SelectList(list, "Value", "Text");
        }
        #endregion

    }
}

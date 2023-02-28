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
            //List<int> studentid = _db.StudentsInfo.Where(x => x.Year == 3 && x.Course == "2").Select(s => s.StudentCode).ToList();
            //foreach(var id in studentid)
            //{
            //    AttendanceSheetBBA_6TH_SEM Sheet = new AttendanceSheetBBA_6TH_SEM();
            //    Sheet.StudentCode = id;
            //    _db.AttendanceSheetBBA_6TH_SEM.Add(Sheet);
            //    _db.SaveChanges();
            //}
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
                subject.SubName = obj.SubName;
                SubjectList.Add(subject);
            }
            teacherSidebar.Name = teacherinfos.Name;
            teacherSidebar.Image = teacherinfos.Image;
            teacherSidebar.SubjectSideBar = SubjectList;
            return PartialView(teacherSidebar);
        }

        public IActionResult _studentlist(string? SubjectCode, string? CurrentDate, string TimeSlot, string CourseCode, string SemCode)
        {
            var year = 0;
            if (SemCode == "1") { year = 1; }
            else if (SemCode == "2") { year = 1; }
            else if (SemCode == "3") { year = 2; }
            else if (SemCode == "4") { year = 2; }
            else if (SemCode == "5") { year = 3; }
            else if (SemCode == "6") { year = 3; }
            List<StudentsInfo> StudentsInfo = _db.StudentsInfo.Where(x => x.Course == CourseCode && x.Year == year).ToList();
            List<StudentList> StudentList = new();

            if (CourseCode == "1")
            {
                if (SemCode == "1")
                {
                    foreach (var obj in StudentsInfo)
                    {
                        List<AttendanceJsonString> list = new();
                        string CurrentAttendanceCode = null;
                        StudentList students = new StudentList();
                        AttendanceSheetBCA_1ST_SEM RecordCurrent = _db.AttendanceSheetBCA_1ST_SEM.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();

                        if (SubjectCode == "AEC1")
                        {
                            if (RecordCurrent.AEC1 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.AEC1);
                            }
                        }
                        else if (SubjectCode == "CC1")
                        {
                            if (RecordCurrent.CC1 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.CC1);
                            };
                        }
                        else if (SubjectCode == "CC2")
                        {
                            if (RecordCurrent.CC2 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.CC2);
                            }
                        }
                        else if (SubjectCode == "GEIC1")
                        {
                            if (RecordCurrent.GEIC1 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.GEIC1);
                            }
                        }
                        else
                        {
                            list = null;
                        }
                        foreach (var li in list)
                        {
                            if (li.AttedanceDateTime == CurrentDate && li.TimeSlot == TimeSlot)
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
                }

                else if (SemCode == "3")
                {
                    foreach (var obj in StudentsInfo)
                    {
                        List<AttendanceJsonString> list = new();
                        string CurrentAttendanceCode = null;
                        StudentList students = new StudentList();
                        AttendanceSheetBCA_3RD_SEM RecordCurrent = _db.AttendanceSheetBCA_3RD_SEM.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();

                        if (SubjectCode == "CC5")
                        {
                            if (RecordCurrent.CC5 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.CC5);
                            }
                        }
                        else if (SubjectCode == "CC6")
                        {
                            if (RecordCurrent.CC6 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.CC6);
                            };
                        }
                        else if (SubjectCode == "CC7")
                        {
                            if (RecordCurrent.CC7 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.CC7);
                            }
                        }
                        else if (SubjectCode == "SEC1")
                        {
                            if (RecordCurrent.SEC1 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.SEC1);
                            }
                        }
                        else if (SubjectCode == "GEIC3")
                        {
                            if (RecordCurrent.GEIC3 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.GEIC3);
                            }
                        }
                        else
                        {
                            list = null;
                        }
                        foreach (var li in list)
                        {
                            if (li.AttedanceDateTime == CurrentDate && li.TimeSlot == TimeSlot)
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
                }

                //BCA STUDENT LIST Addd 2,3,6
                else if (SemCode == "2")
                {
                    foreach (var obj in StudentsInfo)
                    {
                        List<AttendanceJsonString> list = new();
                        string CurrentAttendanceCode = null;
                        StudentList students = new StudentList();
                        AttendanceSheetBCA_2ND_SEM RecordCurrent = _db.AttendanceSheetBCA_2ND_SEM.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();

                        if (SubjectCode == "AEC2")
                        {
                            if (RecordCurrent.AEC2 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.AEC2);
                            }
                        }
                        else if (SubjectCode == "CC3")
                        {
                            if (RecordCurrent.CC3 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.CC3);
                            };
                        }
                        else if (SubjectCode == "CC4")
                        {
                            if (RecordCurrent.CC4 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.CC4);
                            }
                        }
                        else if (SubjectCode == "GEIC2")
                        {
                            if (RecordCurrent.GEIC2 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.GEIC2);
                            }
                        }
                        else
                        {
                            list = null;
                        }
                        foreach (var li in list)
                        {
                            if (li.AttedanceDateTime == CurrentDate && li.TimeSlot == TimeSlot)
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
                }

                else if (SemCode == "4")
                {
                    foreach (var obj in StudentsInfo)
                    {
                        List<AttendanceJsonString> list = new();
                        string CurrentAttendanceCode = null;
                        StudentList students = new StudentList();
                        AttendanceSheetBCA_4TH_SEM RecordCurrent = _db.AttendanceSheetBCA_4TH_SEM.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();

                        if (SubjectCode == "CC8")
                        {
                            if (RecordCurrent.CC8 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.CC8);
                            }
                        }
                        else if (SubjectCode == "CC9")
                        {
                            if (RecordCurrent.CC9 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.CC9);
                            };
                        }
                        else if (SubjectCode == "CC10")
                        {
                            if (RecordCurrent.CC10 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.CC10);
                            }
                        }
                        else if (SubjectCode == "SEC2")
                        {
                            if (RecordCurrent.SEC2 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.SEC2);
                            }
                        }
                        else if (SubjectCode == "GEIC4")
                        {
                            if (RecordCurrent.GEIC4 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.GEIC4);
                            }
                        }
                        else
                        {
                            list = null;
                        }
                        foreach (var li in list)
                        {
                            if (li.AttedanceDateTime == CurrentDate && li.TimeSlot == TimeSlot)
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
                }

                else if (SemCode == "6")
                {
                    foreach (var obj in StudentsInfo)
                    {
                        List<AttendanceJsonString> list = new();
                        string CurrentAttendanceCode = null;
                        StudentList students = new StudentList();
                        AttendanceSheetBCA_6TH_SEM RecordCurrent = _db.AttendanceSheetBCA_6TH_SEM.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();

                        if (SubjectCode == "CC13")
                        {
                            if (RecordCurrent.CC13 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.CC13);
                            }
                        }
                        else if (SubjectCode == "CC14")
                        {
                            if (RecordCurrent.CC14 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.CC14);
                            };
                        }
                        else if (SubjectCode == "DSE3")
                        {
                            if (RecordCurrent.DSEC3 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.DSEC3);
                            }
                        }
                        else if (SubjectCode == "DSE4")
                        {
                            if (RecordCurrent.DSEC4 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.DSEC4);
                            }
                        }
                        else
                        {
                            list = null;
                        }
                        foreach (var li in list)
                        {
                            if (li.AttedanceDateTime == CurrentDate && li.TimeSlot == TimeSlot)
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
                }
                //end
                else
                {
                    foreach (var obj in StudentsInfo)
                    {
                        List<AttendanceJsonString> list = new();
                        string CurrentAttendanceCode = null;
                        StudentList students = new StudentList();
                        AttendanceSheetBCA_5TH_SEM RecordCurrent = _db.AttendanceSheetBCA_5TH_SEM.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();

                        if (SubjectCode == "CC11")
                        {
                            if (RecordCurrent.CC11 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.CC11);
                            }
                        }
                        else if (SubjectCode == "CC12")
                        {
                            if (RecordCurrent.CC12 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.CC12);
                            };
                        }
                        else if (SubjectCode == "DSE1")
                        {
                            if (RecordCurrent.DSE1 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.DSE1);
                            }
                        }
                        else if (SubjectCode == "DSE2")
                        {
                            if (RecordCurrent.DSE2 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.DSE2);
                            }
                        }
                        else
                        {
                            list = null;
                        }
                        foreach (var li in list)
                        {
                            if (li.AttedanceDateTime == CurrentDate && li.TimeSlot == TimeSlot)
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
                }
            }

            else
            {
                if (SemCode == "1")
                {
                    foreach (var obj in StudentsInfo)
                    {
                        List<AttendanceJsonString> list = new();
                        string CurrentAttendanceCode = null;
                        StudentList students = new StudentList();
                        AttendanceSheetBBA_1ST_SEM RecordCurrent = _db.AttendanceSheetBBA_1ST_SEM.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();

                        if (SubjectCode == "101")
                        {
                            if (RecordCurrent.B101 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.B101);
                            }
                        }
                        else if (SubjectCode == "102")
                        {
                            if (RecordCurrent.B102 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.B102);
                            };
                        }
                        else if (SubjectCode == "103")
                        {
                            if (RecordCurrent.B103 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.B103);
                            }
                        }
                        else if (SubjectCode == "104")
                        {
                            if (RecordCurrent.B104 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.B104);
                            }
                        }
                        else
                        {
                            list = null;
                        }
                        foreach (var li in list)
                        {
                            if (li.AttedanceDateTime == CurrentDate && li.TimeSlot == TimeSlot)
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
                }

                else if (SemCode == "3")
                {
                    foreach (var obj in StudentsInfo)
                    {
                        List<AttendanceJsonString> list = new();
                        string CurrentAttendanceCode = null;
                        StudentList students = new StudentList();
                        AttendanceSheetBBA_3RD_SEM RecordCurrent = _db.AttendanceSheetBBA_3RD_SEM.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();

                        if (SubjectCode == "301")
                        {
                            if (RecordCurrent.B301 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.B301);
                            }
                        }
                        else if (SubjectCode == "302")
                        {
                            if (RecordCurrent.B302 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.B302);
                            };
                        }
                        else if (SubjectCode == "303")
                        {
                            if (RecordCurrent.B303 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.B303);
                            }
                        }
                        else if (SubjectCode == "304")
                        {
                            if (RecordCurrent.B304 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.B304);
                            }
                        }
                        else if (SubjectCode == "305")
                        {
                            if (RecordCurrent.B305 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.B305);
                            }
                        }
                        else
                        {
                            list = null;
                        }
                        foreach (var li in list)
                        {
                            if (li.AttedanceDateTime == CurrentDate && li.TimeSlot == TimeSlot)
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
                }
                //BBA 2,4,6 SEM ADD
                else if (SemCode == "2")
                {
                    foreach (var obj in StudentsInfo)
                    {
                        List<AttendanceJsonString> list = new();
                        string CurrentAttendanceCode = null;
                        StudentList students = new StudentList();
                        AttendanceSheetBBA_2ND_SEM RecordCurrent = _db.AttendanceSheetBBA_2ND_SEM.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();

                        if (SubjectCode == "201")
                        {
                            if (RecordCurrent.B201 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.B201);
                            }
                        }
                        else if (SubjectCode == "202")
                        {
                            if (RecordCurrent.B202 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.B202);
                            };
                        }
                        else if (SubjectCode == "203")
                        {
                            if (RecordCurrent.B203 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.B203);
                            }
                        }
                        else if (SubjectCode == "204")
                        {
                            if (RecordCurrent.B204 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.B204);
                            }
                        }
                        else
                        {
                            list = null;
                        }
                        foreach (var li in list)
                        {
                            if (li.AttedanceDateTime == CurrentDate && li.TimeSlot == TimeSlot)
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
                }
                else if (SemCode == "4")
                {
                    foreach (var obj in StudentsInfo)
                    {
                        List<AttendanceJsonString> list = new();
                        string CurrentAttendanceCode = null;
                        StudentList students = new StudentList();
                        AttendanceSheetBBA_4TH_SEM RecordCurrent = _db.AttendanceSheetBBA_4TH_SEM.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();

                        if (SubjectCode == "401")
                        {
                            if (RecordCurrent.B401 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.B401);
                            }
                        }
                        else if (SubjectCode == "402")
                        {
                            if (RecordCurrent.B402 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.B402);
                            };
                        }
                        else if (SubjectCode == "403")
                        {
                            if (RecordCurrent.B403 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.B403);
                            }
                        }
                        else if (SubjectCode == "404")
                        {
                            if (RecordCurrent.B404 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.B404);
                            }
                        }
                        else if (SubjectCode == "405")
                        {
                            if (RecordCurrent.B405 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.B405);
                            }
                        }

                        else
                        {
                            list = null;
                        }
                        foreach (var li in list)
                        {
                            if (li.AttedanceDateTime == CurrentDate && li.TimeSlot == TimeSlot)
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
                }
                else if (SemCode == "6")
                {
                    foreach (var obj in StudentsInfo)
                    {
                        List<AttendanceJsonString> list = new();
                        string CurrentAttendanceCode = null;
                        StudentList students = new StudentList();
                        AttendanceSheetBBA_6TH_SEM RecordCurrent = _db.AttendanceSheetBBA_6TH_SEM.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();

                        if (SubjectCode == "601")
                        {
                            if (RecordCurrent.B601 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.B601);
                            }
                        }
                        else if (SubjectCode == "602")
                        {
                            if (RecordCurrent.B602 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.B602);
                            };
                        }
                        else if (SubjectCode == "603")
                        {
                            if (RecordCurrent.B603 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.B603);
                            }
                        }
                        else if (SubjectCode == "604")
                        {
                            if (RecordCurrent.B604 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.B604);
                            }                           
                        }
                        else if (SubjectCode == "605")
                        {
                            if (RecordCurrent.B605 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.B605);
                            }
                        }   
                        else if (SubjectCode == "606")
                        {
                            if (RecordCurrent.B606 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.B606);
                            }
                        }
                        else
                        {
                            list = null;
                        }
                        foreach (var li in list)
                        {
                            if (li.AttedanceDateTime == CurrentDate && li.TimeSlot == TimeSlot)
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
                }
                //END

                else
                {
                    foreach (var obj in StudentsInfo)
                    {
                        List<AttendanceJsonString> list = new();
                        string CurrentAttendanceCode = null;
                        StudentList students = new StudentList();
                        AttendanceSheetBBA_5TH_SEM RecordCurrent = _db.AttendanceSheetBBA_5TH_SEM.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();

                        if (SubjectCode == "501")
                        {
                            if (RecordCurrent.B501 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.B501);
                            }
                        }
                        else if (SubjectCode == "502")
                        {
                            if (RecordCurrent.B502 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.B502);
                            }
                        }
                        else if (SubjectCode == "503")
                        {
                            if (RecordCurrent.B503 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.B503);
                            }
                        }
                        else if (SubjectCode == "504")
                        {
                            if (RecordCurrent.B504 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.B504);
                            }
                        }
                        else if (SubjectCode == "505")
                        {
                            if (RecordCurrent.B505 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.B505);
                            }
                        }
                        else if (SubjectCode == "506")
                        {
                            if (RecordCurrent.B506 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(RecordCurrent.B506);
                            }
                        }
                        else
                        {
                            list = null;
                        }
                        foreach (var li in list)
                        {
                            if (li.AttedanceDateTime == CurrentDate && li.TimeSlot == TimeSlot)
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
                }

            }

            return PartialView(StudentList);
        }

        public IActionResult MarkAttendance(int StudentCode, string Remarks, string TimeSlot, string Semester, string CourseCode, string SubjectCode, string AttendaceDateTime, string AttendanceCode)
        {
            if (CourseCode == "1")
            {
                if (Semester == "1")
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
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
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
                                TimeSlot = TimeSlot,
                                Remarks = Remarks

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
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
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
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
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
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
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
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
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
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.CC4 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBCA_1ST_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else
                        {
                            if (AttendaceData.GEIC2 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.GEIC2);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
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
                            TimeSlot = TimeSlot,
                            Remarks = Remarks
                        };
                        list.Add(NewData);
                        if (CourseCode == "AEC1") { AttendanceRecord.AEC1 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "CC1") { AttendanceRecord.CC1 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "CC2") { AttendanceRecord.CC2 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "GEIC1") { AttendanceRecord.GEIC1 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "AEC2") { AttendanceRecord.AEC2 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "CC3") { AttendanceRecord.CC3 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "CC4") { AttendanceRecord.CC4 = JsonConvert.SerializeObject(list); }
                        else { AttendanceRecord.GEIC2 = JsonConvert.SerializeObject(list); }

                        _db.AttendanceSheetBCA_1ST_SEM.Add(AttendanceRecord);
                        _db.SaveChanges();
                        return Json("Sucess");
                    }
                }

                else if (Semester == "3")
                {
                    AttendanceSheetBCA_3RD_SEM? AttendaceData = _db.AttendanceSheetBCA_3RD_SEM.Where(x => x.StudentCode == StudentCode).First();
                    List<AttendanceJsonString>? list = new();
                    if (AttendaceData != null)
                    {
                        if (SubjectCode == "CC5")
                        {
                            if (AttendaceData.CC5 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.CC5);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.CC5 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBCA_3RD_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else if (SubjectCode == "CC6")
                        {
                            if (AttendaceData.CC6 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.CC6);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks

                            };
                            list.Add(NewData);
                            AttendaceData.CC6 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBCA_3RD_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else if (SubjectCode == "CC7")
                        {
                            if (AttendaceData.CC7 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.CC7);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.CC7 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBCA_3RD_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else if (SubjectCode == "SEC1")
                        {
                            if (AttendaceData.SEC1 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.SEC1);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.SEC1 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBCA_3RD_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else
                        {
                            if (AttendaceData.GEIC3 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.GEIC3);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.GEIC3 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBCA_3RD_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }

                    }
                    else
                    {
                        AttendanceSheetBCA_3RD_SEM AttendanceRecord = new AttendanceSheetBCA_3RD_SEM();
                        AttendanceRecord.StudentCode = StudentCode;
                        AttendanceJsonString NewData = new()
                        {
                            AttendanceCode = AttendanceCode,
                            AttedanceDateTime = AttendaceDateTime,
                            TimeSlot = TimeSlot,
                            Remarks = Remarks
                        };
                        list.Add(NewData);
                        if (CourseCode == "CC5") { AttendanceRecord.CC5 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "CC6") { AttendanceRecord.CC6 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "CC7") { AttendanceRecord.CC7 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "SEC1") { AttendanceRecord.SEC1 = JsonConvert.SerializeObject(list); }
                        else { AttendanceRecord.GEIC3 = JsonConvert.SerializeObject(list); }

                        _db.AttendanceSheetBCA_3RD_SEM.Add(AttendanceRecord);
                        _db.SaveChanges();
                        return Json("Sucess");
                    }


                }

                #region Adding BCA 2,4,6 SEM END
                //Adding BCA 2,4,6 SEM
                else if (Semester == "2")
                {
                    AttendanceSheetBCA_2ND_SEM? AttendaceData = _db.AttendanceSheetBCA_2ND_SEM.Where(x => x.StudentCode == StudentCode).First();
                    List<AttendanceJsonString>? list = new();
                    if (AttendaceData != null)
                    {
                        if (SubjectCode == "AEC2")
                        {
                            if (AttendaceData.AEC2 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.AEC2);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.AEC2 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBCA_2ND_SEM.Update(AttendaceData);
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
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.CC3 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBCA_2ND_SEM.Update(AttendaceData);
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
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.CC4 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBCA_2ND_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else
                        {
                            if (AttendaceData.GEIC2 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.GEIC2);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.GEIC2 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBCA_2ND_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }

                    }
                    else
                    {
                        AttendanceSheetBCA_2ND_SEM AttendanceRecord = new AttendanceSheetBCA_2ND_SEM();
                        AttendanceRecord.StudentCode = StudentCode;
                        AttendanceJsonString NewData = new()
                        {
                            AttendanceCode = AttendanceCode,
                            AttedanceDateTime = AttendaceDateTime,
                            TimeSlot = TimeSlot,
                            Remarks = Remarks
                        };
                        list.Add(NewData);
                        if (CourseCode == "AEC2") { AttendanceRecord.AEC2 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "CC3") { AttendanceRecord.CC3 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "CC4") { AttendanceRecord.CC4 = JsonConvert.SerializeObject(list); }
                        else { AttendanceRecord.GEIC2 = JsonConvert.SerializeObject(list); }

                        _db.AttendanceSheetBCA_2ND_SEM.Add(AttendanceRecord);
                        _db.SaveChanges();
                        return Json("Sucess");
                    }
                }

                else if (Semester == "4")
                {
                    AttendanceSheetBCA_4TH_SEM? AttendaceData = _db.AttendanceSheetBCA_4TH_SEM.Where(x => x.StudentCode == StudentCode).First();
                    List<AttendanceJsonString>? list = new();
                    if (AttendaceData != null)
                    {
                        if (SubjectCode == "CC8")
                        {
                            if (AttendaceData.CC8 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.CC8);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.CC8 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBCA_4TH_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else if (SubjectCode == "CC9")
                        {
                            if (AttendaceData.CC9 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.CC9);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks

                            };
                            list.Add(NewData);
                            AttendaceData.CC9 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBCA_4TH_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else if (SubjectCode == "CC10")
                        {
                            if (AttendaceData.CC10 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.CC10);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.CC10 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBCA_4TH_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else if (SubjectCode == "SEC2")
                        {
                            if (AttendaceData.SEC2 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.SEC2);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.SEC2 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBCA_4TH_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else
                        {
                            if (AttendaceData.GEIC4 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.GEIC4);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.GEIC4 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBCA_4TH_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }

                    }
                    else
                    {
                        AttendanceSheetBCA_4TH_SEM AttendanceRecord = new AttendanceSheetBCA_4TH_SEM();
                        AttendanceRecord.StudentCode = StudentCode;
                        AttendanceJsonString NewData = new()
                        {
                            AttendanceCode = AttendanceCode,
                            AttedanceDateTime = AttendaceDateTime,
                            TimeSlot = TimeSlot,
                            Remarks = Remarks
                        };
                        list.Add(NewData);
                        if (CourseCode == "CC8") { AttendanceRecord.CC8 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "CC9") { AttendanceRecord.CC9 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "CC10") { AttendanceRecord.CC10 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "SEC2") { AttendanceRecord.SEC2 = JsonConvert.SerializeObject(list); }
                        else { AttendanceRecord.GEIC4 = JsonConvert.SerializeObject(list); }

                        _db.AttendanceSheetBCA_4TH_SEM.Add(AttendanceRecord);
                        _db.SaveChanges();
                        return Json("Sucess");
                    }

                }

                else if (Semester == "6")
                {
                    AttendanceSheetBCA_6TH_SEM? AttendaceData = _db.AttendanceSheetBCA_6TH_SEM.Where(x => x.StudentCode == StudentCode).First();
                    List<AttendanceJsonString>? list = new();
                    if (AttendaceData != null)
                    {
                        if (SubjectCode == "CC13")
                        {
                            if (AttendaceData.CC13 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.CC13);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.CC13 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBCA_6TH_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else if (SubjectCode == "CC14")
                        {
                            if (AttendaceData.CC14 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.CC14);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.CC14 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBCA_6TH_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else if (SubjectCode == "DSE3")
                        {
                            if (AttendaceData.DSEC3 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.DSEC3);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.DSEC3 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBCA_6TH_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else
                        {
                            if (AttendaceData.DSEC4 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.DSEC4);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.DSEC4 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBCA_6TH_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }

                    }
                    else
                    {
                        AttendanceSheetBCA_6TH_SEM AttendanceRecord = new AttendanceSheetBCA_6TH_SEM();
                        AttendanceRecord.StudentCode = StudentCode;
                        AttendanceJsonString NewData = new()
                        {
                            AttendanceCode = AttendanceCode,
                            AttedanceDateTime = AttendaceDateTime,
                            TimeSlot = TimeSlot,
                            Remarks = Remarks
                        };
                        list.Add(NewData);
                        if (CourseCode == "CC13") { AttendanceRecord.CC13 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "CC14") { AttendanceRecord.CC14 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "DSEC3") { AttendanceRecord.DSEC3 = JsonConvert.SerializeObject(list); }
                        else { AttendanceRecord.DSEC4 = JsonConvert.SerializeObject(list); }

                        _db.AttendanceSheetBCA_6TH_SEM.Add(AttendanceRecord);
                        _db.SaveChanges();
                        return Json("Sucess");
                    }
                }
                //Adding BCA 2,4,6 SEM END
                #endregion

                else
                {
                    AttendanceSheetBCA_5TH_SEM? AttendaceData = _db.AttendanceSheetBCA_5TH_SEM.Where(x => x.StudentCode == StudentCode).First();
                    List<AttendanceJsonString>? list = new();
                    if (AttendaceData != null)
                    {
                        if (SubjectCode == "CC11")
                        {
                            if (AttendaceData.CC11 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.CC11);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.CC11 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBCA_5TH_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else if (SubjectCode == "CC12")
                        {
                            if (AttendaceData.CC12 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.CC12);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks

                            };
                            list.Add(NewData);
                            AttendaceData.CC12 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBCA_5TH_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else if (SubjectCode == "DSE1")
                        {
                            if (AttendaceData.DSE1 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.DSE1);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.DSE1 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBCA_5TH_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }

                        else
                        {
                            if (AttendaceData.DSE2 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.DSE2);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.DSE2 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBCA_5TH_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }

                    }
                    else
                    {
                        AttendanceSheetBCA_5TH_SEM AttendanceRecord = new AttendanceSheetBCA_5TH_SEM();
                        AttendanceRecord.StudentCode = StudentCode;
                        AttendanceJsonString NewData = new()
                        {
                            AttendanceCode = AttendanceCode,
                            AttedanceDateTime = AttendaceDateTime,
                            TimeSlot = TimeSlot,
                            Remarks = Remarks
                        };
                        list.Add(NewData);
                        if (CourseCode == "CC11") { AttendanceRecord.CC11 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "CC12") { AttendanceRecord.CC12 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "DSE1") { AttendanceRecord.DSE1 = JsonConvert.SerializeObject(list); }
                        else { AttendanceRecord.DSE2 = JsonConvert.SerializeObject(list); }

                        _db.AttendanceSheetBCA_5TH_SEM.Add(AttendanceRecord);
                        _db.SaveChanges();
                        return Json("Sucess");
                    }
                }
            }

            else
            {
                if (Semester == "1")
                {
                    AttendanceSheetBBA_1ST_SEM? AttendaceData = _db.AttendanceSheetBBA_1ST_SEM.Where(x => x.StudentCode == StudentCode).First();
                    List<AttendanceJsonString>? list = new();
                    if (AttendaceData != null)
                    {
                        if (SubjectCode == "101")
                        {
                            if (AttendaceData.B101 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.B101);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.B101 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBBA_1ST_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else if (SubjectCode == "102")
                        {
                            if (AttendaceData.B102 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.B102);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks

                            };
                            list.Add(NewData);
                            AttendaceData.B102 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBBA_1ST_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else if (SubjectCode == "103")
                        {
                            if (AttendaceData.B103 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.B103);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.B103 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBBA_1ST_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else
                        {
                            if (AttendaceData.B104 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.B104);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.B104 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBBA_1ST_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }

                    }
                    else
                    {
                        AttendanceSheetBBA_1ST_SEM AttendanceRecord = new AttendanceSheetBBA_1ST_SEM();
                        AttendanceRecord.StudentCode = StudentCode;
                        AttendanceJsonString NewData = new()
                        {
                            AttendanceCode = AttendanceCode,
                            AttedanceDateTime = AttendaceDateTime,
                            TimeSlot = TimeSlot,
                            Remarks = Remarks
                        };
                        list.Add(NewData);
                        if (CourseCode == "101") { AttendanceRecord.B101 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "102") { AttendanceRecord.B102 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "104") { AttendanceRecord.B104 = JsonConvert.SerializeObject(list); }
                        else { AttendanceRecord.B103 = JsonConvert.SerializeObject(list); }

                        _db.AttendanceSheetBBA_1ST_SEM.Add(AttendanceRecord);
                        _db.SaveChanges();
                        return Json("Sucess");
                    }
                }

                else if (Semester == "2")
                {
                    AttendanceSheetBBA_2ND_SEM? AttendaceData = _db.AttendanceSheetBBA_2ND_SEM.Where(x => x.StudentCode == StudentCode).First();
                    List<AttendanceJsonString>? list = new();
                    if (AttendaceData != null)
                    {
                        if (SubjectCode == "201")
                        {
                            if (AttendaceData.B201 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.B201);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.B201 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBBA_2ND_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else if (SubjectCode == "202")
                        {
                            if (AttendaceData.B202 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.B202);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.B202 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBBA_2ND_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else if (SubjectCode == "203")
                        {
                            if (AttendaceData.B203 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.B203);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.B203 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBBA_2ND_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else
                        {
                            if (AttendaceData.B204 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.B204);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.B204 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBBA_2ND_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }

                    }
                    else
                    {
                        AttendanceSheetBBA_2ND_SEM AttendanceRecord = new AttendanceSheetBBA_2ND_SEM();
                        AttendanceRecord.StudentCode = StudentCode;
                        AttendanceJsonString NewData = new()
                        {
                            AttendanceCode = AttendanceCode,
                            AttedanceDateTime = AttendaceDateTime,
                            TimeSlot = TimeSlot,
                            Remarks = Remarks
                        };
                        list.Add(NewData);
                        if (CourseCode == "201") { AttendanceRecord.B201 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "202") { AttendanceRecord.B202 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "204") { AttendanceRecord.B204 = JsonConvert.SerializeObject(list); }
                        else { AttendanceRecord.B203 = JsonConvert.SerializeObject(list); }

                        _db.AttendanceSheetBBA_2ND_SEM.Add(AttendanceRecord);
                        _db.SaveChanges();
                        return Json("Sucess");
                    }
                }

                else if (Semester == "3")
                {
                    AttendanceSheetBBA_3RD_SEM? AttendaceData = _db.AttendanceSheetBBA_3RD_SEM.Where(x => x.StudentCode == StudentCode).First();
                    List<AttendanceJsonString>? list = new();
                    if (AttendaceData != null)
                    {
                        if (SubjectCode == "301")
                        {
                            if (AttendaceData.B301 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.B301);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.B301 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBBA_3RD_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else if (SubjectCode == "302")
                        {
                            if (AttendaceData.B302 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.B302);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.B302 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBBA_3RD_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else if (SubjectCode == "303")
                        {
                            if (AttendaceData.B303 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.B303);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.B303 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBBA_3RD_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else if (SubjectCode == "304")
                        {
                            if (AttendaceData.B304 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.B304);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.B304 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBBA_3RD_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else
                        {
                            if (AttendaceData.B305 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.B305);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.B305 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBBA_3RD_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }

                    }
                    else
                    {
                        AttendanceSheetBBA_3RD_SEM AttendanceRecord = new AttendanceSheetBBA_3RD_SEM();
                        AttendanceRecord.StudentCode = StudentCode;
                        AttendanceJsonString NewData = new()
                        {
                            AttendanceCode = AttendanceCode,
                            AttedanceDateTime = AttendaceDateTime,
                            TimeSlot = TimeSlot,
                            Remarks = Remarks
                        };
                        list.Add(NewData);
                        if (CourseCode == "301") { AttendanceRecord.B301 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "302") { AttendanceRecord.B302 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "304") { AttendanceRecord.B304 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "305") { AttendanceRecord.B305 = JsonConvert.SerializeObject(list); }
                        else { AttendanceRecord.B303 = JsonConvert.SerializeObject(list); }

                        _db.AttendanceSheetBBA_3RD_SEM.Add(AttendanceRecord);
                        _db.SaveChanges();
                        return Json("Sucess");
                    }


                }

                else if (Semester == "4")
                {
                    AttendanceSheetBBA_4TH_SEM? AttendaceData = _db.AttendanceSheetBBA_4TH_SEM.Where(x => x.StudentCode == StudentCode).First();
                    List<AttendanceJsonString>? list = new();
                    if (AttendaceData != null)
                    {
                        if (SubjectCode == "401")
                        {
                            if (AttendaceData.B401 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.B401);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.B401 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBBA_4TH_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else if (SubjectCode == "402")
                        {
                            if (AttendaceData.B402 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.B402);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.B402 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBBA_4TH_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else if (SubjectCode == "403")
                        {
                            if (AttendaceData.B403 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.B403);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.B403 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBBA_4TH_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else if (SubjectCode == "404")
                        {
                            if (AttendaceData.B404 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.B404);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.B404 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBBA_4TH_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else
                        {
                            if (AttendaceData.B405 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.B405);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.B405 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBBA_4TH_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }

                    }
                    else
                    {
                        AttendanceSheetBBA_4TH_SEM AttendanceRecord = new AttendanceSheetBBA_4TH_SEM();
                        AttendanceRecord.StudentCode = StudentCode;
                        AttendanceJsonString NewData = new()
                        {
                            AttendanceCode = AttendanceCode,
                            AttedanceDateTime = AttendaceDateTime,
                            TimeSlot = TimeSlot,
                            Remarks = Remarks
                        };
                        list.Add(NewData);
                        if (CourseCode == "401") { AttendanceRecord.B401 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "402") { AttendanceRecord.B402 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "404") { AttendanceRecord.B404 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "405") { AttendanceRecord.B405 = JsonConvert.SerializeObject(list); }
                        else { AttendanceRecord.B403 = JsonConvert.SerializeObject(list); }

                        _db.AttendanceSheetBBA_4TH_SEM.Add(AttendanceRecord);
                        _db.SaveChanges();
                        return Json("Sucess");
                    }


                }

                else if (Semester == "6")
                {
                    AttendanceSheetBBA_6TH_SEM? AttendaceData = _db.AttendanceSheetBBA_6TH_SEM.Where(x => x.StudentCode == StudentCode).First();
                    List<AttendanceJsonString>? list = new();
                    if (AttendaceData != null)
                    {
                        if (SubjectCode == "601")
                        {
                            if (AttendaceData.B601 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.B601);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.B601 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBBA_6TH_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else if (SubjectCode == "602")
                        {
                            if (AttendaceData.B602 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.B602);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.B602 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBBA_6TH_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else if (SubjectCode == "603")
                        {
                            if (AttendaceData.B603 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.B603);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.B603 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBBA_6TH_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else if (SubjectCode == "604")
                        {
                            if (AttendaceData.B604 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.B604);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.B604 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBBA_6TH_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else if (SubjectCode == "605")
                        {
                            if (AttendaceData.B605 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.B605);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.B605 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBBA_6TH_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else
                        {
                            if (AttendaceData.B606 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.B606);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.B606 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBBA_6TH_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }

                    }
                    else
                    {
                        AttendanceSheetBBA_6TH_SEM AttendanceRecord = new AttendanceSheetBBA_6TH_SEM();
                        AttendanceRecord.StudentCode = StudentCode;
                        AttendanceJsonString NewData = new()
                        {
                            AttendanceCode = AttendanceCode,
                            AttedanceDateTime = AttendaceDateTime,
                            TimeSlot = TimeSlot,
                            Remarks = Remarks
                        };
                        list.Add(NewData);
                        if (CourseCode == "601") { AttendanceRecord.B601 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "602") { AttendanceRecord.B602 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "604") { AttendanceRecord.B604 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "605") { AttendanceRecord.B605 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "606") { AttendanceRecord.B606 = JsonConvert.SerializeObject(list); }
                        else { AttendanceRecord.B603 = JsonConvert.SerializeObject(list); }

                        _db.AttendanceSheetBBA_6TH_SEM.Add(AttendanceRecord);
                        _db.SaveChanges();
                        return Json("Sucess");
                    }


                }

                else
                {
                    AttendanceSheetBBA_5TH_SEM? AttendaceData = _db.AttendanceSheetBBA_5TH_SEM.Where(x => x.StudentCode == StudentCode).First();
                    List<AttendanceJsonString>? list = new();
                    if (AttendaceData != null)
                    {
                        if (SubjectCode == "501")
                        {
                            if (AttendaceData.B501 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.B501);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.B501 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBBA_5TH_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else if (SubjectCode == "502")
                        {
                            if (AttendaceData.B502 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.B502);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks

                            };
                            list.Add(NewData);
                            AttendaceData.B502 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBBA_5TH_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else if (SubjectCode == "503")
                        {
                            if (AttendaceData.B503 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.B503);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks

                            };
                            list.Add(NewData);
                            AttendaceData.B503 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBBA_5TH_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else if (SubjectCode == "504")
                        {
                            if (AttendaceData.B504 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.B504);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks

                            };
                            list.Add(NewData);
                            AttendaceData.B504 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBBA_5TH_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }
                        else if (SubjectCode == "505")
                        {
                            if (AttendaceData.B505 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.B505);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks

                            };
                            list.Add(NewData);
                            AttendaceData.B505 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBBA_5TH_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }

                        else
                        {
                            if (AttendaceData.B506 != null)
                            {
                                list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.B506);
                            }
                            AttendanceJsonString NewData = new()
                            {
                                AttendanceCode = AttendanceCode,
                                AttedanceDateTime = AttendaceDateTime,
                                TimeSlot = TimeSlot,
                                Remarks = Remarks
                            };
                            list.Add(NewData);
                            AttendaceData.B506 = JsonConvert.SerializeObject(list);
                            _db.AttendanceSheetBBA_5TH_SEM.Update(AttendaceData);
                            _db.SaveChanges();
                            return Json("Sucess");
                        }

                    }
                    else
                    {
                        AttendanceSheetBBA_5TH_SEM AttendanceRecord = new AttendanceSheetBBA_5TH_SEM();
                        AttendanceRecord.StudentCode = StudentCode;
                        AttendanceJsonString NewData = new()
                        {
                            AttendanceCode = AttendanceCode,
                            AttedanceDateTime = AttendaceDateTime,
                            TimeSlot = TimeSlot,
                            Remarks = Remarks
                        };
                        list.Add(NewData);
                        if (CourseCode == "501") { AttendanceRecord.B502 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "502") { AttendanceRecord.B502 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "503") { AttendanceRecord.B503 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "504") { AttendanceRecord.B504 = JsonConvert.SerializeObject(list); }
                        else if (CourseCode == "505") { AttendanceRecord.B505 = JsonConvert.SerializeObject(list); }
                        else { AttendanceRecord.B506 = JsonConvert.SerializeObject(list); }

                        _db.AttendanceSheetBBA_5TH_SEM.Add(AttendanceRecord);
                        _db.SaveChanges();
                        return Json("Sucess");
                    }
                }
            }
        }

        public IActionResult AttendanceRecord()
        {

            return View();
        }

        public IActionResult _AttendanceRecord(string CourseCode, int SemCode, string StartDate, string EndDate, string SubjectCheck)
        {
            List<AttendanceSheetBCA_1ST_SEM> AttendanceSheet1ST = _db.AttendanceSheetBCA_1ST_SEM.ToList();
            List<AttendanceSheetBCA_2ND_SEM> AttendanceSheet2ND = _db.AttendanceSheetBCA_2ND_SEM.ToList();
            List<AttendanceSheetBCA_3RD_SEM> AttendanceSheet3RD = _db.AttendanceSheetBCA_3RD_SEM.ToList();
            List<AttendanceSheetBCA_4TH_SEM> AttendanceSheet4TH = _db.AttendanceSheetBCA_4TH_SEM.ToList();
            List<AttendanceSheetBCA_5TH_SEM> AttendanceSheet5TH = _db.AttendanceSheetBCA_5TH_SEM.ToList();
            List<AttendanceSheetBCA_6TH_SEM> AttendanceSheet6TH = _db.AttendanceSheetBCA_6TH_SEM.ToList();
            List<AttendanceSheetBBA_1ST_SEM> AttendanceSheetB1ST = _db.AttendanceSheetBBA_1ST_SEM.ToList();
            List<AttendanceSheetBBA_3RD_SEM> AttendanceSheetB3RD = _db.AttendanceSheetBBA_3RD_SEM.ToList();
            List<AttendanceSheetBBA_6TH_SEM> AttendanceSheetB6TH = _db.AttendanceSheetBBA_6TH_SEM.ToList();
            List<AttendanceSheetBBA_4TH_SEM> AttendanceSheetB4TH = _db.AttendanceSheetBBA_4TH_SEM.ToList();
            List<AttendanceSheetBBA_2ND_SEM> AttendanceSheetB2ND = _db.AttendanceSheetBBA_2ND_SEM.ToList();
            List<AttendanceSheetBBA_5TH_SEM> AttendanceSheetB5TH = _db.AttendanceSheetBBA_5TH_SEM.ToList();
            var year = 0;
            if (SemCode == 1) { year = 1; }
            else if (SemCode == 2) { year = 1; }
            else if (SemCode == 3) { year = 2; }
            else if (SemCode == 4) { year = 2; }
            else if (SemCode == 5) { year = 3; }
            else if (SemCode == 6) { year = 3; }
            List<StudentsInfo> studentsInfos = _db.StudentsInfo.Where(x => x.Course == CourseCode && x.Year == year).ToList();
            List<AttendanceRecord> AttendanceRecordList = new();
            List<AttendanceJsonString> jsonlist = new();
            foreach (var obj in studentsInfos)
            {
                int Present = 0;
                int Absent = 0;
                int Late = 0;
                int TotalClass = 0;
                if (CourseCode == "1")
                {
                    if (SemCode == 1)
                    {
                        if (SubjectCheck == "0")
                        {
                            List<Subject> Subject = _db.Subject.Where(x => x.SemCode == SemCode && x.CourseCode == CourseCode).ToList();
                            foreach (var ob in Subject)
                            {
                                AttendanceSheetBCA_1ST_SEM Sheet = AttendanceSheet1ST.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();
                                if (ob.SubCode == "AEC1" && Sheet.AEC1 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.AEC1); }
                                else if (ob.SubCode == "CC1" && Sheet.CC1 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC1); }
                                else if (ob.SubCode == "GEIC1" && Sheet.GEIC1 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.GEIC1); }
                                else if (ob.SubCode == "CC2" && Sheet.CC2 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC2); }
                                else if (ob.SubCode == "AEC2" && Sheet.AEC2 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.AEC2); }
                                else if (ob.SubCode == "CC3" && Sheet.CC3 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC3); }
                                else if (ob.SubCode == "CC4" && Sheet.CC4 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC4); }
                                else { if (Sheet.GEIC2 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.GEIC2); } else { jsonlist = null; } }

                                if (jsonlist != null)
                                {
                                    foreach (var js in jsonlist)
                                    {
                                        var Rdate = Convert.ToDateTime(js.AttedanceDateTime);
                                        if (Rdate >= Convert.ToDateTime(StartDate) && Rdate <= Convert.ToDateTime(EndDate))
                                        {
                                            if (js.AttendanceCode == "1")
                                            {
                                                Present++;
                                            }
                                            else if (js.AttendanceCode == "2")
                                            {
                                                Absent++;
                                            }
                                            else
                                            {
                                                Late++;
                                            }
                                            TotalClass++;
                                        }

                                    }
                                }

                            }

                        }
                        else
                        {
                            AttendanceSheetBCA_1ST_SEM Sheet = AttendanceSheet1ST.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();
                            if (SubjectCheck == "AEC1" && Sheet.AEC1 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.AEC1); }
                            else if (SubjectCheck == "CC1" && Sheet.CC1 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC1); }
                            else if (SubjectCheck == "GEIC1" && Sheet.GEIC1 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.GEIC1); }
                            else if (SubjectCheck == "CC2" && Sheet.CC2 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC2); }
                            else if (SubjectCheck == "AEC2" && Sheet.AEC2 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.AEC2); }
                            else if (SubjectCheck == "CC3" && Sheet.CC3 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC3); }
                            else if (SubjectCheck == "CC4" && Sheet.CC4 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC4); }
                            else { if (Sheet.GEIC2 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.GEIC2); } else { jsonlist = null; } }

                            if (jsonlist != null)
                            {
                                foreach (var js in jsonlist)
                                {
                                    var Rdate = Convert.ToDateTime(js.AttedanceDateTime);
                                    if (Rdate >= Convert.ToDateTime(StartDate) && Rdate <= Convert.ToDateTime(EndDate))
                                    {
                                        if (js.AttendanceCode == "1")
                                        {
                                            Present++;
                                        }
                                        else if (js.AttendanceCode == "2")
                                        {
                                            Absent++;
                                        }
                                        else
                                        {
                                            Late++;
                                        }
                                        TotalClass++;
                                    }

                                }
                            }
                        }
                    }

                    else if (SemCode == 2)
                    {
                        if (SubjectCheck == "0")
                        {
                            List<Subject> Subject = _db.Subject.Where(x => x.SemCode == SemCode && x.CourseCode == CourseCode).ToList();
                            foreach (var ob in Subject)
                            {
                                AttendanceSheetBCA_2ND_SEM Sheet = AttendanceSheet2ND.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();                                
                                if (ob.SubCode == "AEC2" && Sheet.AEC2 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.AEC2); }
                                else if (ob.SubCode == "CC3" && Sheet.CC3 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC3); }
                                else if (ob.SubCode == "CC4" && Sheet.CC4 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC4); }
                                else { if (Sheet.GEIC2 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.GEIC2); } else { jsonlist = null; } }

                                if (jsonlist != null)
                                {
                                    foreach (var js in jsonlist)
                                    {
                                        var Rdate = Convert.ToDateTime(js.AttedanceDateTime);
                                        if (Rdate >= Convert.ToDateTime(StartDate) && Rdate <= Convert.ToDateTime(EndDate))
                                        {
                                            if (js.AttendanceCode == "1")
                                            {
                                                Present++;
                                            }
                                            else if (js.AttendanceCode == "2")
                                            {
                                                Absent++;
                                            }
                                            else
                                            {
                                                Late++;
                                            }
                                            TotalClass++;
                                        }

                                    }
                                }

                            }

                        }
                        else
                        {
                            AttendanceSheetBCA_2ND_SEM Sheet = AttendanceSheet2ND.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();                            
                            if (SubjectCheck == "AEC2" && Sheet.AEC2 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.AEC2); }
                            else if (SubjectCheck == "CC3" && Sheet.CC3 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC3); }
                            else if (SubjectCheck == "CC4" && Sheet.CC4 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC4); }
                            else { if (Sheet.GEIC2 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.GEIC2); } else { jsonlist = null; } }

                            if (jsonlist != null)
                            {
                                foreach (var js in jsonlist)
                                {
                                    var Rdate = Convert.ToDateTime(js.AttedanceDateTime);
                                    if (Rdate >= Convert.ToDateTime(StartDate) && Rdate <= Convert.ToDateTime(EndDate))
                                    {
                                        if (js.AttendanceCode == "1")
                                        {
                                            Present++;
                                        }
                                        else if (js.AttendanceCode == "2")
                                        {
                                            Absent++;
                                        }
                                        else
                                        {
                                            Late++;
                                        }
                                        TotalClass++;
                                    }

                                }
                            }
                        }
                    }

                    else if (SemCode == 3)
                    {
                        if (SubjectCheck == "0")
                        {
                            List<Subject> Subject = _db.Subject.Where(x => x.SemCode == SemCode && x.CourseCode == CourseCode).ToList();
                            foreach (var ob in Subject)
                            {
                                AttendanceSheetBCA_3RD_SEM Sheet = AttendanceSheet3RD.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();
                                if (ob.SubCode == "CC5" && Sheet.CC5 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC5); }
                                else if (ob.SubCode == "CC6" && Sheet.CC6 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC6); }
                                else if (ob.SubCode == "CC7" && Sheet.CC7 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC7); }
                                else if (ob.SubCode == "SEC1" && Sheet.SEC1 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.SEC1); }
                                else { if (Sheet.GEIC3 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.GEIC3); } else { jsonlist = null; } }

                                if (jsonlist != null)
                                {
                                    foreach (var js in jsonlist)
                                    {
                                        var Rdate = Convert.ToDateTime(js.AttedanceDateTime);
                                        if (Rdate >= Convert.ToDateTime(StartDate) && Rdate <= Convert.ToDateTime(EndDate))
                                        {
                                            if (js.AttendanceCode == "1")
                                            {
                                                Present++;
                                            }
                                            else if (js.AttendanceCode == "2")
                                            {
                                                Absent++;
                                            }
                                            else
                                            {
                                                Late++;
                                            }
                                            TotalClass++;
                                        }

                                    }
                                }

                            }

                        }
                        else
                        {
                            AttendanceSheetBCA_3RD_SEM Sheet = AttendanceSheet3RD.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();
                            if (SubjectCheck == "CC5" && Sheet.CC5 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC5); }
                            else if (SubjectCheck == "CC6" && Sheet.CC6 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC6); }
                            else if (SubjectCheck == "CC7" && Sheet.CC7 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC7); }
                            else if (SubjectCheck == "SEC1" && Sheet.SEC1 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.SEC1); }
                            else { if (Sheet.GEIC3 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.GEIC3); } else { jsonlist = null; } }

                            if (jsonlist != null)
                            {
                                foreach (var js in jsonlist)
                                {
                                    var Rdate = Convert.ToDateTime(js.AttedanceDateTime);
                                    if (Rdate >= Convert.ToDateTime(StartDate) && Rdate <= Convert.ToDateTime(EndDate))
                                    {
                                        if (js.AttendanceCode == "1")
                                        {
                                            Present++;
                                        }
                                        else if (js.AttendanceCode == "2")
                                        {
                                            Absent++;
                                        }
                                        else
                                        {
                                            Late++;
                                        }
                                        TotalClass++;
                                    }

                                }
                            }
                        }
                    }

                    else if (SemCode == 4)
                    {
                        if (SubjectCheck == "0")
                        {
                            List<Subject> Subject = _db.Subject.Where(x => x.SemCode == SemCode && x.CourseCode == CourseCode).ToList();
                            foreach (var ob in Subject)
                            {
                                AttendanceSheetBCA_4TH_SEM Sheet = AttendanceSheet4TH.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();
                                if (ob.SubCode == "CC8" && Sheet.CC8 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC8); }
                                else if (ob.SubCode == "CC9" && Sheet.CC9 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC9); }
                                else if (ob.SubCode == "CC10" && Sheet.CC10 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC10); }
                                else if (ob.SubCode == "SEC2" && Sheet.SEC2 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.SEC2); }
                                else { if (Sheet.GEIC4 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.GEIC4); } else { jsonlist = null; } }

                                if (jsonlist != null)
                                {
                                    foreach (var js in jsonlist)
                                    {
                                        var Rdate = Convert.ToDateTime(js.AttedanceDateTime);
                                        if (Rdate >= Convert.ToDateTime(StartDate) && Rdate <= Convert.ToDateTime(EndDate))
                                        {
                                            if (js.AttendanceCode == "1")
                                            {
                                                Present++;
                                            }
                                            else if (js.AttendanceCode == "2")
                                            {
                                                Absent++;
                                            }
                                            else
                                            {
                                                Late++;
                                            }
                                            TotalClass++;
                                        }

                                    }
                                }

                            }

                        }
                        else
                        {
                            AttendanceSheetBCA_4TH_SEM Sheet = AttendanceSheet4TH.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();
                            if (SubjectCheck == "CC8" && Sheet.CC8 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC8); }
                            else if (SubjectCheck == "CC9" && Sheet.CC9 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC9); }
                            else if (SubjectCheck == "CC10" && Sheet.CC10 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC10); }
                            else if (SubjectCheck == "SEC2" && Sheet.SEC2 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.SEC2); }
                            else { if (Sheet.GEIC4 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.GEIC4); } else { jsonlist = null; } }

                            if (jsonlist != null)
                            {
                                foreach (var js in jsonlist)
                                {
                                    var Rdate = Convert.ToDateTime(js.AttedanceDateTime);
                                    if (Rdate >= Convert.ToDateTime(StartDate) && Rdate <= Convert.ToDateTime(EndDate))
                                    {
                                        if (js.AttendanceCode == "1")
                                        {
                                            Present++;
                                        }
                                        else if (js.AttendanceCode == "2")
                                        {
                                            Absent++;
                                        }
                                        else
                                        {
                                            Late++;
                                        }
                                        TotalClass++;
                                    }

                                }
                            }
                        }
                    }

                    else if (SemCode == 6)
                    {
                        if (SubjectCheck == "0")
                        {
                            List<Subject> Subject = _db.Subject.Where(x => x.SemCode == SemCode && x.CourseCode == CourseCode).ToList();
                            foreach (var ob in Subject)
                            {
                                AttendanceSheetBCA_6TH_SEM Sheet = AttendanceSheet6TH.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();
                                if (ob.SubCode == "CC13" && Sheet.CC13 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC13); }
                                else if (ob.SubCode == "CC14" && Sheet.CC14 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC14); }
                                else if (ob.SubCode == "DSEC3" && Sheet.DSEC3 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.DSEC3); }
                                else { if (Sheet.DSEC4 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.DSEC4); } else { jsonlist = null; } }

                                if (jsonlist != null)
                                {
                                    foreach (var js in jsonlist)
                                    {
                                        var Rdate = Convert.ToDateTime(js.AttedanceDateTime);
                                        if (Rdate >= Convert.ToDateTime(StartDate) && Rdate <= Convert.ToDateTime(EndDate))
                                        {
                                            if (js.AttendanceCode == "1")
                                            {
                                                Present++;
                                            }
                                            else if (js.AttendanceCode == "2")
                                            {
                                                Absent++;
                                            }
                                            else
                                            {
                                                Late++;
                                            }
                                            TotalClass++;
                                        }

                                    }
                                }

                            }

                        }
                        else
                        {
                            AttendanceSheetBCA_6TH_SEM Sheet = AttendanceSheet6TH.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();
                            if (SubjectCheck == "CC13" && Sheet.CC13 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC13); }
                            else if (SubjectCheck == "CC14" && Sheet.CC14 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC14); }
                            else if (SubjectCheck == "DSEC3" && Sheet.DSEC3 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.DSEC3); }
                            else { if (Sheet.DSEC4 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.DSEC4); } else { jsonlist = null; } }

                            if (jsonlist != null)
                            {
                                foreach (var js in jsonlist)
                                {
                                    var Rdate = Convert.ToDateTime(js.AttedanceDateTime);
                                    if (Rdate >= Convert.ToDateTime(StartDate) && Rdate <= Convert.ToDateTime(EndDate))
                                    {
                                        if (js.AttendanceCode == "1")
                                        {
                                            Present++;
                                        }
                                        else if (js.AttendanceCode == "2")
                                        {
                                            Absent++;
                                        }
                                        else
                                        {
                                            Late++;
                                        }
                                        TotalClass++;
                                    }

                                }
                            }
                        }
                    }

                    else
                    {
                        if (SubjectCheck == "0")
                        {
                            List<Subject> Subject = _db.Subject.Where(x => x.SemCode == SemCode && x.CourseCode == CourseCode).ToList();
                            foreach (var ob in Subject)
                            {
                                AttendanceSheetBCA_5TH_SEM Sheet = AttendanceSheet5TH.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();
                                if (ob.SubCode == "CC11" && Sheet.CC11 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC11); }
                                else if (ob.SubCode == "CC12" && Sheet.CC12 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC12); }
                                else if (ob.SubCode == "DSE1" && Sheet.DSE1 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.DSE1); }
                                else { if (Sheet.DSE2 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.DSE2); } else { jsonlist = null; } }

                                if (jsonlist != null)
                                {
                                    foreach (var js in jsonlist)
                                    {
                                        var Rdate = Convert.ToDateTime(js.AttedanceDateTime);
                                        if (Rdate >= Convert.ToDateTime(StartDate) && Rdate <= Convert.ToDateTime(EndDate))
                                        {
                                            if (js.AttendanceCode == "1")
                                            {
                                                Present++;
                                            }
                                            else if (js.AttendanceCode == "2")
                                            {
                                                Absent++;
                                            }
                                            else
                                            {
                                                Late++;
                                            }
                                            TotalClass++;
                                        }

                                    }
                                }

                            }

                        }
                        else
                        {
                            AttendanceSheetBCA_5TH_SEM Sheet = AttendanceSheet5TH.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();
                            if (SubjectCheck == "CC11" && Sheet.CC11 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC11); }
                            else if (SubjectCheck == "CC12" && Sheet.CC12 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC12); }
                            else if (SubjectCheck == "DSE1" && Sheet.DSE1 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.DSE1); }
                            else { if (Sheet.DSE2 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.DSE2); } else { jsonlist = null; } }

                            if (jsonlist != null)
                            {
                                foreach (var js in jsonlist)
                                {
                                    var Rdate = Convert.ToDateTime(js.AttedanceDateTime);
                                    if (Rdate >= Convert.ToDateTime(StartDate) && Rdate <= Convert.ToDateTime(EndDate))
                                    {
                                        if (js.AttendanceCode == "1")
                                        {
                                            Present++;
                                        }
                                        else if (js.AttendanceCode == "2")
                                        {
                                            Absent++;
                                        }
                                        else
                                        {
                                            Late++;
                                        }
                                        TotalClass++;
                                    }

                                }
                            }
                        }
                    }
                }
                else
                {
                    if (SemCode == 1)
                    {
                        if (SubjectCheck == "0")
                        {
                            List<Subject> Subject = _db.Subject.Where(x => x.SemCode == SemCode && x.CourseCode == CourseCode).ToList();
                            foreach (var ob in Subject)
                            {
                                AttendanceSheetBBA_1ST_SEM Sheet = AttendanceSheetB1ST.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();
                                if (ob.SubCode == "101" && Sheet.B101 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B101); }
                                else if (ob.SubCode == "102" && Sheet.B102 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B102); }
                                else if (ob.SubCode == "103" && Sheet.B103 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B103); }
                                else { if (Sheet.B104 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B104); } else { jsonlist = null; } }

                                if (jsonlist != null)
                                {
                                    foreach (var js in jsonlist)
                                    {
                                        var Rdate = Convert.ToDateTime(js.AttedanceDateTime);
                                        if (Rdate >= Convert.ToDateTime(StartDate) && Rdate <= Convert.ToDateTime(EndDate))
                                        {
                                            if (js.AttendanceCode == "1")
                                            {
                                                Present++;
                                            }
                                            else if (js.AttendanceCode == "2")
                                            {
                                                Absent++;
                                            }
                                            else
                                            {
                                                Late++;
                                            }
                                            TotalClass++;
                                        }

                                    }
                                }

                            }

                        }
                        else
                        {
                            AttendanceSheetBBA_1ST_SEM Sheet = AttendanceSheetB1ST.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();
                            if (SubjectCheck == "101" && Sheet.B101 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B101); }
                            else if (SubjectCheck == "102" && Sheet.B102 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B102); }
                            else if (SubjectCheck == "103" && Sheet.B103 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B103); }
                            else { if (Sheet.B104 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B104); } else { jsonlist = null; } }

                            if (jsonlist != null)
                            {
                                foreach (var js in jsonlist)
                                {
                                    var Rdate = Convert.ToDateTime(js.AttedanceDateTime);
                                    if (Rdate >= Convert.ToDateTime(StartDate) && Rdate <= Convert.ToDateTime(EndDate))
                                    {
                                        if (js.AttendanceCode == "1")
                                        {
                                            Present++;
                                        }
                                        else if (js.AttendanceCode == "2")
                                        {
                                            Absent++;
                                        }
                                        else
                                        {
                                            Late++;
                                        }
                                        TotalClass++;
                                    }

                                }
                            }
                        }
                    }

                    else if (SemCode == 3)
                    {
                        if (SubjectCheck == "0")
                        {
                            List<Subject> Subject = _db.Subject.Where(x => x.SemCode == SemCode && x.CourseCode == CourseCode).ToList();
                            foreach (var ob in Subject)
                            {
                                AttendanceSheetBBA_3RD_SEM Sheet = AttendanceSheetB3RD.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();
                                if (ob.SubCode == "301" && Sheet.B301 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B301); }
                                else if (ob.SubCode == "302" && Sheet.B302 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B302); }
                                else if (ob.SubCode == "303" && Sheet.B303 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B303); }
                                else if (ob.SubCode == "304" && Sheet.B304 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B304); }
                                else { if (Sheet.B305 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B305); } else { jsonlist = null; } }

                                if (jsonlist != null)
                                {
                                    foreach (var js in jsonlist)
                                    {
                                        var Rdate = Convert.ToDateTime(js.AttedanceDateTime);
                                        if (Rdate >= Convert.ToDateTime(StartDate) && Rdate <= Convert.ToDateTime(EndDate))
                                        {
                                            if (js.AttendanceCode == "1")
                                            {
                                                Present++;
                                            }
                                            else if (js.AttendanceCode == "2")
                                            {
                                                Absent++;
                                            }
                                            else
                                            {
                                                Late++;
                                            }
                                            TotalClass++;
                                        }

                                    }
                                }

                            }

                        }
                        else
                        {
                            AttendanceSheetBBA_3RD_SEM Sheet = AttendanceSheetB3RD.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();
                            if (SubjectCheck == "301" && Sheet.B301 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B301); }
                            else if (SubjectCheck == "302" && Sheet.B302 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B302); }
                            else if (SubjectCheck == "303" && Sheet.B303 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B303); }
                            else if (SubjectCheck == "304" && Sheet.B304 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B304); }
                            else { if (Sheet.B305 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B305); } else { jsonlist = null; } }

                            if (jsonlist != null)
                            {
                                foreach (var js in jsonlist)
                                {
                                    var Rdate = Convert.ToDateTime(js.AttedanceDateTime);
                                    if (Rdate >= Convert.ToDateTime(StartDate) && Rdate <= Convert.ToDateTime(EndDate))
                                    {
                                        if (js.AttendanceCode == "1")
                                        {
                                            Present++;
                                        }
                                        else if (js.AttendanceCode == "2")
                                        {
                                            Absent++;
                                        }
                                        else
                                        {
                                            Late++;
                                        }
                                        TotalClass++;
                                    }

                                }
                            }
                        }
                    }

                    //bba 2,4,6 sem add
                    else if (SemCode == 2)
                    {
                        if (SubjectCheck == "0")
                        {
                            List<Subject> Subject = _db.Subject.Where(x => x.SemCode == SemCode && x.CourseCode == CourseCode).ToList();
                            foreach (var ob in Subject)
                            {
                                AttendanceSheetBBA_2ND_SEM Sheet = AttendanceSheetB2ND.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();
                                if (ob.SubCode == "201" && Sheet.B201 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B201); }
                                else if (ob.SubCode == "202" && Sheet.B202 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B202); }
                                else if (ob.SubCode == "203" && Sheet.B203 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B203); }
                                else { if (Sheet.B204 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B204); } else { jsonlist = null; } }

                                if (jsonlist != null)
                                {
                                    foreach (var js in jsonlist)
                                    {
                                        var Rdate = Convert.ToDateTime(js.AttedanceDateTime);
                                        if (Rdate >= Convert.ToDateTime(StartDate) && Rdate <= Convert.ToDateTime(EndDate))
                                        {
                                            if (js.AttendanceCode == "1")
                                            {
                                                Present++;
                                            }
                                            else if (js.AttendanceCode == "2")
                                            {
                                                Absent++;
                                            }
                                            else
                                            {
                                                Late++;
                                            }
                                            TotalClass++;
                                        }

                                    }
                                }

                            }

                        }
                        else
                        {
                            AttendanceSheetBBA_2ND_SEM Sheet = AttendanceSheetB2ND.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();
                            if (SubjectCheck == "201" && Sheet.B201 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B201); }
                            else if (SubjectCheck == "202" && Sheet.B202 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B202); }
                            else if (SubjectCheck == "203" && Sheet.B203 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B203); }
                            else { if (Sheet.B204 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B204); } else { jsonlist = null; } }

                            if (jsonlist != null)
                            {
                                foreach (var js in jsonlist)
                                {
                                    var Rdate = Convert.ToDateTime(js.AttedanceDateTime);
                                    if (Rdate >= Convert.ToDateTime(StartDate) && Rdate <= Convert.ToDateTime(EndDate))
                                    {
                                        if (js.AttendanceCode == "1")
                                        {
                                            Present++;
                                        }
                                        else if (js.AttendanceCode == "2")
                                        {
                                            Absent++;
                                        }
                                        else
                                        {
                                            Late++;
                                        }
                                        TotalClass++;
                                    }

                                }
                            }
                        }
                    }

                    else if (SemCode == 4)
                    {
                        if (SubjectCheck == "0")
                        {
                            List<Subject> Subject = _db.Subject.Where(x => x.SemCode == SemCode && x.CourseCode == CourseCode).ToList();
                            foreach (var ob in Subject)
                            {
                                AttendanceSheetBBA_4TH_SEM Sheet = AttendanceSheetB4TH.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();
                                if (ob.SubCode == "401" && Sheet.B401 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B401); }
                                else if (ob.SubCode == "402" && Sheet.B402 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B402); }
                                else if (ob.SubCode == "403" && Sheet.B403 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B403); }
                                else if (ob.SubCode == "405" && Sheet.B405 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B405); }
                                else { if (Sheet.B404 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B404); } else { jsonlist = null; } }

                                if (jsonlist != null)
                                {
                                    foreach (var js in jsonlist)
                                    {
                                        var Rdate = Convert.ToDateTime(js.AttedanceDateTime);
                                        if (Rdate >= Convert.ToDateTime(StartDate) && Rdate <= Convert.ToDateTime(EndDate))
                                        {
                                            if (js.AttendanceCode == "1")
                                            {
                                                Present++;
                                            }
                                            else if (js.AttendanceCode == "2")
                                            {
                                                Absent++;
                                            }
                                            else
                                            {
                                                Late++;
                                            }
                                            TotalClass++;
                                        }

                                    }
                                }

                            }

                        }
                        else
                        {
                            AttendanceSheetBBA_4TH_SEM Sheet = AttendanceSheetB4TH.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();
                            if (SubjectCheck == "401" && Sheet.B401 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B401); }
                            else if (SubjectCheck == "402" && Sheet.B402 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B402); }
                            else if (SubjectCheck == "403" && Sheet.B403 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B403); }
                            else if (SubjectCheck == "405" && Sheet.B405 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B405); }
                            else { if (Sheet.B404 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B404); } else { jsonlist = null; } }

                            if (jsonlist != null)
                            {
                                foreach (var js in jsonlist)
                                {
                                    var Rdate = Convert.ToDateTime(js.AttedanceDateTime);
                                    if (Rdate >= Convert.ToDateTime(StartDate) && Rdate <= Convert.ToDateTime(EndDate))
                                    {
                                        if (js.AttendanceCode == "1")
                                        {
                                            Present++;
                                        }
                                        else if (js.AttendanceCode == "2")
                                        {
                                            Absent++;
                                        }
                                        else
                                        {
                                            Late++;
                                        }
                                        TotalClass++;
                                    }

                                }
                            }
                        }
                    }

                    else if (SemCode == 6)
                    {
                        if (SubjectCheck == "0")
                        {
                            List<Subject> Subject = _db.Subject.Where(x => x.SemCode == SemCode && x.CourseCode == CourseCode).ToList();
                            foreach (var ob in Subject)
                            {
                                AttendanceSheetBBA_6TH_SEM Sheet = AttendanceSheetB6TH.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();
                                if (ob.SubCode == "601" && Sheet.B601 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B601); }
                                else if (ob.SubCode == "602" && Sheet.B602 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B602); }
                                else if (ob.SubCode == "603" && Sheet.B603 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B603); }
                                else if (ob.SubCode == "605" && Sheet.B605 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B605); }
                                else if (ob.SubCode == "605" && Sheet.B606 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B606); }
                                else { if (Sheet.B604 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B604); } else { jsonlist = null; } }

                                if (jsonlist != null)
                                {
                                    foreach (var js in jsonlist)
                                    {
                                        var Rdate = Convert.ToDateTime(js.AttedanceDateTime);
                                        if (Rdate >= Convert.ToDateTime(StartDate) && Rdate <= Convert.ToDateTime(EndDate))
                                        {
                                            if (js.AttendanceCode == "1")
                                            {
                                                Present++;
                                            }
                                            else if (js.AttendanceCode == "2")
                                            {
                                                Absent++;
                                            }
                                            else
                                            {
                                                Late++;
                                            }
                                            TotalClass++;
                                        }

                                    }
                                }

                            }

                        }
                        else
                        {
                            AttendanceSheetBBA_6TH_SEM Sheet = AttendanceSheetB6TH.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();
                            if (SubjectCheck == "601" && Sheet.B601 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B601); }
                            else if (SubjectCheck == "602" && Sheet.B602 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B602); }
                            else if (SubjectCheck == "603" && Sheet.B603 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B603); }
                            else if (SubjectCheck == "605" && Sheet.B605 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B605); }
                            else if (SubjectCheck == "606" && Sheet.B606 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B606); }
                            else { if (Sheet.B604 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B604); } else { jsonlist = null; } }

                            if (jsonlist != null)
                            {
                                foreach (var js in jsonlist)
                                {
                                    var Rdate = Convert.ToDateTime(js.AttedanceDateTime);
                                    if (Rdate >= Convert.ToDateTime(StartDate) && Rdate <= Convert.ToDateTime(EndDate))
                                    {
                                        if (js.AttendanceCode == "1")
                                        {
                                            Present++;
                                        }
                                        else if (js.AttendanceCode == "2")
                                        {
                                            Absent++;
                                        }
                                        else
                                        {
                                            Late++;
                                        }
                                        TotalClass++;
                                    }

                                }
                            }
                        }
                    }
                    //end

                    else
                    {
                        if (SubjectCheck == "0")
                        {
                            List<Subject> Subject = _db.Subject.Where(x => x.SemCode == SemCode && x.CourseCode == CourseCode).ToList();
                            foreach (var ob in Subject)
                            {
                                AttendanceSheetBBA_5TH_SEM Sheet = AttendanceSheetB5TH.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();
                                if (ob.SubCode == "501" && Sheet.B501 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B501); }
                                else if (ob.SubCode == "502" && Sheet.B502 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B502); }
                                else if (ob.SubCode == "503" && Sheet.B503 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B503); }
                                else if (ob.SubCode == "504" && Sheet.B504 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B504); }
                                else if (ob.SubCode == "505" && Sheet.B505 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B505); }
                                else { if (Sheet.B506 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B506); } else { jsonlist = null; } }

                                if (jsonlist != null)
                                {
                                    foreach (var js in jsonlist)
                                    {
                                        var Rdate = Convert.ToDateTime(js.AttedanceDateTime);
                                        if (Rdate >= Convert.ToDateTime(StartDate) && Rdate <= Convert.ToDateTime(EndDate))
                                        {
                                            if (js.AttendanceCode == "1")
                                            {
                                                Present++;
                                            }
                                            else if (js.AttendanceCode == "2")
                                            {
                                                Absent++;
                                            }
                                            else
                                            {
                                                Late++;
                                            }
                                            TotalClass++;
                                        }

                                    }
                                }

                            }

                        }
                        else
                        {
                            AttendanceSheetBBA_5TH_SEM Sheet = AttendanceSheetB5TH.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();
                            if (SubjectCheck == "501" && Sheet.B501 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B501); }
                            else if (SubjectCheck == "502" && Sheet.B502 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B502); }
                            else if (SubjectCheck == "503" && Sheet.B503 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B503); }
                            else if (SubjectCheck == "504" && Sheet.B504 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B504); }
                            else if (SubjectCheck == "505" && Sheet.B505 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B505); }
                            else { if (Sheet.B506 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B506); } else { jsonlist = null; } }

                            if (jsonlist != null)
                            {
                                foreach (var js in jsonlist)
                                {
                                    var Rdate = Convert.ToDateTime(js.AttedanceDateTime);
                                    if (Rdate >= Convert.ToDateTime(StartDate) && Rdate <= Convert.ToDateTime(EndDate))
                                    {
                                        if (js.AttendanceCode == "1")
                                        {
                                            Present++;
                                        }
                                        else if (js.AttendanceCode == "2")
                                        {
                                            Absent++;
                                        }
                                        else
                                        {
                                            Late++;
                                        }
                                        TotalClass++;
                                    }

                                }
                            }
                        }
                    }
                }

                AttendanceRecord AttendanceRecord = new AttendanceRecord();
                AttendanceRecord.StudentName = obj.StudentName;
                AttendanceRecord.StudentCode = obj.StudentCode;
                AttendanceRecord.ParentsNumber = obj.ParentsMobileno;
                AttendanceRecord.Present = Present;
                AttendanceRecord.Absent = Absent;
                AttendanceRecord.Late = Late;
                AttendanceRecord.TotalClass = TotalClass;
                if (TotalClass != 0) { AttendanceRecord.Percentage = Present / TotalClass * 100; }
                else { AttendanceRecord.Percentage = 0; }
                AttendanceRecordList.Add(AttendanceRecord);
            }
            return PartialView(AttendanceRecordList);
        }

        #region Get Subject List

        public async Task<SelectList> GetSubject(short SemCode, string CourseCode)
        {
            var fun = _db.Subject.Where(x => x.SemCode == SemCode && x.CourseCode == CourseCode).ToList();
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var row in fun)
            {
                list.Add(new SelectListItem()
                {
                    Text = row.SubName,
                    Value = row.SubCode.ToString()
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

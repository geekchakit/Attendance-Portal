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
                            if(AttendaceData.GEIC3!=null)
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
                            if(AttendaceData.DSE2!=null)
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
                        //else if (SubjectCode == "CC2")
                        //{
                        //    if (AttendaceData.CC2 != null)
                        //    {
                        //        list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.CC2);
                        //    }
                        //    AttendanceJsonString NewData = new()
                        //    {
                        //        AttendanceCode = AttendanceCode,
                        //        AttedanceDateTime = AttendaceDateTime,
                        //        TimeSlot = TimeSlot,
                        //        Remarks = Remarks
                        //    };
                        //    list.Add(NewData);
                        //    AttendaceData.CC2 = JsonConvert.SerializeObject(list);
                        //    _db.AttendanceSheetBCA_1ST_SEM.Update(AttendaceData);
                        //    _db.SaveChanges();
                        //    return Json("Sucess");
                        //}
                        else
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
                        //else if (CourseCode == "104") { AttendanceRecord.104 = JsonConvert.SerializeObject(list); }
                        else { AttendanceRecord.B103 = JsonConvert.SerializeObject(list); }

                        _db.AttendanceSheetBBA_1ST_SEM.Add(AttendanceRecord);
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
                        //else if (SubjectCode == "SEC1")
                        //{
                        //    if (AttendaceData.SEC1 != null)
                        //    {
                        //        list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.SEC1);
                        //    }
                        //    AttendanceJsonString NewData = new()
                        //    {
                        //        AttendanceCode = AttendanceCode,
                        //        AttedanceDateTime = AttendaceDateTime,
                        //        TimeSlot = TimeSlot,
                        //        Remarks = Remarks
                        //    };
                        //    list.Add(NewData);
                        //    AttendaceData.SEC1 = JsonConvert.SerializeObject(list);
                        //    _db.AttendanceSheetBCA_3RD_SEM.Update(AttendaceData);
                        //    _db.SaveChanges();
                        //    return Json("Sucess");
                        //}
                        else
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
                        //else if (CourseCode == "SEC1") { AttendanceRecord.SEC1 = JsonConvert.SerializeObject(list); }
                        else { AttendanceRecord.B303 = JsonConvert.SerializeObject(list); }

                        _db.AttendanceSheetBBA_3RD_SEM.Add(AttendanceRecord);
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
                        //else if (SubjectCode == "CC12")
                        //{
                        //    if (AttendaceData.CC12 != null)
                        //    {
                        //        list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.CC12);
                        //    }
                        //    AttendanceJsonString NewData = new()
                        //    {
                        //        AttendanceCode = AttendanceCode,
                        //        AttedanceDateTime = AttendaceDateTime,
                        //        TimeSlot = TimeSlot,
                        //        Remarks = Remarks

                        //    };
                        //    list.Add(NewData);
                        //    AttendaceData.CC12 = JsonConvert.SerializeObject(list);
                        //    _db.AttendanceSheetBCA_5TH_SEM.Update(AttendaceData);
                        //    _db.SaveChanges();
                        //    return Json("Sucess");
                        //}
                        //else if (SubjectCode == "DSE1")
                        //{
                        //    if (AttendaceData.DSE1 != null)
                        //    {
                        //        list = list = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(AttendaceData.DSE1);
                        //    }
                        //    AttendanceJsonString NewData = new()
                        //    {
                        //        AttendanceCode = AttendanceCode,
                        //        AttedanceDateTime = AttendaceDateTime,
                        //        TimeSlot = TimeSlot,
                        //        Remarks = Remarks
                        //    };
                        //    list.Add(NewData);
                        //    AttendaceData.DSE1 = JsonConvert.SerializeObject(list);
                        //    _db.AttendanceSheetBCA_5TH_SEM.Update(AttendaceData);
                        //    _db.SaveChanges();
                        //    return Json("Sucess");
                        //}

                        else
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
                        //else if (CourseCode == "CC12") { AttendanceRecord.CC12 = JsonConvert.SerializeObject(list); }
                        //else if (CourseCode == "DSE1") { AttendanceRecord.DSE1 = JsonConvert.SerializeObject(list); }
                        else { AttendanceRecord.B502 = JsonConvert.SerializeObject(list); }

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
            List<AttendanceSheetBCA_3RD_SEM> AttendanceSheet3RD = _db.AttendanceSheetBCA_3RD_SEM.ToList();
            List<AttendanceSheetBCA_5TH_SEM> AttendanceSheet5TH = _db.AttendanceSheetBCA_5TH_SEM.ToList();
            List<AttendanceSheetBBA_1ST_SEM> AttendanceSheetB1ST = _db.AttendanceSheetBBA_1ST_SEM.ToList();
            List<AttendanceSheetBBA_3RD_SEM> AttendanceSheetB3RD = _db.AttendanceSheetBBA_3RD_SEM.ToList();
            List<AttendanceSheetBBA_5TH_SEM> AttendanceSheetB5TH = _db.AttendanceSheetBBA_5TH_SEM.ToList();
            var year = 0;
            if (SemCode == 1) { year = 1; }
            else if (SemCode == 2) { year = 1; }
            else if (SemCode == 3) { year = 2; }
            else if (SemCode == 4) { year = 2; }
            else if (SemCode == 5) { year = 3; }
            else if (SemCode == 6) { year = 3; }
            List<StudentsInfo> studentsInfos = _db.StudentsInfo.Where(x=>x.Course==CourseCode &&x.Year==year).ToList();
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
                                //else if (ob.SubCode == "GEIC1" && Sheet.GEIC1 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.GEIC1); }
                                else { if (Sheet.B103 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B103); } else { jsonlist = null; } }

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
                            //else if (SubjectCheck == "GEIC1" && Sheet.GEIC1 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.GEIC1); }
                            else { if (Sheet.B103 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B103); } else { jsonlist = null; } }

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
                                //else if (ob.SubCode == "CC7" && Sheet.CC7 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC7); }
                                //else if (ob.SubCode == "SEC1" && Sheet.SEC1 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.SEC1); }
                                else { if (Sheet.B303 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B303); } else { jsonlist = null; } }

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
                            //else if (SubjectCheck == "CC7" && Sheet.CC7 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC7); }
                            //else if (SubjectCheck == "SEC1" && Sheet.SEC1 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.SEC1); }
                            else { if (Sheet.B303 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B303); } else { jsonlist = null; } }

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
                                AttendanceSheetBBA_5TH_SEM Sheet = AttendanceSheetB5TH.Where(x => x.StudentCode == obj.StudentCode).FirstOrDefault();
                                if (ob.SubCode == "501" && Sheet.B502 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B502); }
                                //else if (ob.SubCode == "CC12" && Sheet.CC12 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC12); }
                                //else if (ob.SubCode == "DSE1" && Sheet.DSE1 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.DSE1); }
                                else { if (Sheet.B502 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B502); } else { jsonlist = null; } }

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
                            //else if (SubjectCheck == "CC12" && Sheet.CC12 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.CC12); }
                            //else if (SubjectCheck == "DSE1" && Sheet.DSE1 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.DSE1); }
                            else { if (Sheet.B502 != null) { jsonlist = jsonlist = JsonConvert.DeserializeObject<List<AttendanceJsonString>>(Sheet.B502); } else { jsonlist = null; } }

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

        #region Get Exams List

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

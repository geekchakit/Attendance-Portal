using Attendance_Portal.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace freecode.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<StudentsInfo> StudentsInfo { get; set; }
    public DbSet<Teacherinfo> Teacherinfo { get; set; }
    public DbSet<Subject> Subject { get; set; }
    public DbSet<AttendanceSheetBCA_1ST_SEM> AttendanceSheetBCA_1ST_SEM { get; set; }
    public DbSet<AttendanceSheetBCA_3RD_SEM> AttendanceSheetBCA_3RD_SEM { get; set; }
    public DbSet<AttendanceSheetBCA_5TH_SEM> AttendanceSheetBCA_5TH_SEM { get; set; }
    public DbSet<AttendanceSheetBBA_1ST_SEM> AttendanceSheetBBA_1ST_SEM { get; set; }
    public DbSet<AttendanceSheetBBA_3RD_SEM> AttendanceSheetBBA_3RD_SEM { get; set; }
    public DbSet<AttendanceSheetBBA_5TH_SEM> AttendanceSheetBBA_5TH_SEM { get; set; }

    //ADDING 246 BBA BBCA
    public DbSet<AttendanceSheetBCA_2ND_SEM> AttendanceSheetBCA_2ND_SEM { get; set; }
    public DbSet<AttendanceSheetBCA_4TH_SEM> AttendanceSheetBCA_4TH_SEM { get; set; }
    public DbSet<AttendanceSheetBCA_6TH_SEM> AttendanceSheetBCA_6TH_SEM { get; set; }
    public DbSet<AttendanceSheetBBA_2ND_SEM> AttendanceSheetBBA_2ND_SEM { get; set; }
    public DbSet<AttendanceSheetBBA_4TH_SEM> AttendanceSheetBBA_4TH_SEM { get; set; }
    public DbSet<AttendanceSheetBBA_6TH_SEM> AttendanceSheetBBA_6TH_SEM { get; set; }

}
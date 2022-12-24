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
   
}
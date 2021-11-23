using AttendanceRegister2.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceRegister2.ApplicationDbContex
{
    public class ApplicationDbContext : IdentityDbContext<StaffModel>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

<<<<<<< HEAD
        public DbSet<AttendanceModel> Attendance { get; set; }
        public DbSet<ReasonsModel> Reasons { get; set; }
        public DbSet<RulesModel> Rules { get; set; }
        public DbSet<RolesModel> Department { get; set; }
        public DbSet<StaffsReasonsTableModel> StaffsReasons { get; set; }

       
=======

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    builder.Entity<RolesModel>().HasData(new RolesModel=);
        //}
>>>>>>> 4b8b4600b6cb367ebb9e37c1a477cbec33f1948f
    }
}

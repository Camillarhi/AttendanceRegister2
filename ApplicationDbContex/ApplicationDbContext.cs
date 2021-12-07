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

        public DbSet<AttendanceModel> Attendance { get; set; }
        public DbSet<ReasonsModel> Reasons { get; set; }
        public DbSet<RulesModel> Rules { get; set; }
        public DbSet<RolesModel> Department { get; set; }
        public DbSet<SubRolesModel> SubDepartment { get; set; }
        public DbSet<CompanyModel> CompanyName { get; set; }
        public DbSet<StaffsReasonsTableModel> StaffsReasons { get; set; }
       
    }
}

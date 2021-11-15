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


        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    builder.Entity<RolesModel>().HasData(new RolesModel=);
        //}
    }
}

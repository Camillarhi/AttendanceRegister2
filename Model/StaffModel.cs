using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceRegister2.Model
{
    public class StaffModel : IdentityUser
    {
        public string StaffId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set;}

        public string ProfilePicture { get; set; }


        [ForeignKey(nameof(SubRolesModel.SubDepartment))]

        public string SubDepartment { get; set; }






    }
}

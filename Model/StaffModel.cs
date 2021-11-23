using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.ComponentModel.DataAnnotations.Schema;
=======
>>>>>>> 4b8b4600b6cb367ebb9e37c1a477cbec33f1948f
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

<<<<<<< HEAD
       // [ForeignKey(nameof(RulesModel))]

        //public int RulesModelId { get; set; }

        //public RulesModel RulesModel { get; set; }


=======
>>>>>>> 4b8b4600b6cb367ebb9e37c1a477cbec33f1948f

        
    }
}

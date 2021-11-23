using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceRegister2.HelperClass
{
    public  class DropDown
    {
      //  public static string Male = "Male";
        //public static string Female = "Female";    
        public static string Admin = "Admin";    
        public static string SoftwareDevelopers = "Software Developers";    
        public static string SoftwareTesters = "Software Testers";    
        public static string OfficeCustodians = "Office Custodians";    
        public static string YouthCorpers = "Youth Corper";    
        public static string IndustrialTraining = "Industrial Training";    
<<<<<<< HEAD
        public static string Trainee = "Trainee";    
=======
        public static string Trainiee = "Trainiee";    
>>>>>>> 4b8b4600b6cb367ebb9e37c1a477cbec33f1948f

        //public static List<SelectListItem> GetGenderDropDown()
        //{
        //    return new List<SelectListItem>
        //    {
        //        new SelectListItem{Value=DropDown.Male, Text=DropDown.Male},
        //        new SelectListItem{Value=DropDown.Female, Text=DropDown.Female}

        //    };
        //}

        public  List<SelectListItem> GetRolesDropDown()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Value=DropDown.Admin, Text=DropDown.Admin},
                new SelectListItem{Value=DropDown.SoftwareDevelopers, Text=DropDown.SoftwareDevelopers},
                new SelectListItem{Value=DropDown.SoftwareTesters, Text=DropDown.SoftwareTesters},
                new SelectListItem{Value=DropDown.OfficeCustodians, Text=DropDown.OfficeCustodians},
                new SelectListItem{Value=DropDown.YouthCorpers, Text=DropDown.YouthCorpers},
<<<<<<< HEAD
                new SelectListItem{Value=DropDown.IndustrialTraining, Text=DropDown.IndustrialTraining},
                new SelectListItem{Value=DropDown.Trainee, Text=DropDown.Trainee}
=======
                new SelectListItem{Value=DropDown.IndustrialTraining, Text=DropDown.IndustrialTraining}
>>>>>>> 4b8b4600b6cb367ebb9e37c1a477cbec33f1948f
            };
        }


    }
}

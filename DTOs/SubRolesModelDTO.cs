using AttendanceRegister2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceRegister2.DTOs
{
    public class SubRolesModelDTO
    {
        [ForeignKey(nameof(RolesModel.Department))]
        public string Department { get; set; }

        public string SubDepartment { get; set; }


    }
}

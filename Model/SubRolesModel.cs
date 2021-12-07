using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceRegister2.Model
{
    public class SubRolesModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(RolesModel.Department))]
        public string Department { get; set; }

        public string SubDepartment { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceRegister2.Model
{
    public class RolesModel
    {
        [Key]
        public string Id { get; set; }

        public string Department { get; set; }
    }
}

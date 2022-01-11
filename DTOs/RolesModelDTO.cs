using AttendanceRegister2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceRegister2.DTOs
{
    public class RolesModelDTO
    {

        public string Department { get; set; }


        [ForeignKey(nameof(RulesModel))]

        public int RulesModelId { get; set; }
    }
}

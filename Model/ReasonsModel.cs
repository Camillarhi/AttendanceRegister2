using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceRegister2.Model
{
    public class ReasonsModel
    {
        [Key]
        public int Id { get; set; }

        public string Reasons { get; set;}
    }
}

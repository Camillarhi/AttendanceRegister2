using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceRegister2.Model
{
    public class AttendanceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceRegister2.Model
{
    public class RulesModel
    {

        [Key]
        public int Id { get; set; }

        public int StartOfDayHour { get; set; }

        public int StartOfDayMinutes { get; set; }

        public int StartOfDayGracePeriod { get; set; }

        public int EndOfDayHour { get; set; }

        public int EndOfDayMinutes { get; set; }

        public int EndOfDayGracePeriod { get; set; }
    }
}

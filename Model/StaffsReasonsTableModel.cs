using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceRegister2.Model
{
    public class StaffsReasonsTableModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(ReasonsModel.Reasons))]
        public string Reasons { get; set; }
                       
        [ForeignKey(nameof(StaffModel.StaffId))]
        public string StaffId { get; set; }

        [ForeignKey(nameof(StaffModel.FirstName))]

        public string FirstName { get; set; }
        [ForeignKey(nameof(StaffModel.LastName))]

        public string LastName { get; set; }

        [ForeignKey(nameof(AttendanceModel.TimeIn))]
        public DateTime? TimeIn { get; set; }

        [ForeignKey(nameof(AttendanceModel.TimeOut))]
        public DateTime? TimeOut { get; set; }
    }
}

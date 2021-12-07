using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceRegister2.Model
{
    public class AttendanceModel
    {
        [Key]
        public int Id { get; set; }
       
        [ForeignKey(nameof(StaffModel.StaffId))]
        public string StaffId { get; set; }

        [ForeignKey(nameof(StaffModel.FirstName))]

        public string FirstName { get; set; }
        [ForeignKey(nameof(StaffModel.LastName))]

        public string LastName { get; set; }

        public DateTime? TimeIn { get; set; }
        
        public DateTime? TimeOut { get; set; }
        
    }
}

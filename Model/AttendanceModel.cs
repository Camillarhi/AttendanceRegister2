using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
=======
>>>>>>> 4b8b4600b6cb367ebb9e37c1a477cbec33f1948f
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceRegister2.Model
{
    public class AttendanceModel
    {
<<<<<<< HEAD
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
=======
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
>>>>>>> 4b8b4600b6cb367ebb9e37c1a477cbec33f1948f
    }
}

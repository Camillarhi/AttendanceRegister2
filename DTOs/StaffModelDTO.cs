using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceRegister2.DTOs
{
    public class StaffModelDTO
    {
        [Key]
        [Required]

        public string StaffId { get; set; }
        
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Gender { get; set; }
        
        public string Department { get; set; }

        [Required]
        public IFormFile ProfilePicture { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        //[Required]
        //[EmailAddress]
        //public string Email { get; set; }

       // [Required]
       // [DataType(DataType.Password)]
       // [StringLength(100, ErrorMessage ="The {0} must be at least {2} characters long", MinimumLength =6)]
       // public string Password { get; set; }

       //[DataType(DataType.Password)]
       // [Display(Name ="Confirm Password")]
       // [Compare("Password", ErrorMessage ="The password and confirmation password do not match")]
       // public string ConfirmPassword { get; set; }



    }
}

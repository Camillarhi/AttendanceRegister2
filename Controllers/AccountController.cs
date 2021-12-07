using AttendanceRegister2.ApplicationDbContex;
using AttendanceRegister2.DTOs;
using AttendanceRegister2.HelperClass;
using AttendanceRegister2.Model;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceRegister2.Controllers
{
    [ApiController]
    [Route("api/staff")]
    public class AccountController : ControllerBase
    {


        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;
        private readonly string containerName = "Staff";
        UserManager<StaffModel> _userManager;
        SignInManager<StaffModel> _signInManager;
        RoleManager<IdentityRole> _roleManager;

        private List<DropDown> dropDowns { get; set; }
        // private List<Staff> staffs { get; set; }

        public AccountController(ApplicationDbContext db, IFileStorageService fileStorageService, IMapper mapper, UserManager<StaffModel> userManager,
        SignInManager<StaffModel> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
        }


        [HttpGet]
        public ActionResult<StaffModel> GetAll()
        {
            try
            {
                var staffs = (from user in _db.Users
                              select new StaffModel
                              {
                                  FirstName = user.FirstName,
                                  LastName = user.LastName
                              }
                         ).ToList();

                //  var users = await _userManager.FindByIdAsync(staffs);
                return Ok(staffs);
            }
           
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(string Id)
        {
            try
            {
                var staff = _db.Users.Where(u => u.StaffId == Id).Select(u => u.Id).FirstOrDefault();

                var users = await _userManager.FindByIdAsync(staff);


                if (staff == null)
                {
                    return NotFound();
                }
                //return _mapper.Map<StaffModelDTO>(users);
                return Ok(users);
            }
           
            catch (Exception ex)
            {

                return BadRequest(ex);
            }




            // var staff = await _db.Users.FirstOrDefault(x => x.StaffId == Id);
        }


       // [HttpPost("Roles")]
        //public async Task<IActionResult> Registerr()
        //{
        //    if (!_roleManager.RoleExistsAsync(DropDown.Admin).GetAwaiter().GetResult())
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole(DropDown.Admin));
        //        await _roleManager.CreateAsync(new IdentityRole(DropDown.SoftwareDevelopers));
        //        await _roleManager.CreateAsync(new IdentityRole(DropDown.SoftwareTesters));
        //        await _roleManager.CreateAsync(new IdentityRole(DropDown.IndustrialTraining));
        //        await _roleManager.CreateAsync(new IdentityRole(DropDown.OfficeCustodians));
        //        await _roleManager.CreateAsync(new IdentityRole(DropDown.YouthCorpers));
        //        await _roleManager.CreateAsync(new IdentityRole(DropDown.Trainee));
        //    }
        //    return Ok();
        //}



        [HttpPost("register")]

        public async Task<IActionResult> Register([FromForm] StaffModelDTO model)// create another endpoint for register and the staffid will be a foreign key
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var staff = _mapper.Map<StaffModel>(model);

                    var newStaff = _db.Users.Where(u => u.UserName == model.UserName).Select(u => u.Id).FirstOrDefault();
                    var newStaff2 = await _userManager.FindByIdAsync(newStaff);
                    newStaff2.DateOfBirth = model.DateOfBirth;
                    newStaff2.FirstName = model.FirstName;
                    newStaff2.Gender = model.Gender;
                    newStaff2.LastName = model.LastName;
                    newStaff2.PhoneNumber = model.PhoneNumber;
                    newStaff2.SubDepartment = model.SubDepartment;

                    var getCompanyName = _db.CompanyName.FirstOrDefault();
                    var companyName = getCompanyName.CompanyName.Substring(0, 3);
                             
                    StaffModel user = new StaffModel();

                   
                    var departmentName = model.Department.Substring(0, 3);
                    var subDepartmentName = model.SubDepartment.Substring(0, 3);
                    var rnd = new Random();
                    int num = rnd.Next(1000);
                   

                    newStaff2.StaffId = companyName + departmentName + subDepartmentName+num;


                    if (model.ProfilePicture != null)
                    {
                        newStaff2.ProfilePicture = await _fileStorageService.SaveFile(containerName, model.ProfilePicture);
                    }

                    var result = await _userManager.UpdateAsync(newStaff2);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(newStaff2, model.Department);
                       // return Ok(new { userName = register.UserName });
                    }
                    //else
                    //{
                    //    return BadRequest();
                    //}
                    await _db.SaveChangesAsync();
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpPost("Create User")]
        public async Task<IActionResult> CreateUser([FromForm] RegisterModel register)// create another endpoint for register and the staffid will be a foreign key
        {
            if (ModelState.IsValid){
                var newUser = new RegisterModel();
                var staff = new StaffModel();
                staff.UserName = register.UserName;
                staff.Email = register.UserName;

                //newUser.StaffId = register.StaffId;
                //var staff = _db.Users.Where(u => u.StaffId == register.StaffId).Select(u => u.Id).FirstOrDefault();
                //var user = await _userManager.FindByIdAsync(staff);

                //user.Email = register.UserName;
                //user.UserName = register.UserName;
                var result = await _userManager.CreateAsync(staff,register.Password);
                //await _userManager.AddPasswordAsync(user, register.Password);


            }
            return Ok();
           


        }




        [HttpPut("UpdateStaffLoginInf0")]//the password isnt changing
        public async Task<IActionResult> EditLoginInfo(string Email, [FromForm] RegisterModel register)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new RegisterModel();
                    var staff = new StaffModel();

                    var editPassword = _db.Users
                              .Where(u => u.UserName == Email)
                              .Select(u => u.Id)
                              .FirstOrDefault();


                    var users = _userManager.FindByIdAsync(editPassword);



                    //var result = await _userManager.UpdateAsync(register);

                    await _userManager.RemovePasswordAsync(staff);
                    await _userManager.AddPasswordAsync(staff, "newpassword");
                    



                }
                return Ok();
            }
            
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

        }


        
       

        [HttpPut("updateStaffInfo")]//not working for now
        public async Task<IActionResult> Update(string Id,[FromForm] StaffModelDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   // var staff = _mapper.Map<StaffModel>(model);
                    var staff = new StaffModel();

                    var getCompanyName = _db.CompanyName.FirstOrDefault();
                    var companyName = getCompanyName.CompanyName.Substring(0, 3);



                    var departmentName = model.Department.Substring(0, 3);
                    var subDepartmentName = model.SubDepartment.Substring(0, 3);
                    var rnd = new Random();
                    int num = rnd.Next(1000);




                    //to get the Id of the staff from the database
                    var del = _db.Users
                           .Where(u => u.StaffId == Id)
                           .Select(u => u.Id)
                           .FirstOrDefault();
                    

                    //get User Data from del (using the Id to get the column)
                    var users = await _userManager.FindByIdAsync(del);
                   // users.StaffId=
                   
                    users.DateOfBirth = model.DateOfBirth;
                    users.FirstName = model.FirstName;
                    users.LastName = model.LastName;
                    users.Gender = model.Gender;
                    users.PhoneNumber = model.PhoneNumber;
                    users.StaffId = companyName + departmentName + subDepartmentName + num;



                    if (model.ProfilePicture != null)
                    {
                        staff.ProfilePicture = await _fileStorageService.SaveFile(containerName, model.ProfilePicture);
                    }

                    //update the column with the new information
                    var result=await _userManager.UpdateAsync(users);
                    var role = await _userManager.GetRolesAsync(users);
                   
                    await _userManager.RemoveFromRolesAsync(users, role);



                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(users, model.Department);
                        await _db.SaveChangesAsync();
                    }
                   
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        var jwt = new JwtSecurityToken();
                        return Ok(new
                        {
                            userName = login.Email,
                            token = new JwtSecurityTokenHandler().WriteToken(jwt)
                        });
                    }
                    else
                    {
                        return BadRequest(new { Error = "Invalid Username or Password" });
                    }

                   
                }
                else
                {
                    return BadRequest(ModelState);
                }


            }

            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromForm] LoginModel login)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, lockoutOnFailure: false);
        //            if (result.Succeeded)
        //            {
        //                return Ok(new { userName = login.Email });
        //            }
        //            else
        //            {
        //                return BadRequest(new { Error = "Invalid Username or Password" });
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest(ModelState);
        //        }
        //    }

        //    catch (Exception ex)
        //    {

        //        return BadRequest(ex);
        //    }
        //}

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return NoContent();
        }

        [HttpPost]
        [Route("Change-Password")]
        public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return NotFound();
            }
            if (string.Compare(model.NewPassword, model.ConfirmNewPAssword) != 0)
            {
                return NotFound();
            }

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                var errors = new List<string>();

                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }


        //reset admin password
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("reset-password-admin")]
        public async Task<IActionResult> resetPasswordForAdmin([FromForm] ResetPasswordModelForAdmin reset)
        {
            var user = await _userManager.FindByEmailAsync(reset.Email);
            if (user == null)
            {
                return NotFound();
            }
            if (string.Compare(reset.NewPassword, reset.ConfirmNewPassword) != 0)
            {
                return NotFound();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, reset.NewPassword);

            if (!result.Succeeded)
            {
                var errors = new List<string>();

                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }

        [HttpPost]
        [Route("reset-password-token")]
        public async Task<IActionResult> ResetPasswordToken([FromForm] ResetPasswordTokenModel reset)
        {
            var user = await _userManager.FindByEmailAsync(reset.Email);
            if (user == null)
            {
                return NotFound();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return Ok(new { token = token });
        }

        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordModel reset)
        {
            var user = await _userManager.FindByEmailAsync(reset.Email);
            if (user == null)
            {
                return NotFound();
            }
            if (string.Compare(reset.NewPassword, reset.ConfirmNewPassword) != 0)
            {
                return NotFound();
            }
            if (string.IsNullOrEmpty(reset.Token))
            {
                return NotFound();
            }

            var result = await _userManager.ResetPasswordAsync(user, reset.Token, reset.NewPassword);
            if (!result.Succeeded)
            {
                var errors = new List<string>();

                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok();

        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                throw new ArgumentException($"'{nameof(Id)}' cannot be null or empty.", nameof(Id));
            }

            try
            {

                var del = _db.Users
                            .Where(u => u.StaffId == Id)
                            .Select(u => u.Id)
                            .FirstOrDefault();


                //get User Data from del
                var user = await _userManager.FindByIdAsync(del);



                if (del == null)
                {
                    return NotFound();
                }

                await _userManager.DeleteAsync(user);
                await _db.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex);



            }
        }

    }
}


//        var portfolio = _db.Users
//                   .Where(u => u.UserName == userName)
//                   .Select(u => u.Portfolio)
//                   .FirstOrDefault();
//if(portfolio == null)
//        {
//            return HttpNotFound();
//    }//end of if
//        return View(portfolio);




//}
//}


// _db.Add(staff);
// await _db.SaveChangesAsync();
//return Ok();
// {



//FirstName = model.FirstName,
//LastName = model.LastName,
//DateOfBirth = model.DateOfBirth,
//Email = model.Email,
//Gender = model.Gender,
//PhoneNumber = model.PhoneNumber,
//   ProfilePicture = model.ProfilePicture

//    };
//var staff = new StaffModel
//var user = _mapper.Map<StaffModel>(register);
//user.UserName = register.UserName;
//user.Email = register.UserName;

//{
//    UserName = register.UserName,
//    Email = register.UserName
//};

// var user = new StaffModel { UserName = model.Email, Email = model.Email };

// staff.PasswordHash = model.Password;
// await _signInManager.SignInAsync(staff, isPersistent: false);
//return BadRequest(result.Errors);
// _db.as Add(staff);



//public async Task<ActionResult> DeleteUserd(string userId)
//{
//    if (userId == null)
//    {
//        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//    }

//    //get User Data from Userid
//    var user = await UserManager.FindByIdAsync(userId);

//    //List Logins associated with user
//    var logins = user.Logins;

//    //Gets list of Roles associated with current user
//    var rolesForUser = await UserManager.GetRolesAsync(userId);

//    using (var transaction = context.Database.BeginTransaction())
//    {
//        foreach (var login in logins.ToList())
//        {
//            await UserManager.RemoveLoginAsync(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
//        }

//        if (rolesForUser.Count() > 0)
//        {
//            foreach (var item in rolesForUser.ToList())
//            {
// item should be the name of the role
//                var result = await UserManager.RemoveFromRoleAsync(user.Id, item);
//            }
//        }

//        //Delete User
//        await UserManager.DeleteAsync(user);

//        TempData["Message"] = "User Deleted Successfully. ";
//        TempData["MessageValue"] = "1";
//        //transaction.commit();
//    }

//    return RedirectToAction("UsersWithRoles", "ManageUsers", new { area = "", });
//}
//StaffModel user = new StaffModel();


//var user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
//var users = await _userManager.FindByIdAsync(user.Id);

// var del = _db.Users.Find(StaffId).StaffId;
//var del = await _userManager.FindByIdAsync(Id);


//users.DateOfBirth = model.DateOfBirth;
//users.FirstName = model.FirstName;
//users.LastName = model.LastName;
//users.Gender = model.Gender;
//users.PhoneNumber = model.PhoneNumber;

//var staffId = _db.Users
//       .Where(u => u.Id )
//       .Select(u => u.Id)
//       .FirstOrDefault();

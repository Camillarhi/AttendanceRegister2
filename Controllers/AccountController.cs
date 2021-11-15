using AttendanceRegister2.ApplicationDbContex;
using AttendanceRegister2.DTOs;
using AttendanceRegister2.HelperClass;
using AttendanceRegister2.Model;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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




        [HttpPost("Roles")]
        public async Task<IActionResult> Registerr()
        {
            if (!_roleManager.RoleExistsAsync(DropDown.Admin).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole(DropDown.Admin));
                await _roleManager.CreateAsync(new IdentityRole(DropDown.SoftwareDevelopers));
                await _roleManager.CreateAsync(new IdentityRole(DropDown.SoftwareTesters));
                await _roleManager.CreateAsync(new IdentityRole(DropDown.IndustrialTraining));
                await _roleManager.CreateAsync(new IdentityRole(DropDown.OfficeCustodians));
                await _roleManager.CreateAsync(new IdentityRole(DropDown.Trainiee));
            }
            return Ok();
        }



        [HttpPost("register")]

        public async Task<IActionResult> Register([FromForm] StaffModelDTO model, [FromForm] RegisterModel register)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var staff = _mapper.Map<StaffModel>(model);

                    StaffModel user = new StaffModel();

                    staff.UserName = register.UserName;
                    staff.Email = register.UserName;

                    if (model.ProfilePicture != null)
                    {
                        staff.ProfilePicture = await _fileStorageService.SaveFile(containerName, model.ProfilePicture);
                    }

                    var result = await _userManager.CreateAsync(staff, register.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(staff, model.Department);
                        return Ok(new { userName = register.UserName });
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


        [HttpPut("UpdateStaffLoginInf0")]
        public async Task<IActionResult> EditLoginInfo(string Email, [FromForm] RegisterModel register)
        {
            if (ModelState.IsValid)
            {
                StaffModel user = new StaffModel();

                //user.UserName = register.UserName;
                //user.Email = register.UserName;

                var editPassword = _db.Users
                          .Where(u => u.UserName==Email)
                          .Select(u => u.Id)
                          .FirstOrDefault();

                //var users = await _userManager.FindByIdAsync(del);


                //users.UserName = register.UserName;


                //var result = await _userManager.UpdateAsync(users);


                //if (result.Succeeded)
                //{
                //   // await _userManager.AddToRoleAsync(staff, model.Department);
                //     return Ok(new { userName = register.UserName });
                //}
                // await _userManager.UpdateAsync(user);
                //await _db.SaveChangesAsync();




                //UserManager<IdentityUser> userManager =
                //  new UserManager<IdentityUser>(new UserStore<IdentityUser>());

                var users = _userManager.FindByIdAsync(editPassword);
                

                   await _userManager.RemovePasswordAsync(user);
                   await _userManager.AddPasswordAsync(user, "newpassword");
                


            }
            return Ok();

        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginModel login)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return Ok(new { userName = login.Email });
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

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return NoContent();
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

        [HttpPut("updateStaffInfo")]
        public async Task<IActionResult> Update(string Id,[FromForm] StaffModelDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var staff = _mapper.Map<StaffModel>(model);

                    var del = _db.Users
                           .Where(u => u.StaffId == Id)
                           .Select(u => u.Id)
                           .FirstOrDefault();
                    
                    
                    //get User Data from del
                    var users = await _userManager.FindByIdAsync(del);
                   
                    staff.DateOfBirth = model.DateOfBirth;
                    staff.FirstName = model.FirstName;
                    staff.LastName = model.LastName;
                    staff.Gender = model.Gender;
                    staff.PhoneNumber = model.PhoneNumber;
                   

                    if (model.ProfilePicture != null)
                    {
                        staff.ProfilePicture = await _fileStorageService.SaveFile(containerName, model.ProfilePicture);
                    }

                    var result=await _userManager.UpdateAsync(users);


                   // var result = await _userManager.CreateAsync(user);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(staff, model.Department);
                    }
                    await _db.SaveChangesAsync();
                }
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

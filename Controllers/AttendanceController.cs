using AttendanceRegister2.ApplicationDbContex;
using AttendanceRegister2.DTOs;
using AttendanceRegister2.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceRegister2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        UserManager<StaffModel> _userManager;
        RoleManager<IdentityRole> _roleManager;


        public AttendanceController(ApplicationDbContext db,    UserManager<StaffModel> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        [HttpGet]
        public ActionResult<AttendanceModel> GetAll()
        {
            try
            {
                var attend = (from user in _db.Attendance
                              select new AttendanceModel
                              {
                                  FirstName = user.FirstName,
                                  LastName = user.LastName,
                                  Id = user.Id,
                                  TimeIn = user.TimeIn,
                                  TimeOut = user.TimeOut,
                                  StaffId = user.StaffId

                              }
                          ).ToList();


                //  var users = await _userManager.FindByIdAsync(staffs);
                return Ok(attend);
            }

            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpGet("{Id}")]

        public async Task<ActionResult<AttendanceModel>> Get(string Id)
        {
            try
            {
                var staff = _db.Attendance.Where(u => u.StaffId == Id).Select(u => u.Id).FirstOrDefault();

                //var users = await _db.Attendance.FirstOrDefaultAsync(staff);

                var del = await _db.Attendance.FindAsync(staff);



                //if (Id == null)
                //{
                //    return NotFound();
                //}
                //return _mapper.Map<StaffModelDTO>(users);
                return Ok(del);
            }


            catch (Exception ex)
            {

                return BadRequest(ex);
            }


        }


        [HttpPost("TimeIn")]

        public async Task<IActionResult> TimeIn(string Id, int? Ids)
        {
            try
            {
                var user = new AttendanceModel();
                //var rules = new RulesModel();
                var reasons = new ReasonsModel();
                //var staff = new StaffModelDTO();
                //var roles = new RolesModel();


                var del = _db.Users.Where(u => u.StaffId == Id).FirstOrDefault();
                var dels = _db.Users.Where(u => u.StaffId == Id).Select(u => u.Id).FirstOrDefault();
                var reasonsId = _db.Reasons.Find(Ids);
                var rule = _db.Rules.FirstOrDefault();
                var roleName = _db.Department.FirstOrDefault();
                var users = await _userManager.FindByIdAsync(dels);
                var role = await _userManager.GetRolesAsync(users);



                
              
                
               

                var timeTest = DateTime.Now.Date.AddMinutes(510);



                if (del != null)
                {
                    if (role.Contains(roleName.Department))
                    {
                        if (rule.Id == roleName.RulesModelId)
                        {
                            var startOfDay = rule.StartOfDayHour * 60 + rule.StartOfDayMinutes;
                            var startOfDayGracePeriod = rule.StartOfDayGracePeriod;


                            if (DateTime.Now <= DateTime.Now.Date.AddMinutes(startOfDay) || DateTime.Now <= DateTime.Now.Date.AddMinutes(startOfDayGracePeriod))
                            {
                                user.TimeIn = DateTime.Now;
                                user.LastName = del.LastName;
                                user.FirstName = del.FirstName;
                                user.StaffId = del.StaffId;
                            }
                            else if (DateTime.Now > DateTime.Now.Date.AddMinutes(startOfDayGracePeriod))
                            {
                                user.TimeIn = DateTime.Now;
                                user.LastName = del.LastName;
                                user.FirstName = del.FirstName;
                                user.StaffId = del.StaffId;
                                reasons.Reasons = reasonsId.Reasons;

                                var staffReasons = new StaffsReasonsTableModel();
                                staffReasons.FirstName = user.FirstName;
                                staffReasons.LastName = user.LastName;
                                staffReasons.Reasons = reasons.Reasons;
                                staffReasons.StaffId = user.StaffId;
                                staffReasons.TimeIn = user.TimeIn;

                                await _db.StaffsReasons.AddAsync(staffReasons);
                            }
                            else
                            {
                                return BadRequest();
                            }

                        }
                    }




                }
                else
                {
                    return NotFound();
                }

                await _db.Attendance.AddAsync(user);

                await _db.SaveChangesAsync();

                return Ok();
            }

            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpPut("TimeOut")]
        public async Task<IActionResult> TimeOut(string Id, int? Ids, [FromForm] AttendanceModel attendance)
        {
            try
            {
                var user = new AttendanceModel();
                var reason = new ReasonsModel();
                var userId = _db.Attendance.Find(attendance.Id);
                var del = _db.Users.Where(u => u.StaffId == Id).FirstOrDefault();
                var reasonsId = _db.Reasons.Find(Ids);
                var dels = _db.Users.Where(u => u.StaffId == Id).Select(u => u.Id).FirstOrDefault();
                var rule = _db.Rules.FirstOrDefault();
                var roleName = _db.Department.FirstOrDefault();
                var users = await _userManager.FindByIdAsync(dels);
                var role = await _userManager.GetRolesAsync(users);





                if (userId != null)
                {
                    if (role.Contains(roleName.Department))
                    {
                        if (rule.Id == roleName.RulesModelId)
                        {
                            var endOfDay = rule.EndOfDayHour * 60 + rule.EndOfDayMinutes;
                            var endOfDayGracePeriod = rule.EndOfDayGracePeriod;

                            if (userId.TimeOut == null && userId.StaffId == Id && userId.TimeIn != null)
                            {
                                if (DateTime.Now == DateTime.Now.Date.AddMinutes(endOfDay) || DateTime.Now <= DateTime.Now.Date.AddMinutes(endOfDayGracePeriod))
                                {
                                    userId.TimeOut = DateTime.Now;
                                    attendance.LastName = userId.LastName;
                                    attendance.FirstName = userId.FirstName;
                                    attendance.StaffId = userId.StaffId;
                                }
                                else if (DateTime.Now < DateTime.Now.Date.AddMinutes(endOfDay) || DateTime.Now > DateTime.Now.Date.AddMinutes(endOfDayGracePeriod))
                                {
                                    userId.TimeOut = DateTime.Now;
                                    attendance.LastName = userId.LastName;
                                    attendance.FirstName = userId.FirstName;
                                    attendance.StaffId = userId.StaffId;
                                    reason.Reasons = reasonsId.Reasons;

                                    var staffReasons = new StaffsReasonsTableModel();
                                    staffReasons.FirstName = del.FirstName;
                                    staffReasons.LastName = del.LastName;
                                    staffReasons.Reasons = reasonsId.Reasons;
                                    staffReasons.StaffId = del.StaffId;
                                    staffReasons.TimeOut = userId.TimeOut;

                                    await _db.StaffsReasons.AddAsync(staffReasons);

                                }
                                else
                                {
                                    return BadRequest();
                                }


                            }
                            else
                            {
                                return NotFound();
                            }


                            _db.Attendance.Update(userId);
                            await _db.SaveChangesAsync();

                        }

                    }
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

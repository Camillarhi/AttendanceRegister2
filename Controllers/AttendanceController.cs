//using AttendanceRegister2.ApplicationDbContex;
//using AttendanceRegister2.DTOs;
//using AttendanceRegister2.Model;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace AttendanceRegister2.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AttendanceController : ControllerBase
//    {
//        private readonly ApplicationDbContext _db;

//        public AttendanceController(ApplicationDbContext db)
//        {
//            _db = db;
//        }


//        [HttpGet]
//        public ActionResult<AttendanceModel> GetAll()
//        {
//            try
//            {
//                var attend = (from user in _db.Attendance
//                              select new AttendanceModel
//                              {
//                                  FirstName = user.FirstName,
//                                  LastName = user.LastName,
//                                  Id = user.Id,
//                                  TimeIn = user.TimeIn,
//                                  TimeOut = user.TimeOut,
//                                  StaffId = user.StaffId
                                  
//                              }
//                          ).ToList();
               

//                //  var users = await _userManager.FindByIdAsync(staffs);
//                return Ok(attend);
//            }
            
//             catch (Exception ex)
//            {

//                return BadRequest(ex);
//            }
//        }

//        [HttpGet("{Id}")]

//        public async Task<ActionResult<AttendanceModel>> Get(string Id)
//        {
//            try
//            {
//                var staff = _db.Attendance.Where(u => u.StaffId == Id).Select(u => u.Id).FirstOrDefault();

//                //var users = await _db.Attendance.FirstOrDefaultAsync(staff);

//                var del = await _db.Attendance.FindAsync(staff);



//                //if (Id == null)
//                //{
//                //    return NotFound();
//                //}
//                //return _mapper.Map<StaffModelDTO>(users);
//                return Ok(del);
//            }

           
//             catch (Exception ex)
//            {

//                return BadRequest(ex);
//            }


//        }


//            [HttpPost("TimeIn")]

//        public async Task<IActionResult> TimeIn(string Id,int? Ids, ReasonsModel reasonsModel)
//        {
//            try
//            {
//                var user = new AttendanceModel();
//                var rules = new RulesModel();
//                var reasons = new ReasonsModel();
//                var staff = new StaffModelDTO();
            

//                var del = _db.Users.Where(u => u.StaffId == Id).FirstOrDefault();
//                var reasonsId = _db.Reasons.Find(Ids);
//                var startOfDay= rules.StartOfDayHour

//                var timeTest = DateTime.Now.Date.AddMinutes(510);



//                if (del != null)
//                {
//                    if(DateTime.Now<= DateTime.Now.Date.AddMinutes(70) || DateTime.Now<=rulesId.StartOfDayGracePeriod)
//                    {
//                        user.TimeIn = DateTime.Now;
//                        user.LastName = del.LastName;
//                        user.FirstName = del.FirstName;
//                        user.StaffId = del.StaffId;
//                    }else if (DateTime.Now > rulesId.StartOfDayGracePeriod)
//                    {
//                        user.TimeIn = DateTime.Now;
//                        user.LastName = del.LastName;
//                        user.FirstName = del.FirstName;
//                        user.StaffId = del.StaffId;
//                        reasonsModel.Reasons = reasonsId.Reasons;

//                        var staffReasons = new StaffsReasonsTableModel();
//                        staffReasons.FirstName = user.FirstName;
//                        staffReasons.LastName = user.LastName;
//                        staffReasons.Reasons = reasonsModel.Reasons;
//                        staffReasons.StaffId = user.StaffId;
//                        staffReasons.TimeIn = user.TimeIn;

//                        await _db.StaffsReasons.AddAsync(staffReasons);
//                    }
//                    else
//                    {
//                        return BadRequest();
//                    }
                   

//                }
//                else
//                {
//                    return NotFound();
//                }

//                await _db.Attendance.AddAsync(user);

//                await _db.SaveChangesAsync();

//                return Ok();
//            }
            
//             catch (Exception ex)
//            {

//                return BadRequest(ex);
//            }
//        }

//        [HttpPut("TimeOut")]
//        public async Task<IActionResult> TimeOut(string Id,int Ids,[FromForm] AttendanceModel attendance,ReasonsModel reasonsModel)
//        {
//            try
//            {
//                // var user = new AttendanceModel();
//                var del = _db.Attendance.Find(attendance.Id);
//                var dels = _db.Users.Where(u => u.StaffId == Id).FirstOrDefault();
//                var reasonsId = _db.Reasons.Find(Ids);
//                var rulesId = _db.Rules.Find(1);


//                if (del.TimeOut == null && del.StaffId == Id && del.TimeIn != null)
//                {
//                    if (DateTime.Now == rulesId.EndOfDay|| DateTime.Now <= rulesId.EndOfDayGracePeriod)
//                    {
//                        del.TimeOut = DateTime.Now;
//                        attendance.LastName = del.LastName;
//                        attendance.FirstName = del.FirstName;
//                        attendance.StaffId = del.StaffId;
//                    }
//                    else if(DateTime.Now <rulesId.EndOfDay||DateTime.Now>rulesId.EndOfDayGracePeriod)
//                    {
//                        del.TimeOut = DateTime.Now;
//                        attendance.LastName = del.LastName;
//                        attendance.FirstName = del.FirstName;
//                        attendance.StaffId = del.StaffId;
//                        reasonsModel.Reasons = reasonsId.Reasons;
//                    }
//                    else
//                    {
//                        return BadRequest();
//                    }
                    

//                }
//                else
//                {
//                    return NotFound();
//                }


//                _db.Attendance.Update(del);
//                await _db.SaveChangesAsync();

//                return Ok();
//            }
           
//             catch (Exception ex)
//            {

//                return BadRequest(ex);
//            }

//        }

//    }
//}

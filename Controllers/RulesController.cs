using AttendanceRegister2.ApplicationDbContex;
using AttendanceRegister2.DTOs;
//using AttendanceRegister2.Migrations;
using AttendanceRegister2.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceRegister2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RulesController : ControllerBase
    {
       private readonly ApplicationDbContext _db;

        public RulesController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult<RulesModel> GetAll()
        {
            var rules = (from user in _db.Rules
                         select new RulesModel
                         {
                             StartOfDayHour = user.StartOfDayHour,
                             StartOfDayMinutes = user.StartOfDayMinutes,
                             StartOfDayGracePeriod = user.StartOfDayGracePeriod,
                             EndOfDayHour = user.EndOfDayHour,
                             EndOfDayMinutes = user.EndOfDayMinutes,
                             EndOfDayGracePeriod = user.EndOfDayGracePeriod
                         }
                           ).ToList();
            
            return Ok(rules);
        }


        [HttpGet("{Id}")]

        public ActionResult Get(int? Id)
        {
            if (Id == 0)
            {
                return NotFound();
            }
            var obj = _db.Rules.Find(Id);


            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }


        [HttpPost]

        public ActionResult Post([FromBody] RulesModelDTO rulesModel)
        {
            if (ModelState.IsValid)
            {
                var rules = new RulesModel()
                {
                    StartOfDayHour = rulesModel.StartOfDayHour,
                    StartOfDayMinutes = rulesModel.StartOfDayMinutes,
                    StartOfDayGracePeriod = rulesModel.StartOfDayGracePeriod,
                    EndOfDayHour = rulesModel.EndOfDayHour,
                    EndOfDayMinutes = rulesModel.EndOfDayMinutes,
                    EndOfDayGracePeriod = rulesModel.EndOfDayGracePeriod
                };
                _db.Rules.Add(rules);
                _db.SaveChanges();
            }
            return Ok();
        }


        [HttpPut]

        public ActionResult Put(int Id, [FromBody] RulesModel rulesModel)
        {
            if (ModelState.IsValid)
            {
                var rules = _db.Rules.Find(Id);
                rules.StartOfDayHour = rulesModel.StartOfDayHour;
                rules.StartOfDayMinutes = rulesModel.StartOfDayMinutes;
                rules.StartOfDayGracePeriod = rulesModel.StartOfDayGracePeriod;
                rules.EndOfDayHour = rulesModel.EndOfDayHour;
                rules.EndOfDayMinutes = rulesModel.EndOfDayMinutes;
                rules.EndOfDayGracePeriod = rulesModel.EndOfDayGracePeriod;

                _db.Rules.Update(rules);
                _db.SaveChanges();

            }
            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            var rules = _db.Rules.Find(Id);

            _db.Rules.Remove(rules);
            _db.SaveChanges();
            return Ok();

        }
    }
}

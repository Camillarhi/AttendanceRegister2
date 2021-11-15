using AttendanceRegister2.ApplicationDbContex;
using AttendanceRegister2.DTOs;
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
    public class ReasonsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ReasonsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult<ReasonsModel> GetAll()
        {
            IEnumerable<ReasonsModel> objList = _db.Reasons;
            return Ok(objList);
        }


        [HttpGet("{Id}")]

        public ActionResult Get(int Id)
        {
            if (Id == 0)
            {
                return NotFound();
            }
            var obj = _db.Reasons.Find(Id);


            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }


        [HttpPost]

        public ActionResult Post([FromBody] ReasonsModelDTO reasonsModel)
        {
            if (ModelState.IsValid)
            {
                var reasons = new ReasonsModel()
                {
                    Reasons = reasonsModel.Reasons
                };
                _db.Reasons.Add(reasons);
                _db.SaveChanges();
            }
            return Ok();
        }


        [HttpPut]

        public ActionResult Put(int Id, [FromBody] ReasonsModel reasonsModel)
        {
            if (ModelState.IsValid)
            {
                var reasons = _db.Reasons.Find(Id);
                reasons.Reasons = reasonsModel.Reasons;
                               
                _db.Reasons.Update(reasons);
                _db.SaveChanges();

            }
            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            var reasons = _db.Reasons.Find(Id);

            _db.Reasons.Remove(reasons);
            _db.SaveChanges();
            return Ok();

        }




    }
}

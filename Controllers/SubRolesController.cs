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
    public class SubRolesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        
        public SubRolesController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult<SubRolesModel> GetAll()
        {
           IEnumerable<SubRolesModel> objList = _db.SubDepartment;

            return Ok(objList);
        }

        [HttpGet("{Id}")]

        public ActionResult Get(int Id)
        {
            if (Id == 0)
            {
                return NotFound();
            }
            var obj = _db.SubDepartment.Find(Id);


            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        [HttpPost]

        public ActionResult Post([FromBody] SubRolesModelDTO subRolesModel)
        {
            if (ModelState.IsValid)
            {
                var subDepartment = new SubRolesModel();
                var department = _db.Department.Where(u => u.Department == subRolesModel.Department).Select(u=>u.Department).FirstOrDefault();
                subDepartment.Department = department;
                subDepartment.SubDepartment = subRolesModel.SubDepartment;

                _db.SubDepartment.Add(subDepartment);
                _db.SaveChanges();
            }
            return Ok();
        }


        [HttpPut]

        public ActionResult Put(int Id, [FromBody] SubRolesModel subRolesModel)
        {
            if (ModelState.IsValid)
            {
                var subDepartment = _db.SubDepartment.Find(Id);
                var department = _db.Department.Where(u => u.Department == subRolesModel.Department).Select(u => u.Department).FirstOrDefault();
                subDepartment.Department = department;
                subDepartment.SubDepartment = subRolesModel.SubDepartment;


                _db.SubDepartment.Update(subDepartment);
                _db.SaveChanges();

            }
            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            var subDepartment = _db.SubDepartment.Find(Id);

            _db.SubDepartment.Remove(subDepartment);
            _db.SaveChanges();
            return Ok();

        }
    }
}

﻿using AttendanceRegister2.ApplicationDbContex;
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
    public class CompanyController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public CompanyController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult<CompanyModel> GetAll()
        {
            IEnumerable<CompanyModel> objList = _db.CompanyName;
            return Ok(objList);
        }


        [HttpGet("{Id}")]

        public ActionResult Get(int Id)
        {
            if (Id == 0)
            {
                return NotFound();
            }
            var obj = _db.CompanyName.Find(Id);


            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }


        [HttpPost]

        public ActionResult Post([FromBody] CompanyModelDTO companyModel)
        {
            if (ModelState.IsValid)
            {
                var companyName = new CompanyModel()
                {
                    CompanyName = companyModel.CompanyName
                };
                _db.CompanyName.Add(companyName);
                _db.SaveChanges();
            }
            return Ok();
        }


        [HttpPut]

        public ActionResult Put(int Id, [FromBody] CompanyModel companyModel)
        {
            if (ModelState.IsValid)
            {
                var companyName = _db.CompanyName.Find(Id);
                companyName.CompanyName = companyModel.CompanyName;

                _db.CompanyName.Update(companyName);
                _db.SaveChanges();

            }
            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            var companyName = _db.CompanyName.Find(Id);

            _db.CompanyName.Remove(companyName);
            _db.SaveChanges();
            return Ok();

        }



    }
}

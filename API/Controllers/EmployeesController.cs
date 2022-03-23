using API.Base;
using API.Context;
using API.Models;
//using API.Repository;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EmployeesController : BasesController<Employee, EmployeeRepository, string>
    {
        /*private readonly EmployeeRepository employeeRepository;
        private readonly MyContext myContext;*/

        public EmployeesController(EmployeeRepository employeeRepository) : base(employeeRepository)
        {
           /* this.employeeRepository = employeeRepository;
            this.myContext = myContext;*/
        }
    }
}
    /*public class EmployeesController : ControllerBase
    {
        private readonly EmployeeRepository employeeRepository;
        public EmployeesController(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }


        [HttpPost]
        public ActionResult Post(Employee employee)
        {
            try
            {
                var insert = employeeRepository.Insert(employee);
                return insert switch
                {
                    0 => Ok(new { status = HttpStatusCode.OK, result = employee, message = "Insert Data Successfull" }),
                    1 => BadRequest(new { status = HttpStatusCode.BadRequest, result = employee, message = "Insert Data Failed (NIK Duplicate)" }),
                    2 => BadRequest(new { status = HttpStatusCode.BadRequest, result = employee, message = "Insert Data Failed (Email & Phone Duplicate)" }),
                    3 => BadRequest(new { status = HttpStatusCode.BadRequest, result = employee, message = "Insert Data Failed (Email Duplicate)" }),
                    4 => BadRequest(new { status = HttpStatusCode.BadRequest, result = employee, message = "Insert Data Failed (Phone Duplicate)" }),
                    _ => throw new NotImplementedException(),
                };
            }
            catch 
            {
                return StatusCode(500, new { status = HttpStatusCode.InternalServerError, result = employeeRepository.Get(), message = "terjadi kesalahan" });
            }
           
        }

        [HttpGet]
        public ActionResult Get()
        {
            try {
                int count = employeeRepository.Get().ToList().Count;
                if (count > 0)
                {
                    return Ok(new { status = HttpStatusCode.OK, result = employeeRepository.Get(), message = "Data ditemukan" });
                }
                else
                {
                    return StatusCode(404, new { status = HttpStatusCode.NotFound, result = employeeRepository.Get(), message = "Data tidak ditemukan" });
                    
                }
            }
            catch
            {
                return StatusCode(500, new { status = HttpStatusCode.InternalServerError, result = employeeRepository.Get(), message = "terjadi kesalahan" });
            }
        }

        [HttpGet("{NIK}")]
        public ActionResult GetNIK(string NIK)
        {
            try
            {
                //int count = employeeRepository.Get(NIK).ToList().Count;
                if (employeeRepository.Get(NIK) == null)
                {
                    return StatusCode(404, new { status = HttpStatusCode.NotFound, result = employeeRepository.Get(NIK), message = "Data tidak ditemukan" });
                    
                }
                else
                {

                    return Ok(new { status = HttpStatusCode.OK, result = employeeRepository.Get(NIK), message = "Data ditemukan" });
                }
            }
            catch
            {
                return StatusCode(500, new { status = HttpStatusCode.InternalServerError, result = employeeRepository.Get(NIK), message = "terjadi kesalahan" });
            }
            *//* if (employeeRepository.Get(NIK) == null)
             {
                 return NotFound("data tidak tersedia, silahkan input data terlebih dahulu");
             }
             else
             {
                 return Ok(employeeRepository.Get(NIK));
             }*//*

        }

        [HttpDelete("{NIK}")]
        public ActionResult DeleteNIK(string NIK)
        {
            try
            {
                employeeRepository.Delete(NIK);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentNullException)
                {
                    return StatusCode(404, new { status = HttpStatusCode.NotFound, result = employeeRepository.Get(NIK), message = "Data tidak ada" });
                }
            }
            return Ok(new { status = HttpStatusCode.OK, result = employeeRepository.Get(NIK), message = "Data terhapus" });
        }

        [HttpPut]
        public ActionResult UpdateData(Employee employee)
        {
            try
            {
                employeeRepository.Update(employee);
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateConcurrencyException || ex is DbUpdateException )
                {
                    return StatusCode(404, new { status = HttpStatusCode.NotFound, result = employeeRepository.Get(employee.NIK), message = "Data tidak ada" });
                }
            }
            return Ok(new { status = HttpStatusCode.OK, result = employeeRepository.Get(employee.NIK), message = "Data terupdate" });
        }


        *//*[HttpPatch]
        public ActionResult Patch()*//*
    }
}
*/
using API.Base;
using API.Context;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : BasesController<University, UniversityRepository, int>
    {
        private readonly UniversityRepository universityRepository;
        private readonly MyContext myContext;

        public UniversityController(UniversityRepository universityRepository, MyContext myContext) : base(universityRepository)
        {
            this.universityRepository = universityRepository;
            this.myContext = myContext;
        }
        [HttpPost("GetIdUniv")]
        public ActionResult GetIdUniv(University university)
        {
            int result = universityRepository.GetIdUniv(university);
            if (result > 0)
            {
                return Ok(new { Status = HttpStatusCode.OK, result = university, message = "Berhasil menambahkan data" });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.BadRequest, result = university, message = "Gagal menambahkan data" });
            }
        }
    }
}

using API.Base;
using API.Context;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationController : BasesController<Education, EducationRepository, int>
    {
        /*private readonly EducationRepository educationRepository;
        private readonly MyContext myContext;*/

        public EducationController(EducationRepository educationRepository, MyContext myContext) : base(educationRepository)
        {
            /*this.educationRepository = educationRepository;
            this.myContext = myContext;*/
        }
    }
}

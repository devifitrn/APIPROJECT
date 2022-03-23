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
    public class ProfilingController : BasesController<Profiling, ProfilingRepository, string>
    {
        /*private readonly ProfilingRepository profilingRepository;
        private readonly MyContext myContext;*/

        public ProfilingController(ProfilingRepository profilingRepository, MyContext myContext) : base(profilingRepository)
        {
            /*this.profilingRepository = profilingRepository;
            this.myContext = myContext;*/
        }
    }
}

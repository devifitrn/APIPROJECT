using API.Models;
using Client.Base;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class UniversityController : BaseController<University, UniversityRepository, int>
    {
        private readonly UniversityRepository repository;
        public UniversityController(UniversityRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        
    }
}

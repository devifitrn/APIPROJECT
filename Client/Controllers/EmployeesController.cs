using API.Models;
using Client.Base;
using Client.Repositories.Data;
using Client.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository repository;
        public EmployeesController(EmployeeRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPut]
        public JsonResult UpdateNIK(Employee employee)
        {
            var result = repository.UpdateNIK(employee);
            return Json(result);
        }
    }
}

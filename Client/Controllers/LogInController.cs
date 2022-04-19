using API.Models;
using Client.Base;
using Client.Repositories.Data;
using Client.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class LogInController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository repository;
        public LogInController(AccountRepository repository) : base(repository)
        {
            this.repository = repository;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult login(LogInVM login)
        {
            var results = repository.login(login);
            if (results.JWT == null)
            {
                return Json(results);
            }
            else
            {
                HttpContext.Session.SetString("JWToken", results.JWT);
                // HttpContext.Session.SetString("Name", jwtHandler.GetName(login.JWT));
            }
            //HttpContext.Session.SetString("JWToken", login.JWT);
            //HttpContext.Session.SetString("Name", jwtHandler.GetName(login.JWT));
            //HttpContext.Session.SetString("ProfilePicture", "assets/img/theme/user.png");
            return Json(results);
        }
    }
}

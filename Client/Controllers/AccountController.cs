using API.Models;
using Client.Base;
using Client.Repositories.Data;
using Client.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class AccountController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository repository;
        public AccountController(AccountRepository repository) : base(repository)
        {
            this.repository = repository;

        }

        [Authorize(Roles ="Directur, Manager ")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetRegisteredData()
        {
            var result = await repository.GetRegisteredData();
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetRegisterData(string NIK)
        {
            var result = await repository.GetRegisterData(NIK);
            return Json(result);
        }

        [HttpPost]
        public JsonResult Register(RegistrationVM registrationVM)
        {
            var result = repository.Register(registrationVM);
            return Json(result);
        }

        /*[HttpPost]
        public async Task<IActionResult> Auth(LogInVM login)
        {
            var JWToken = await repository.Auth(login);
            var token = JWToken.IdToken;

            if (token == null)
            {
                TempData["Message"] = JWToken.Message;
                return RedirectToAction("index", "logIn");
            }
            //var results = repository.Auth(login);
            HttpContext.Session.SetString("JWToken", token);
            //HttpContext.Session.SetString("Name", jwtHandler.GetName(token));
            //HttpContext.Session.SetString("ProfilePicture", "assets/img/theme/user.png");

            //HttpContext.Session.SetString("name", repository.JwtName(JWToken.IdToken));
            *//*TempData["name"] = repository.JwtHandler(JWToken.IdToken);
            return RedirectToAction("index", "Home");*//*
            //return Json(results);
            return RedirectToAction("index", "Home");
        }*/

        
    }

}

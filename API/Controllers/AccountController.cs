using API.Base;
using API.Context;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
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
    public class AccountController : BasesController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;
        //private readonly MyContext myContext;

        public AccountController(AccountRepository accountRepository, MyContext myContext) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
            //this.myContext = myContext;
        }

        [HttpPost("Register")]
        public ActionResult Register(RegisterVM registerVM)
        {
            //return Ok(accountRepository.Register(registerVM));
            var register = accountRepository.Register(registerVM);
            return register switch
            {
                0 => Ok(new { status = HttpStatusCode.OK, result = registerVM, message = "Register Data Successfull" }),
                1 => BadRequest(new { status = HttpStatusCode.BadRequest, result = registerVM, message = "Register Data Failed (NIK Duplicate)" }),
                2 => BadRequest(new { status = HttpStatusCode.BadRequest, result = registerVM, message = "Register Data Failed (Email & Phone Duplicate)" }),
                3 => BadRequest(new { status = HttpStatusCode.BadRequest, result = registerVM, message = "Register Data Failed (Email Duplicate)" }),
                4 => BadRequest(new { status = HttpStatusCode.BadRequest, result = registerVM, message = "Register Data Failed (Phone Duplicate)" }),
                _ => BadRequest(new { status = HttpStatusCode.BadRequest, message = "Register Failed" })
            };
        }

        [HttpPost("Login")]
        public ActionResult Login(LoginVM loginVM)
        {
            var login = accountRepository.Login(loginVM);
            return login switch
            {
                0 => Ok(new { status = HttpStatusCode.OK, result = loginVM, message = "Login Successfull" }),
                1 => BadRequest(new { status = HttpStatusCode.BadRequest, result = loginVM, message = "Login Failed (Password salah)" }),
                2 => BadRequest(new { status = HttpStatusCode.BadRequest, result = loginVM, message = "Login Failed (Email dan password salah)" }),
                _ => BadRequest(new { status = HttpStatusCode.BadRequest, message = "error" })
            };

        }

        [HttpGet("MasterEmployeeData")]
        public ActionResult MasterData()
        {
            var getMaster = accountRepository.MasterEmployeeData();
            if (getMaster != null)
            {
                return Ok(new { status = HttpStatusCode.OK, result = getMaster, message = "Data berhasil ditampilkan" });
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = getMaster, message = "Data tidak ditemukan" });
            }
        }
    }
}

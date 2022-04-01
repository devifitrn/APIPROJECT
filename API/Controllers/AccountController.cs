﻿using API.Base;
using API.Context;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
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
                2 => BadRequest(new { status = HttpStatusCode.BadRequest, result = registerVM, message = "Register Data Failed (Email & Phone Duplicate)"}),
                3 => BadRequest(new { status = HttpStatusCode.BadRequest, result = registerVM, message = "Register Data Failed (Email Duplicate)" }),
                4 => BadRequest(new { status = HttpStatusCode.BadRequest, result = registerVM, message = "Register Data Failed (Phone Duplicate)" }),
                5 => BadRequest(new { status = HttpStatusCode.BadRequest, result = registerVM, message = "Register Data Failed (RoleId Wrong!!)" }),
                _ => BadRequest(new { status = HttpStatusCode.BadRequest, message = "Register Failed" })
            };
        }

        


        [HttpPost("Login")]
        public ActionResult Login(LoginVM loginVM)
        {
            string login = accountRepository.Login(loginVM);
            return login switch
            {
                
                "1" => BadRequest(new { status = HttpStatusCode.BadRequest, result = login, message = "Login Failed (Password salah)" }),
                "2" => BadRequest(new { status = HttpStatusCode.BadRequest, result = login, message = "Login Failed (Email dan password salah)" }),
                _ => Ok(new { status = HttpStatusCode.OK, result = login, message = "Login Successfull" })
                // _ => NotFound(new { status = HttpStatusCode.NotFound, message = "error" })
            };

        }

        [Authorize]
        [HttpGet("TestJWT")]
        public ActionResult TestJWT()
        {
            return Ok("Test JWT berhasil");
        }

        [Authorize(Roles = "Directur, Manager")]
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

        [HttpPost("ForgotPass")]
        public ActionResult ForgotPassword(ForgotPassVM forgotPassVM)
        {
            var getData = accountRepository.ForgotPassword(forgotPassVM);
            return getData switch
            {
                0 => Ok(new { status = HttpStatusCode.OK, result = forgotPassVM, message = "Email has been sent, please check the OTP code in your email!" }),
                1 => BadRequest(new { status = HttpStatusCode.BadRequest, result = forgotPassVM, message = "Email is not registered" }),
                _ => BadRequest(new { status = HttpStatusCode.BadRequest, result = forgotPassVM, message = "Your email is empty" }),
            };
        }

        [HttpPut("ChangePass")]
        public ActionResult ChengePassword(ChangePassVM changePassVM)
        {
            var getData = accountRepository.ChangePassword(changePassVM);
            return getData switch
            {
                0 => Ok(new { status = HttpStatusCode.OK, result = changePassVM, message = "Change Password Success" }),
                1 => BadRequest(new { status = HttpStatusCode.BadRequest, result = changePassVM, message = "OTP expired, Please request OTP again!" }),
                2 => BadRequest(new { status = HttpStatusCode.BadRequest, result = changePassVM, message = "OTP already used, Please request OTP again!" }),
                3 => BadRequest(new { status = HttpStatusCode.BadRequest, result = changePassVM, message = "Wrong OTP, please re-enter the OTP code!" }),
                4 => BadRequest(new { status = HttpStatusCode.BadRequest, result = changePassVM, message = "Email is not registered, please register first!" }),
                5 => BadRequest(new { status = HttpStatusCode.BadRequest, result = changePassVM, message = "password is not match!" }),
                _ => BadRequest(new { status = HttpStatusCode.BadRequest, result = changePassVM, message = "Change Password Failed, your email is empty!" }),
            };
        }


    }
}

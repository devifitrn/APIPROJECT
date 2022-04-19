using API.Base;
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
            
                /*var register = accountRepository.Register(registerVM);
                return register switch
                {
                    0 => Ok(new { status = HttpStatusCode.OK, result = registerVM, message = "Register Data Successfull" }),
                    1 => BadRequest(new { status = HttpStatusCode.BadRequest, result = registerVM, message = "Register Data Failed (NIK Duplicate)" }),
                    2 => BadRequest(new { status = HttpStatusCode.BadRequest, result = registerVM, message = "Register Data Failed (Email & Phone Duplicate)" }),
                    3 => BadRequest(new { status = HttpStatusCode.BadRequest, result = registerVM, message = "Register Data Failed (Email Duplicate)" }),
                    4 => BadRequest(new { status = HttpStatusCode.BadRequest, result = registerVM, message = "Register Data Failed (Phone Duplicate)" }),
                    5 => BadRequest(new { status = HttpStatusCode.BadRequest, result = registerVM, message = "Register Data Failed (RoleId Wrong!!)" }),
                    _ => BadRequest(new { status = HttpStatusCode.BadRequest, message = "Register Failed" })
                };*/
            
            return Ok(accountRepository.Register(registerVM));
            
        }

        


        [HttpPost("Login")]
        public ActionResult Login(LoginVM loginVM)
        {
            string login = accountRepository.Login(loginVM);
            return login switch
            {
                
                "1" => BadRequest(new { status = HttpStatusCode.BadRequest, message = "Login Failed (Password salah)" }),
                "2" => BadRequest(new { status = HttpStatusCode.BadRequest, message = "Login Failed (Email dan password salah)" }),
                _ => Ok(new { status = HttpStatusCode.OK, JWT = login, message = "Login Successfull" })
                // _ => NotFound(new { status = HttpStatusCode.NotFound, message = "error" })
            };

        }
        [Authorize(Roles = "Employee, Manager")]
        [HttpGet("TestJWT")]
        public ActionResult TestJWT()
        {
            return Ok("Test JWT berhasil");
        }

        //[Authorize(Roles = "Directur, Manager")]
        [HttpGet("Master")]
        public ActionResult MasterData()
        {
            var getMaster = accountRepository.Master();
            return Ok(getMaster);
            /*if (getMaster != null)
            {
                return Ok(new { status = HttpStatusCode.OK, result = getMaster, message = "Data berhasil ditampilkan" });
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = getMaster, message = "Data tidak ditemukan" });
            }*/
        }

        [HttpGet("master/{nik}")]
        // [Authorize(Roles = "Director, Manager")]
        public ActionResult GetDataByNIK(string nik)
        {
            try
            {
                var result = accountRepository.GetDataByNIK(nik);

                return result switch
                {
                    0 => Ok(new { status = HttpStatusCode.OK, result = result, message = "Get berhasil" }),
                    1 => BadRequest(new { status = HttpStatusCode.BadRequest, message = "Get Failed " }),
                    _ => BadRequest(new { status = HttpStatusCode.BadRequest, message = "Get gagal!" })
                };
            }
            catch (Exception e)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = "error" });
            }
        }

        [HttpPut("master/update")]
        public ActionResult UpdateAccount(UpdateVM updateVM)
        {
            try
            {
                var result = accountRepository.UpdateAccount(updateVM);
                return result switch
                {
                    0 => Ok(new { status = HttpStatusCode.OK, result = result, message = "Update berhasil" }),
                    1 => BadRequest(new { status = HttpStatusCode.BadRequest, message = "Update Failed (pgone duplicate)" }),
                    2 => BadRequest(new { status = HttpStatusCode.BadRequest, message = "Email sudah digunakan!" }),
                    3 => NotFound(new { status = HttpStatusCode.NotFound, message = "Role employee tidak ada, silahkan tambah role employee terlebih dahulu!" }),
                    _ => BadRequest(new { status = HttpStatusCode.BadRequest, message = "Update gagal!" })
                };
            }
            catch (Exception e)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = "error" });
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

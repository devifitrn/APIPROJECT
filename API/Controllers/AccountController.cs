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
    public class AccountController : BasesController<Account, AccountRepository, string>
    {
        /*private readonly AccountRepository accountRepository;
        private readonly MyContext myContext;*/

        public AccountController(AccountRepository accountRepository, MyContext myContext) : base(accountRepository)
        {
            /*this.accountRepository = accountRepository;
            this.myContext = myContext;*/
        }
    }
}

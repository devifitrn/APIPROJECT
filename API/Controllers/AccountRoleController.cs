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
    public class AccountRoleController : BasesController<AccountRole, AccountRoleRepository, int>
    {
        private readonly AccountRoleRepository accountRoleRepository;
        private readonly MyContext myContext;
        public AccountRoleController(AccountRoleRepository accountRoleRepository, MyContext myContext) : base(accountRoleRepository)
        {
            this.accountRoleRepository = accountRoleRepository;
            this.myContext = myContext;
        }

    [Authorize(Roles = "Directur")]
    [HttpPost]
    [Route("AssignManager")]
    public ActionResult AssignManager(AssignManagerVM assignManagerVM)
    {
        var regisManager = accountRoleRepository.AssignManager(assignManagerVM);
        return regisManager switch
        {
            0 => Ok(new { status = HttpStatusCode.OK, result = regisManager, message = "Assign Manager Success" }),
            1 => BadRequest(new { status = HttpStatusCode.BadRequest, result = regisManager, message = "Assign Manager Failed" }),
            _ => BadRequest(new { status = HttpStatusCode.BadRequest, result = regisManager, message = "Assign Manager Failed" }),
        };
    }
    }
}

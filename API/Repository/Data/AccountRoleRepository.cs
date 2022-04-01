using API.Context;
using API.Models;
using API.ViewModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class AccountRoleRepository : GeneralRepository<MyContext, AccountRole, int>
    {
        private readonly MyContext myContext;
        public IConfiguration _configuration;
        public AccountRoleRepository(MyContext myContext, IConfiguration configuration) : base(myContext)
        {
            this.myContext = myContext;
            this._configuration = configuration;
        }

        public int AssignManager(AssignManagerVM assignManagerVM)
        {
            var cekNIK = myContext.Employees.SingleOrDefault(e => e.NIK == assignManagerVM.NIK);
            if (cekNIK != null)
            {
                var cekAccount = myContext.Account.SingleOrDefault(a => a.NIK == cekNIK.NIK);
                var cekAccRole = myContext.AccountRoles.FirstOrDefault(arl => arl.AccountId == cekAccount.NIK);
                var cekRole = myContext.Roles.FirstOrDefault(rl => rl.RoleId == cekAccRole.RoleId);
                var accrl = new AccountRole
                {
                    AccountId = cekNIK.NIK,
                    RoleId = 2
                };
                myContext.AccountRoles.Add(accrl);
                myContext.SaveChanges();
                return 0;
            }else
            {
                return 1;
            }
        }
    }
}

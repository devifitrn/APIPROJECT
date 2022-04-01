using API.Context;
using API.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext myContext;
        public IConfiguration _configuration;
        public EmployeeRepository(MyContext myContext, IConfiguration configuration) : base(myContext)
        {
            this.myContext = myContext;
            this._configuration = configuration;
        }


        /*public int Inserted(Employee employee)
        {
            int count = myContext.Employees.ToList().Count;
            var year = DateTime.Now.Year;
            if (count == 0)
            {
                employee.NIK = year + "001";
                myContext.Employees.Add(employee);
                var result = myContext.SaveChanges();
                return 0;
            }
            else
            {
                string nik = myContext.Employees.ToList().LastOrDefault().NIK;
                int lastNIK = Convert.ToInt32(nik) % year + 1;
                string idNIK = year + lastNIK.ToString("D3");
                var cekNIK = myContext.Employees.Any(e => e.NIK == idNIK);
                var cekEmail = myContext.Employees.Any(e => e.Email == employee.Email);
                var cekPhone = myContext.Employees.Any(e => e.Phone == employee.Phone);
                if (cekNIK)
                {
                    return 1;
                }
                else if (cekEmail && cekPhone)
                {
                    return 2;
                }
                else if (cekEmail)
                {
                    return 3;
                }
                else if (cekPhone)
                {
                    return 4;
                }
                else
                {
                    employee.NIK = idNIK;
                    myContext.Employees.Add(employee);
                    var result = myContext.SaveChanges();
                    return 0;
                }*/
           // }
        //}
    }
}

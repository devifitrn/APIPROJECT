using API.Context;
using API.Models;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public int Delete(string NIK)
        {
            //throw new NotImplementedException();
            var entity = context.Employees.Find(NIK);
            context.Remove(entity);
            var result = context.SaveChanges();
            return result;
        }

        public IEnumerable<Employee> Get()
        {
            //throw new NotImplementedException();
            return context.Employees.ToList();

        }

        public Employee Get(string NIK)
        {
            // throw new NotImplementedException();
            //context.SaveChanges(); SingleOrDefault(e => e.FirstName == NIK);
            return context.Employees.Find(NIK);

        }

        public int Insert(Employee employee)
        {
            int count = context.Employees.ToList().Count;
            var year = DateTime.Now.Year;
            if (count == 0)
            {
                employee.NIK = year + "001";
                context.Employees.Add(employee);
                var result = context.SaveChanges();
                return 0;
            }
            else
            {
                string nik = context.Employees.ToList().LastOrDefault().NIK;
                int lastNIK = Convert.ToInt32(nik) % year + 1;
                string idNIK = year + lastNIK.ToString("D3");
                var cekNIK = context.Employees.Any(e => e.NIK == idNIK);
                var cekEmail = context.Employees.Any(e => e.Email == employee.Email);
                var cekPhone = context.Employees.Any(e => e.Phone == employee.Phone);
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
                    context.Employees.Add(employee);
                    var result = context.SaveChanges();
                    return 0;
                }
            }
        }

        public int Update(Employee employee)
        {
            //throw new NotImplementedException();
            context.Entry(employee).State = EntityState.Modified;
            var result = context.SaveChanges();
            return result;
        }


        private readonly MyContext context;
        public EmployeeRepository(MyContext context)
        {
            this.context = context;
        }

        
    }
    
}

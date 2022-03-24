using API.Context;
using API.Models;
using API.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext myContext;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public int Register(RegisterVM registerVM)
        {
            string NIK = GetNIK();
            var emp = new Employee
            {
                NIK = NIK,
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Phone = registerVM.Phone,
                BirthDate = registerVM.BirthDate,
                Salary = registerVM.Salary,
                Email = registerVM.Email
            };
            var acc = new Account
            {
                NIK = emp.NIK,
                Password = registerVM.Password
            };
            var edc = new Education
            {
                Degree = registerVM.Degree,
                GPA = registerVM.GPA,
                UniversityId = registerVM.UniversityId
            };


            var cekNIK = myContext.Employees.Any(e => e.NIK == NIK);
            var cekEmail = myContext.Employees.Any(e => e.Email == registerVM.Email);
            var cekPhone = myContext.Employees.Any(e => e.Phone == registerVM.Phone);
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
                registerVM.NIK = NIK;
                myContext.Employees.Add(emp);
                myContext.Account.Add(acc);
                myContext.Educations.Add(edc);
                myContext.SaveChanges();
                var pro = new Profiling
                {
                    NIK = emp.NIK,
                    EducationId = edc.id
                };
                myContext.Profillings.Add(pro);
                myContext.SaveChanges();
                return 0;
            }
        }

        public string GetNIK()
        {
            int count = myContext.Employees.ToList().Count;
            var year = DateTime.Now.Year;
            if (count == 0)
            {
                string NIK = year + "001";
                return NIK;
            }
            else
            {
                string nik = myContext.Employees.ToList().LastOrDefault().NIK;
                int lastNIK = Convert.ToInt32(nik) % year + 1;
                string idNIK = year + lastNIK.ToString("D3");
                return idNIK;
            }
        }
      
        public int Login(LoginVM loginVM)
        {
            var cekEmail = myContext.Employees.SingleOrDefault(e => e.Email == loginVM.Email);
            if (cekEmail != null)
            {
                var cekPassword = myContext.Account.SingleOrDefault(e => e.NIK == cekEmail.NIK);
                if (cekPassword.Password == loginVM.Password)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }else
            {
                return 2;
            }
        }

        public IEnumerable MasterEmployeeData()
        {
            var masterData = (from emp in myContext.Employees
                               join pro in myContext.Profillings on emp.NIK equals pro.NIK
                               join edc in myContext.Educations on pro.EducationId equals edc.id
                               join unv in myContext.Universities on edc.UniversityId equals unv.Id
                               select new 
                               {
                                   NIK = emp.NIK,
                                   FullName = emp.FirstName + " " + emp.LastName,
                                   Phone = emp.Phone,
                                   Gender = ((Gender)emp.Gender).ToString(),
                                   Email = emp.Email,
                                   Birthdate = emp.BirthDate,
                                   Salary = emp.Salary,
                                   EducationId = pro.EducationId,
                                   GPA = edc.GPA,
                                   Degree = edc.Degree,
                                   UniversityName = unv.Name
                               }).ToList();
           
                return masterData;
            
            
        }
    }
}
    



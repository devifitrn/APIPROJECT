using API.Context;
using API.Models;
using API.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext myContext;
        public IConfiguration _configuration;
        public AccountRepository(MyContext myContext, IConfiguration configuration) : base(myContext)
        {
            this.myContext = myContext;
            this._configuration = configuration;
        }

        public int Register(RegisterVM registerVM)
        {
            string NIK = GetNIK();
            var emp = new Employee
            {
                NIK = NIK,
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Gender = registerVM.Gender,
                Phone = registerVM.Phone,
                BirthDate = registerVM.BirthDate,
                Salary = registerVM.Salary,
                Email = registerVM.Email
                
            };
            var acc = new Account
            {
                NIK = emp.NIK,
                Password = BCrypt.Net.BCrypt.HashPassword(registerVM.Password)
            };
            var accrole = new AccountRole
            {
                AccountId = acc.NIK,
                RoleId = 3
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
                if (registerVM.RoleId != 3)
                {
                    return 5;
                }
                else
                {
                    registerVM.NIK = NIK;
                    myContext.Employees.Add(emp);
                    myContext.Account.Add(acc);
                    myContext.AccountRoles.Add(accrole);
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

        public string Login(LoginVM loginVM)
        {
            var cekEmail = myContext.Employees.SingleOrDefault(e => e.Email == loginVM.Email);
            if (cekEmail != null)
            {
                var cekPassword = myContext.Account.SingleOrDefault(e => e.NIK == cekEmail.NIK);
                if (BCrypt.Net.BCrypt.Verify(loginVM.Password, Convert.ToString(cekPassword.Password)))
                {
                    var getData = (from emp in myContext.Employees
                                   join acc in myContext.Account on emp.NIK equals acc.NIK
                                   join accrl in myContext.AccountRoles on acc.NIK equals accrl.AccountId
                                   join rl in myContext.Roles on accrl.RoleId equals rl.RoleId
                                   where emp.Email == loginVM.Email
                                   select new
                                   {
                                       email = emp.Email,
                                       roles = rl.RoleName
                                   });
                    var claims = new List<Claim>();
                    claims.Add(new Claim("Email", loginVM.Email));
                    foreach (var item in getData)
                    {
                        claims.Add(new Claim("roles", item.roles));
                    }
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                                _configuration["Jwt:Issuer"],
                                _configuration["Jwt:Audience"],
                                claims,
                                expires: DateTime.UtcNow.AddDays(1),
                                signingCredentials: signIn
                                );
                    var idtoken = new JwtSecurityTokenHandler().WriteToken(token);
                    claims.Add(new Claim("TokenSecurity", idtoken.ToString()));
                    return idtoken;
                }
                else
                {
                    return "1";
                }
            }
            else
            {
                return "2";
            }
        }

       

        public IEnumerable Master()
        {
            var masterData = (from emp in myContext.Employees
                              join acc in myContext.Account on emp.NIK equals acc.NIK
                              //join arl in myContext.AccountRoles on acc.NIK equals arl.AccountId
                              //join rl in myContext.Roles on arl.RoleId equals rl.RoleId
                              join pro in myContext.Profillings on acc.NIK equals pro.NIK
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
                                  UniversityName = unv.Name,
                                  RoleName = (from account in myContext.AccountRoles 
                                              join rl in myContext.Roles on account.RoleId equals rl.RoleId
                                              where account.AccountId == emp.NIK
                                              select new
                                              {
                                                  rl.RoleName
                                              }).Select(x => x.RoleName).ToArray()
                              }).ToList();

            return masterData;
        }

        public int GetDataByNIK(string nik)
        {
            var isExist = myContext.Employees.Find(nik);
            if (isExist == null)
            {
                return 1;
            };

            var getAccount = (
               from employee in myContext.Employees
               join account in myContext.Account
                  on employee.NIK equals account.NIK
               join profiling in myContext.Profillings
                  on account.NIK equals profiling.NIK
               join education in myContext.Educations
                  on profiling.EducationId equals education.id
               join university in myContext.Universities
                  on education.UniversityId equals university.Id
               where employee.NIK == nik
               select new
               {
                   NIK = employee.NIK,
                   FullName = employee.FirstName + " " + employee.LastName,
                   employee.Phone,
                   Gender = ((Gender)employee.Gender).ToString(),
                   employee.Email,
                   employee.BirthDate,
                   employee.Salary,
                   Education_id = profiling.EducationId,
                   education.GPA,
                   education.Degree,
                   UniversityName = university.Name,
                   UniversityId = university.Id,
                   Roles = (from account in myContext.AccountRoles
                            join role in myContext.Roles
                         on account.RoleId equals role.RoleId
                            where account.AccountId == employee.NIK
                            select new
                            {
                                role.RoleName
                            }).Select(x => x.RoleName).ToArray()
               }
            ).FirstOrDefault();

            return 0;

        }

        public int UpdateAccount(UpdateVM updateVM)
        {
            
        /*{
           "FirstName": "Ayu",
           "LastName": "Aulia",
           "Phone": "098834425",
           "BirthDate": "2000-03-23",
           "Gender": 0,
           "Salary": 1800000,
           "Email": "aulia@gmail.com",
           "Password": "password",
           "Degree": "S1",
           "GPA": "3.5",
           "UniversityId": 3
        }*/

    var employee = myContext.Employees.SingleOrDefault(e => e.NIK == updateVM.NIK);
            if (employee != null)
            {
                ValidationCheckForUpdate(updateVM.NIK, updateVM.Phone, "Phone");
                ValidationCheckForUpdate(updateVM.NIK, updateVM.Email, "Email");

        employee.FirstName = updateVM.FirstName;
                employee.LastName = updateVM.LastName;
                employee.Phone = updateVM.Phone;
                employee.BirthDate = updateVM.BirthDate;
                employee.Salary = updateVM.Salary;
                employee.Email = updateVM.Email;

                myContext.SaveChanges();
            }

        return 0;
        }

        private void ValidationCheckForUpdate(string nik, string value, string field)
        {
            if (field == "Phone")
            {
                var checkPhone = myContext.Employees.Any(e => e.Phone == value && e.NIK != nik);
                if (checkPhone) throw new Exception("Nomor telepon sudah digunakan!");
            }
            else if (field == "Email")
            {
                var checkEmail = myContext.Employees.Any(e => e.Email == value && e.NIK != nik);
                if (checkEmail) throw new Exception("Email sudah digunakan!");
            }
        }

        public int ForgotPassword(ForgotPassVM forgotPassVM)
        {
            var cekEmail = myContext.Employees.SingleOrDefault(e => e.Email == forgotPassVM.Email);
            if (cekEmail != null)
            {
                var cekAccount = myContext.Account.SingleOrDefault(e => e.NIK == cekEmail.NIK);

                Random random = new Random();
                cekAccount.OTP = random.Next(0, 1000000);
                cekAccount.Expired = DateTime.Now.AddMinutes(5);
                cekAccount.IsUsed = false;

                string to = forgotPassVM.Email; //To address    
                string from = "devifn18@gmail.com"; //From address    
                MailMessage message = new MailMessage(from, to);
                string mailbody = $"Forgot Password success. Please reset your password, with OTP Code: {cekAccount.OTP} " +
                                  $"| Expired in 5 minutes start from {DateTime.Now.ToString("T")} WIB";
                message.Subject = $"FORGOT PASSWORD";
                message.Body = mailbody;
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
                System.Net.NetworkCredential basicCredential1 = new
                System.Net.NetworkCredential("devifn18@gmail.com", "nurvadila18");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicCredential1;
                client.Send(message);
                myContext.Entry(cekAccount).State = EntityState.Modified;
                myContext.SaveChanges();
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public int ChangePassword(ChangePassVM changePassVM)
        {
            var cekEmail = myContext.Employees.SingleOrDefault(e => e.Email == changePassVM.Email);
            if (cekEmail != null)
            {
                var cekAccount = myContext.Account.SingleOrDefault(a => a.NIK == cekEmail.NIK);
                if (cekAccount.OTP == changePassVM.OTP)
                {
                    if (cekAccount.IsUsed == false)
                    {
                        if (cekAccount.Expired > DateTime.Now)
                        {
                            cekAccount.Password = BCrypt.Net.BCrypt.HashPassword(changePassVM.NewPassword);
                            cekAccount.Password = BCrypt.Net.BCrypt.HashPassword(changePassVM.ConfirmPassword);
                            if (changePassVM.NewPassword != changePassVM.ConfirmPassword)
                            {
                                return 5;
                            }
                            else
                            {
                                cekAccount.IsUsed = true;
                                myContext.Entry(cekAccount).State = EntityState.Modified;
                                myContext.SaveChanges();
                                return 0;
                            }
                        }
                        else
                        {
                            return 1;
                        }
                    }
                    else
                    {
                        return 2;
                    }
                }
                else
                {
                    return 3;
                }
            }
            else
            {
                return 4;
            }
        }
    }
}

        

        /*public void SendEmail(string Email)
        {
            Random random = new Random();
            var OTP = random.Next(0, 100000);
            string to = Email; //To address    
            string from = "devifitrianurvadila@gmail.com"; //From address    
            MailMessage message = new MailMessage(from, to);

            string mailbody = $"Reset Password NETCore berhasil. OTP send : {OTP} ";
            message.Subject = $"FORGOT PASSWORD";
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("devifitrianurvadila@gmail.com", "Defitnur18");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
            }

            catch (Exception ex)
            {
                throw ex;
            }*/

        /*private string GenerateRandomOTP(int iOTPLength, string[] saAllowedCharacters)

         {

             string sOTP = String.Empty;

             string sTempChars = String.Empty;

             Random rand = new Random();

             for (int i = 0; i < iOTPLength; i++)

             {

                 int p = rand.Next(0, saAllowedCharacters.Length);

                 sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];

                 sOTP += sTempChars;

             }

             return sOTP;

         }*/



    

    

    



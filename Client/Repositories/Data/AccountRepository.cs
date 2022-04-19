using API.Models;
using Azure;
using Client.Base;
using Client.ViewModel;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repositories.Data
{
    public class AccountRepository : GeneralRepository<Account, string>
    {
        /*private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;*/

        public AccountRepository(Address address) : base(address, "Account/")
        {
            /*this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };*/
        }

        public async Task<List<RegisterVM>> GetRegisteredData()
        {
            List<RegisterVM> entities = new List<RegisterVM>();

            using (var response = await httpClient.GetAsync(request + "Master"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<RegisterVM>>(apiResponse);
            }
            return entities;
        }

        public async Task<List<RegisterVM>> GetRegisterData(string NIK)
        {
            List<RegisterVM> entities = new List<RegisterVM>();

            using (var response = await httpClient.GetAsync(request + "Master/" + NIK))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<RegisterVM>>(apiResponse);
            }
            return entities;
        }

        public Object Register(RegistrationVM registrationVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(registrationVM), Encoding.UTF8, "application/json");
            Object entities = new Object();
            using (var response = httpClient.PostAsync(request + "Register", content).Result)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                entities = JsonConvert.DeserializeObject<Object>(apiResponse);
            }

            return entities;
        }

      /*  public string JwtHandler(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken result = tokenHandler.ReadJwtToken(token);

            return result.Claims.FirstOrDefault(claim => claim.Type.Equals("name")).Value;
        }*/

        /*public async Task<JWTokenVM> Auth(LogInVM login)
        {
            JWTokenVM token = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            using (var result = httpClient.PostAsync(request + "Login", content).Result)
            {
                string apiResponse = await result.Content.ReadAsStringAsync();
                token = JsonConvert.DeserializeObject<JWTokenVM>(apiResponse);
            }
            return token;
        }*/

        public JWTokenVM login(LogInVM login)
        {
            
            StringContent content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            JWTokenVM token ;
            using (var response = httpClient.PostAsync(request + "Login", content).Result)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                token = JsonConvert.DeserializeObject<JWTokenVM>(apiResponse);
            }
            return token;
        }
    }

}


using API.Models;
using Client.Base;
using Microsoft.AspNetCore.Http;
using System;
//using Client.Repositories.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Repositories.Data
{
    public class UniversityRepository : GeneralRepository<University, int>
    {
        /*private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;*/
        public UniversityRepository(Address address) : base(address, "University/")
        {
            /*this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };*/
        }
    }
}

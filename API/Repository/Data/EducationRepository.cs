using API.Context;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class EducationRepository : GeneralRepository<MyContext, Education, int>
    {
        private readonly MyContext context;
        public EducationRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }
    }
}

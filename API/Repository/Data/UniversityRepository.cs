using API.Context;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class UniversityRepository : GeneralRepository<MyContext, University, int>
    {
        private readonly MyContext myContext;
        public UniversityRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        public int GetIdUniv(University university)
        {
            int count = myContext.Universities.ToList().Count;
            if (count == 0)
            {
                university.Id = 1;
                myContext.Universities.Add(university);
                var result = myContext.SaveChanges();
                return result;
            }
            else
            {
                int IdUniv = myContext.Universities.ToList().LastOrDefault().Id;
                int lastId = IdUniv + 1;
                myContext.Universities.Add(university);
                var result = myContext.SaveChanges();
                return result;
            }
            
        }
    }
}

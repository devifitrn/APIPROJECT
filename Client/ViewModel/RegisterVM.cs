using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public class RegisterVM
    {
        public string NIK { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int Salary { get; set; }


        public string Phone { get; set; }
        public string Birthdate { get; set; }
        public string Gender { get; set; }

        public string Degree { get; set; }
        public float GPA { get; set; }
        public string UniversityName { get; set; }

    }
    /*public enum Gender
    {
        Male,
        Female 
    }*/
}

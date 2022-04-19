using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public class RegistrationVM
    {
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime Birthdate { get; set; }
        public int Salary { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string Degree { get; set; }
        public float GPA { get; set; }
        public int UniversityId { get; set; }
        
    }
    public enum Gender
    {
        Male,
        Female
    }
}

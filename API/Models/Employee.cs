using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("Employee")] // nama tabel
    public class Employee
    {
        [Key]
        public string NIK { get; set; } //primary key
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public Gender Gender {get; set;}
        public Account Account { get; set; }
    
    }
    public enum Gender
    {
        Male,
        Female
    }
}

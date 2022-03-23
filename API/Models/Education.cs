using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("Education")]
    public class Education
    {
        [Key, Required]
        public int id { get; set; }
        [Required]
        public string Degree { get; set; }
        [Required]
        public string GPA { get; set; }
        public int UniversityId { get; set; }
        public ICollection<Profiling> Profilings { get; set; }
        public University University { get; set; }
    }
}

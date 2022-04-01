using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("Account")]
    public class Account
    {
        [Key, Required]
        public string NIK { get; set; } 
        [Required]
        public string Password { get; set; }

        [JsonIgnore]
        public virtual Employee Employee { get; set; }
        [JsonIgnore]
        public virtual Profiling Profiling { get; set; }
        public int OTP { get; set; }
        public bool IsUsed { get; set; }
        public DateTime Expired { get; set; }
        [JsonIgnore]
        public virtual ICollection<AccountRole> AccountRole { get; set; }
    }
}

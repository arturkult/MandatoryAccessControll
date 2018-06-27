using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BSK_Projekt2.Models
{
    public class User
    {
        public User()
        {
            this.Invoices = new HashSet<Invoice>();
            this.Cars = new HashSet<Car>();
            this.Repairs = new HashSet<Repair>();
            this.Clearance = 1;
        }
        
        [Required]
        public Int32 Clearance { get; set; }
        [Required]
        [Key]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; } 

        public virtual ICollection<Car> Cars { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }

        public virtual ICollection<Repair> Repairs { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BSK_Projekt2.Models
{
    public class Invoice
    {
        public Invoice()
        {
            Repairs = new HashSet<Repair>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("Cena na fakturze")]
        [Required]
        public double Cost { get; set; }
        [Required]
        public virtual ICollection<Repair> Repairs { get; set; }
        [DisplayName("Wystawiajacy")]
        public virtual User Issuer { get; set; }
    }
}
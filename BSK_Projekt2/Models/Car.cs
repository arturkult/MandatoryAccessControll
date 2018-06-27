using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BSK_Projekt2.Models
{
    public class Car
    {
        public Car()
        {
            Repairs = new HashSet<Repair>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [DisplayName("Marka")]
        public string Brand { get; set; }
        [DisplayName("Model")]
        [Required]
        public string Model { get; set; }
        public virtual ICollection<Repair> Repairs { get; set; }
    }
}
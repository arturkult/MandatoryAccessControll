using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BSK_Projekt2.Models
{
    public class Repair
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get;set;}

        [Required]
        [DisplayName("Opis naprawy")]
        public string Description { get; set; }
        [DisplayName("Koszt naprawy")]
        public double Cost { get; set; }
        [DisplayName("Naprawiany samochod")]
        public virtual Car RepairedCar { get; set; }
        [DisplayName("Dodane do faktury")]
        public virtual Invoice Invoice { get; set; }
        [DisplayName("Mechanik")]
        public virtual User Mechanic { get; set; }
        
    }
}
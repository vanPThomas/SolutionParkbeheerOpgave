using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ParkDataLayer.Model
{
    public class HuisEF
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(250)]
        public string Straat { get; set; }

        [Required]
        public int Nr { get; set; }

        [Required]
        public bool Actief { get; set; }

        public ParkEF Park { get; set; }

        public ICollection<HuurcontractEF> Huurcontracten { get; set; } =
            new List<HuurcontractEF>();
    }
}

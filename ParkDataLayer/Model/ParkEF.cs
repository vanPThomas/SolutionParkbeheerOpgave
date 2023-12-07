using ParkBusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Model
{
    public class ParkEF
    {
        [Required]
        [MaxLength(20)]
        public string Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Naam { get; set; }

        [MaxLength(500)]
        public string Locatie { get; set; }
        public ICollection<HuisEF> _huis { get; set; } = new List<HuisEF>() { };
    }
}

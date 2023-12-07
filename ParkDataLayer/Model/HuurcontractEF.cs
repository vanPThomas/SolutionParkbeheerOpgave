using System;
using System.ComponentModel.DataAnnotations;

namespace ParkDataLayer.Model
{
    public class HuurcontractEF
    {
        [Key]
        [MaxLength(25)]
        public string Id { get; private set; }

        public HuurderEF Huurder { get; set; }
        public HuisEF Huis { get; set; }

        [Required]
        public DateTime StartDatum { get; set; }

        [Required]
        public DateTime EindDatum { get; set; }

        [Required]
        public int Aantaldagen { get; set; }
    }
}

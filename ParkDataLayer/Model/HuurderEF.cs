using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ParkDataLayer.Model
{
    public class HuurderEF
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Naam { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(100)]
        public string Tel { get; set; }

        [MaxLength(100)]
        public string Adres { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MagicVila_Web.Models.Dto.VilaDto
{
    public class VilaCreateDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Details { get; set; }
        [Required]
        public int Rate { get; set; }
        public int Occupancy { get; set; }
        public int Sqft { get; set; }
        public string ImageUrl { get; set; }
        public string Amenity { get; set; }
    }
}

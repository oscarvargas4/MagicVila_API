using System.ComponentModel.DataAnnotations;

namespace MagicVila_VilaAPI.Models.Dto
{
    public class VilaNumberCreateDto
    {
        [Required]
        public int VilaNo { get; set; }
        [Required]
        public int VilaID { get; set; }
        public string SpecialDetails { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MagicVila_VilaAPI.Models.Dto.VilaNumberDto
{
    public class VilaNumberUpdateDto
    {
        [Required]
        public int VilaNumber { get; set; }
        public string SpecialDetails { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MagicVila_Web.Models.Dto.VilaNumberDto
{
    public class VilaNumberDto
    {
        [Required]
        public int VilaNo { get; set; }
        [Required]
        public int VilaID { get; set; }
        public string SpecialDetails { get; set; }
    }
}

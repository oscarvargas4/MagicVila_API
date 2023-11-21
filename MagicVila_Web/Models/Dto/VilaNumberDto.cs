using MagicVila_Web.Models.Dto;
using System.ComponentModel.DataAnnotations;

namespace MagicVila_Web.Models.Dto
{
    public class VilaNumberDto
    {
        [Required]
        public int VilaNo { get; set; }
        [Required]
        public int VilaID { get; set; }
        public string SpecialDetails { get; set; }
        public VilaDto Vila { get; set; }
    }
}

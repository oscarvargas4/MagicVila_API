using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVila_VilaAPI.Models
{
    public class VilaNumber
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VilaNo { get; set; }
        public string SpecialDetails { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}

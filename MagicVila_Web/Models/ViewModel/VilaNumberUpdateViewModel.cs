using MagicVila_Web.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVila_Web.Models.ViewModel
{
    public class VilaNumberUpdateViewModel
    {
        public VilaNumberUpdateViewModel()
        {
            VilaNumber = new VilaNumberUpdateDto();
        }

        public VilaNumberUpdateDto VilaNumber { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> VilaList { get; set; }
    }
}

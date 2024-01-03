using Microsoft.AspNetCore.Identity;

namespace MagicVila_VilaAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}

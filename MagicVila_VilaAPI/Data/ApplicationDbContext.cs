using MagicVila_VilaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVila_VilaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Vila> Vilas { get; set; }
    }
}

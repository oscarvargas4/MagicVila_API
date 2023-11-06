using MagicVila_VilaAPI.Data;
using MagicVila_VilaAPI.Models;
using MagicVila_VilaAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVila_VilaAPI.Repository
{
    public class VilaNumberRepository : Repository<VilaNumber>, IVilaNumberRepository
    {
        private readonly ApplicationDbContext _db;
        public VilaNumberRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        

        public async Task<VilaNumber> UpdateAsync(VilaNumber entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.VilaNumbers.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}

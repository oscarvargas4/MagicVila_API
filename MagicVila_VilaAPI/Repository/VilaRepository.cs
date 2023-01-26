using MagicVila_VilaAPI.Data;
using MagicVila_VilaAPI.Models;
using MagicVila_VilaAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVila_VilaAPI.Repository
{
    public class VilaRepository : Repository<Vila>, IVilaRepository
    {
        private readonly ApplicationDbContext _db;
        public VilaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        

        public async Task<Vila> UpdateAsync(Vila entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Vilas.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}

using MagicVila_VilaAPI.Data;
using MagicVila_VilaAPI.Models;
using MagicVila_VilaAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVila_VilaAPI.Repository
{
    public class VilaRepository : IVilaRepository
    {
        private readonly ApplicationDbContext _db;
        public VilaRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task CreateAsync(Vila entity)
        {
            await _db.Vilas.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<Vila> GetAsync(Expression<Func<Vila,bool>> filter = null, bool tracked = true)
        {
            IQueryable<Vila> query = _db.Vilas;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Vila>> GetAllAsync(Expression<Func<Vila,bool>> filter = null)
        {
            IQueryable<Vila> query = _db.Vilas;
            if (filter != null)
            {
            query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task RemoveAsync(Vila entity)
        {
            _db.Vilas.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Vila entity)
        {
            _db.Vilas.Update(entity);
            await SaveAsync();
        }
    }
}

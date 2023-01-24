using MagicVila_VilaAPI.Models;
using System.Linq.Expressions;

namespace MagicVila_VilaAPI.Repository.IRepository
{
    public interface IVilaRepository
    {
        Task<Vila> GetAsync(Expression<Func<Vila,bool>> filter = null, bool tracked = true);
        Task<List<Vila>> GetAllAsync(Expression<Func<Vila,bool>> filter = null);
        Task CreateAsync(Vila entity);
        Task UpdateAsync(Vila entity);
        Task RemoveAsync(Vila entity);
        Task SaveAsync();
    }
}

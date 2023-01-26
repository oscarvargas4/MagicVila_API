using MagicVila_VilaAPI.Models;
using System.Linq.Expressions;

namespace MagicVila_VilaAPI.Repository.IRepository
{
    public interface IVilaRepository : IRepository<Vila>
    {
        Task<Vila> UpdateAsync(Vila entity);
    }
}

using MagicVila_VilaAPI.Models;
using System.Linq.Expressions;

namespace MagicVila_VilaAPI.Repository.IRepository
{
    public interface IVilaNumberRepository : IRepository<VilaNumber>
    {
        Task<VilaNumber> UpdateAsync(VilaNumber entity);
    }
}

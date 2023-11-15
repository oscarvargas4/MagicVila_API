using MagicVila_Web.Models.Dto.VilaDto;

namespace MagicVila_Web.Services.IServices
{
    public interface IVilaService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(VilaCreateDto dto);
        Task<T> UpdateAsync<T>(VilaUpdateDto dto);
        Task<T> DeleteAsync<T>(int id);
    }
}

using MagicVila_Web.Models.Dto;

namespace MagicVila_Web.Services.IServices
{
    public interface IVilaService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(VilaCreateDto dto, string token);
        Task<T> UpdateAsync<T>(VilaUpdateDto dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}

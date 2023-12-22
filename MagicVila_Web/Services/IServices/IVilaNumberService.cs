using MagicVila_Web.Models.Dto;

namespace MagicVila_Web.Services.IServices
{
    public interface IVilaNumberService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(VilaNumberCreateDto dto, string token);
        Task<T> UpdateAsync<T>(VilaNumberUpdateDto dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}

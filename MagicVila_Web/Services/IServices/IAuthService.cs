using MagicVila_Web.Models;
using MagicVila_Web.Models.Dto;

namespace MagicVila_Web.Services.IServices
{
    public interface IAuthService
    {
        Task<T> LoginAsync<T>(LoginRequestDto objToCreate);
        Task<T> RegisterAsync<T>(RegistrationRequestDto objToCreate);
    }
}

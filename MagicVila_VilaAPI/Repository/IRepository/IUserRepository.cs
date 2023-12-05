using MagicVila_VilaAPI.Models;
using MagicVila_VilaAPI.Models.Dto;

namespace MagicVila_VilaAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<LocalUser> Register(RegistrationRequestDto registrationRequestDto);
    }
}

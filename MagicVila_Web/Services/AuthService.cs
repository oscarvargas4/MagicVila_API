using MagicVila_Utility;
using MagicVila_Web.Models;
using MagicVila_Web.Models.Dto;
using MagicVila_Web.Services.IServices;

namespace MagicVila_Web.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string vilaUrl;
        public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            vilaUrl = configuration.GetValue<string>("ServiceUrls:VilaAPI");
        }
        public Task<T> LoginAsync<T>(LoginRequestDto obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = vilaUrl + "/api/UserAuth/login"
            });
        }

        public Task<T> RegisterAsync<T>(UserDto obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = vilaUrl + "/api/UserAuth/register"
            });
        }
    }
}

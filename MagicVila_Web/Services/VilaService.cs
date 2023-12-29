using MagicVila_Utility;
using MagicVila_Web.Models;
using MagicVila_Web.Models.Dto;
using MagicVila_Web.Services.IServices;

namespace MagicVila_Web.Services
{
    public class VilaService : BaseService, IVilaService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string vilaUrl;
        public VilaService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            vilaUrl = configuration.GetValue<string>("ServiceUrls:VilaAPI");
        }

        public Task<T> CreateAsync<T>(VilaCreateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = vilaUrl + "/api/v1/VilaAPI",
                Token = token,
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = vilaUrl + "/api/v1/VilaAPI/" + id,
                Token = token,
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = vilaUrl + "/api/v1/VilaAPI",
                Token = token,
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = vilaUrl + "/api/v1/VilaAPI/" + id,
                Token = token,
            });
        }

        public Task<T> UpdateAsync<T>(VilaUpdateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = vilaUrl + "/api/v1/VilaAPI/" + dto.Id,
                Token = token,
            });
        }
    }
}

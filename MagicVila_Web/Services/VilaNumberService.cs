using MagicVila_Utility;
using MagicVila_Web.Models;
using MagicVila_Web.Models.Dto;
using MagicVila_Web.Services.IServices;
using Newtonsoft.Json.Linq;

namespace MagicVila_Web.Services
{
    public class VilaNumberService : BaseService, IVilaNumberService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string vilaUrl;
        public VilaNumberService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            vilaUrl = configuration.GetValue<string>("ServiceUrls:VilaAPI");
        }

        public Task<T> CreateAsync<T>(VilaNumberCreateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = vilaUrl + "/api/VilaNumberAPI",
                Token = token,
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = vilaUrl + "/api/VilaNumberAPI/" + id,
                Token = token,
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = vilaUrl + "/api/VilaNumberAPI",
                Token = token,
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = vilaUrl + "/api/VilaNumberAPI/" + id,
                Token = token,
            });
        }

        public Task<T> UpdateAsync<T>(VilaNumberUpdateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = vilaUrl + "/api/VilaNumberAPI/" + dto.VilaNo,
                Token = token,
            });
        }
    }
}

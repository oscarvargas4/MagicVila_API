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

        public Task<T> CreateAsync<T>(VilaCreateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = vilaUrl + "/api/VilaAPI"
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = vilaUrl + "/api/VilaAPI/" + id
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = vilaUrl + "/api/VilaAPI"
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = vilaUrl + "/api/VilaAPI/" + id
            });
        }

        public Task<T> UpdateAsync<T>(VilaUpdateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = vilaUrl + "/api/VilaAPI/" + dto.Id
            });
        }
    }
}

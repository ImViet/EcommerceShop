using System.Text;
using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.OrderDtos;
using EcommerceShop.WebApp.Interfaces;
using Newtonsoft.Json;

namespace EcommerceShop.WebApp.Services
{
    public class OrderApiService : IOrderApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public OrderApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ApiResponse<bool>> SaveOrder(CreateOrderDto newOrder)
        {
            var client = _httpClientFactory.CreateClient("myclient");
            var json = JsonConvert.SerializeObject(newOrder);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "/api/order/createorder";
            var response = await client.PostAsync(url, httpContent);
            var data = await response.Content.ReadAsStringAsync();
            if(response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResponse<bool>>(data);
            return JsonConvert.DeserializeObject<ApiErrorResponse<bool>>(data);
        }
    }
}
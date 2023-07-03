using System.Text;
using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.EnumDtos;
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
        public async Task<ApiResponse<List<OrderDto>>> GetUserOrder(string userName, string email)
        {
            var client = _httpClientFactory.CreateClient("myclient");
            var url = $"/api/order/getuserorder?username={userName}&email={email}";
            var response = await client.GetAsync(url);
            var data = await response.Content.ReadAsStringAsync();
            if(response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiResponse<List<OrderDto>>>(data);
            return JsonConvert.DeserializeObject<ApiResponse<List<OrderDto>>>(data);
        }
        public async Task<ApiResponse<bool>> SaveOrder(OrderCreateDto newOrder)
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
        public async Task<ApiResponse<bool>> UpdateStatus(Guid orderId, OrderStatusDto status)
        {
            var client = _httpClientFactory.CreateClient("myclient");
            var json = JsonConvert.SerializeObject(status);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var url = $"/api/order/updatestatus?orderid={orderId}";
            var response = await client.PutAsync(url, httpContent);
            var data = await response.Content.ReadAsStringAsync();
            if(response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResponse<bool>>(data);
            return JsonConvert.DeserializeObject<ApiErrorResponse<bool>>(data);
        }
    }
}
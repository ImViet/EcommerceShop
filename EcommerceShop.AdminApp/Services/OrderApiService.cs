using System.Text;
using EcommerceShop.AdminApp.Extensions;
using EcommerceShop.AdminApp.Interfaces;
using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.EnumDtos;
using EcommerceShop.Contracts.Dtos.OrderDtos;
using Newtonsoft.Json;

namespace EcommerceShop.AdminApp.Services
{
    public class OrderApiService : BaseHttpClientService, IOrderApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OrderApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory, httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApiResponse<List<OrderDto>>> GetListOrder(OrderStatusDto status)
        {
            var url = $"/api/order/getall?status={status}";
            var result = await GetAsync<ApiResponse<List<OrderDto>>>(url);
            return result;
        }
        public async Task<ApiResponse<bool>> ChangeOrderStatus(Guid orderId, OrderStatusDto status)
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
using System.Net.Http.Headers;
using EcommerceShop.AdminApp.Extensions;
using EcommerceShop.AdminApp.Interfaces;
using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Constants;
using EcommerceShop.Contracts.Dtos;
using EcommerceShop.Contracts.Dtos.ProductDtos;
using EcommerceShop.Contracts.Dtos.RequestDtos;
using Newtonsoft.Json;

namespace EcommerceShop.AdminApp.Services
{
    public class ProductApiService : BaseHttpClientService, IProductApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory, httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApiResponse<PagedResultDto<ProductDto>>> GetAllProduct(ProductPagingRequestDto request)
        {
            var url = $"/api/product/getallpaging?search={request.search}"
            + $"&pageIndex={request.PageIndex}&pageSize={request.PageSize}&languageId={request.LanguageId}";
            var result = await GetAsync<ApiResponse<PagedResultDto<ProductDto>>>(url);
            return result;
        }
        public async Task<ApiResponse<bool>> CreateProduct(ProductCreateDto product)
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient("myclient");
            var languageId = _httpContextAccessor.HttpContext?.Session.GetString("Language"); 
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var requestContent = new MultipartFormDataContent();
            if (product.ThumbnailImage != null)
            {
                byte[] dataImg;
                using (var br = new BinaryReader(product.ThumbnailImage.OpenReadStream()))
                {
                    dataImg = br.ReadBytes((int)product.ThumbnailImage.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(dataImg);
                requestContent.Add(bytes, "thumbnailImage", product.ThumbnailImage.FileName);
            }
            requestContent.Add(new StringContent(product.Price.ToString()), "price");
            requestContent.Add(new StringContent(product.OriginalPrice.ToString()), "originalPrice");
            requestContent.Add(new StringContent(product.Stock.ToString()), "stock");
            requestContent.Add(new StringContent(product.Name.ToString()), "name");
            requestContent.Add(new StringContent(product.Description.ToString()), "description");

            requestContent.Add(new StringContent(product.Details.ToString()), "details");
            requestContent.Add(new StringContent(product.SeoDescription.ToString()), "seoDescription");
            requestContent.Add(new StringContent(product.SeoTitle.ToString()), "seoTitle");
            requestContent.Add(new StringContent(product.SeoAlias.ToString()), "seoAlias");
            requestContent.Add(new StringContent(languageId), "languageId");

            var url = $"/api/product/create";
            var response = await client.PostAsync(url, requestContent);
            var data = await response.Content.ReadAsStringAsync();
            if(response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResponse<bool>>(data);
            return JsonConvert.DeserializeObject<ApiErrorResponse<bool>>(data);
        }
    }
}
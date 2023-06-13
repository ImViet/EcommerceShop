using EcommerceShop.Contracts.Dtos.CartDtos;
using EcommerceShop.WebApp.Interfaces;
using Newtonsoft.Json;

namespace EcommerceShop.WebApp.Services
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductApiService _productService;
        public CartService(IHttpContextAccessor httpContextAccessor, IProductApiService productService)
        {
            _httpContextAccessor = httpContextAccessor;
            _productService = productService;
        }
        public List<CartDto>? GetCart()
        {
            var jsonCart = _httpContextAccessor.HttpContext?.Request.Cookies["cart"];
            if(jsonCart == null)
                return new List<CartDto>();
            return JsonConvert.DeserializeObject<List<CartDto>>(jsonCart);
        }
        public async void AddToCart(string languageId, int productId)
        {
            var product = await _productService.GetProductById(languageId, productId);
            var cart = GetCart();
            var cartItem = cart.Find(x => x.Product.ProductId == productId);
            if(cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                CartDto newItem = new CartDto()
                {
                    Product = product.ResponseObject,
                    Quantity = 1
                };
                cart.Add(newItem);
            }
            SaveCartSession(cart);
        }

        public void ClearCart()
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Delete("cart");
        }

        public int CountItem()
        {
            var cart = GetCart();
            var countItem = 0;
            if(cart.Count > 0)
            {
                foreach (var item in cart)
                {
                    countItem += item.Quantity;
                }
                return countItem;
            }
            return 0;
        }


        public void RemoveItem(int productId)
        {
            var cart = GetCart();
            var product = cart.Find(x => x.Product.ProductId == productId);
            cart.Remove(product);
            SaveCartSession(cart);
        }

        public void SaveCartSession(List<CartDto> cart)
        {
            var jsonCart = JsonConvert.SerializeObject(cart);
            _httpContextAccessor.HttpContext.Response.Cookies.Append("cart", jsonCart);
        }
    }
}
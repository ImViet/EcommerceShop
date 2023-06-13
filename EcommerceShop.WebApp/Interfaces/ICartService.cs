using EcommerceShop.Contracts.Dtos.CartDtos;

namespace EcommerceShop.WebApp.Interfaces
{
    public interface ICartService
    {
        List<CartDto> GetCart();
        void AddToCart(string languageId, int productId);
        void RemoveItem(int productId);
        void ClearCart();
        void SaveCartCookies(List<CartDto> cart);
        int CountItem();
    }
}
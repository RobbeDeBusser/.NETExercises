using MusicStore.Models;

namespace MusicStore.Services
{
    public class DiscountNumberOf : IDiscountService
    {
        public int GetDiscount(List<CartItem> cartItems) { 
            if (cartItems.Count() <5)
            {
                return 0;
            } else if (cartItems.Count() < 10) {
                return 5;
            } else
            {
                return 10;
            }
        }
    }
}

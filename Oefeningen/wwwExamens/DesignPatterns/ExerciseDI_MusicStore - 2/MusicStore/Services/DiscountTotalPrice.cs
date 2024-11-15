using MusicStore.Models;

namespace MusicStore.Services
{
    public class DiscountTotalPrice : IDiscountService
    {
        public int GetDiscount(List<CartItem> items)
        {
            int total = 0;
            foreach (CartItem item in items)
            {
                var album = item.Album;
                total += album.Price * item.Count;
            }
            if (total < 25)
            {
                return 0;
            }
            else if (total < 50)
            {
                return 5;
            }
            else
            {
                return 10;
            }
        }
    }
}

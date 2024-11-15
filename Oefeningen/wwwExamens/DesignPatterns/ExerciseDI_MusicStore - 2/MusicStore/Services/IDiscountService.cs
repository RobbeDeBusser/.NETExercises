using MusicStore.Models;

namespace MusicStore.Services
{
    public interface IDiscountService
    {
        int GetDiscount(List<CartItem> items);
    }
}

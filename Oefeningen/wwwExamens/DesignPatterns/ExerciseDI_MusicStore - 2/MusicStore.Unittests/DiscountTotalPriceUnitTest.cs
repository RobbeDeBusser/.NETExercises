using MusicStore.Models;
using MusicStore.Services;
using Moq;

namespace MusicStore.Unittests
{
    [TestClass]
    public class DiscountTotalPriceUnitTest
    {
        [TestMethod]
        public void GetDiscount_ArticlesPrice10_Returns0()
        {
            var cartItem = new CartItem
            {
                CartItemID = 1,
                AlbumID = 308,
                CartKey = "Lol",
                Count = 1,
                DateCreated = DateTime.Now,
            };

            cartItem.Album = new Album
            {
                AlbumID = 308,
                Price = 10,
            };
            var cartItemList = new List<CartItem> { cartItem };
            var discountService = new Mock<IDiscountService>();
            var discountTotalPrice = new DiscountTotalPrice();
            int discount = discountTotalPrice.GetDiscount(cartItemList);

            Assert.AreEqual(0, discount);

        }
        [TestMethod]
        public void GetDiscount_ArticlesPrice30_Returns5()
        {
            var cartItem = new CartItem
            {
                CartItemID = 1,
                AlbumID = 308,
                CartKey = "Lol",
                Count = 3,
                DateCreated = DateTime.Now,
            };

            cartItem.Album = new Album
            {
                AlbumID = 308,
                Price = 10,
            };

            var cartItemList = new List<CartItem> { cartItem };

            var discountService = new Mock<IDiscountService>();

            var discountTotalPrice = new DiscountTotalPrice();
            int discount = discountTotalPrice.GetDiscount(cartItemList);

            Assert.AreEqual(5, discount);
        }
        [TestMethod]
        public void GetDiscount_ArticlesPrice60_Returns10()
        {
            var cartItem = new CartItem
            {
                CartItemID = 1,
                AlbumID = 308,
                CartKey = "Lol",
                Count = 6,
                DateCreated = DateTime.Now,
            };

            cartItem.Album = new Album
            {
                AlbumID = 308,
                Price = 10,
            };

            var cartItemList = new List<CartItem> { cartItem };

            var discountService = new Mock<IDiscountService>();

            var discountTotalPrice = new DiscountTotalPrice();
            int discount = discountTotalPrice.GetDiscount(cartItemList);

            Assert.AreEqual(10, discount);
        }
    }
}
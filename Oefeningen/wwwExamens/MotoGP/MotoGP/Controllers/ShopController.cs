using Microsoft.AspNetCore.Mvc;

namespace MotoGP.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult OrderTicket()
        {
            ViewData["BannerNr"] = 3;
            ViewData["Title"] = "Order Tickets";
            return View();
        }
        public IActionResult ConfirmOrder()
        {
            ViewData["BannerNr"] = 3;
            ViewData["Title"] = "Confirmation";
            return View();
        }
    }
}

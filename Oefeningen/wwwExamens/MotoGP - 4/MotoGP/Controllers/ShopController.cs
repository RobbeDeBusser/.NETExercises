using Microsoft.AspNetCore.Mvc;
using MotoGP.Data;
using MotoGP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace MotoGP.Controllers
{
    public class ShopController : Controller
    {
        private readonly GPContext _context;
        public ShopController(GPContext context)
        {
            _context = context;
        }

        public IActionResult OrderTicket()
        {
            ViewData["BannerNr"] = 3;
            ViewData["Title"] = "Order Tickets";
            ViewData["Countries"] = new SelectList(_context.Countries.OrderBy(c=>c.Name), "CountryID", "Name");
            ViewData["Races"] = new SelectList(_context.Races.OrderBy(r=>r.Name), "RaceID", "Name");
            return View();
        }
        public IActionResult ConfirmOrder(int id)
        {
            ViewData["BannerNr"] = 3;
            ViewData["Title"] = "Confirmation";
            var ticket = _context.Tickets.Include(t=>t.Race).FirstOrDefault(t=>t.TicketID == id);
            return View(ticket);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("TicketID,Name,Email,Address,CountryID,RaceID,Number")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.OrderDate = DateTime.Now;
                ticket.Paid = false;
                _context.Add(ticket);
                _context.SaveChanges();
                return RedirectToAction("ConfirmOrder", new { id = ticket.TicketID });
            }
            return View();
        }
    }
}

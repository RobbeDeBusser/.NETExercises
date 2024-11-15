using Microsoft.AspNetCore.Mvc;
using MotoGP.Data;
using MotoGP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using MotoGP.Models.ViewModels;
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
        public IActionResult ListTickets(int raceID)
        {
            ViewData["Title"] = "Ordered Tickets";
            ViewData["BannerNr"] = 3;
            var SelectTicketVM = new SelectTicketViewModel();
            if(raceID != 0)
            {
                SelectTicketVM.TicketList = _context.Tickets.Include(t=>t.Country).Where(t=>t.RaceID == raceID).OrderBy(t=>t.Name).ToList();
            } else
            {
                SelectTicketVM.TicketList = _context.Tickets.Include(t => t.Country).OrderBy(t => t.Name).ToList();
            }
            SelectTicketVM.Races = new SelectList(_context.Races.OrderBy(r => r.Name), "RaceID", "Name");
            SelectTicketVM.raceID = raceID;
            return View(SelectTicketVM);
        }
        public IActionResult EditTicket(int id)
        {
            ViewData["Title"] = "Edit Ticket";
            ViewData["BannerNr"] = 3;
            var ticket = _context.Tickets.SingleOrDefault(t => t.TicketID == id);
            return View(ticket);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditTicket(int id, [Bind("TicketID,Name,Email,Address,Paid")] Ticket ticket)
        {
            ViewData["BannerNr"] = 3;
            if (ModelState.IsValid)
            {
                var existingTicket = _context.Tickets.SingleOrDefault(t => t.TicketID == id);
                existingTicket.Paid = ticket.Paid;
                _context.Update(existingTicket);
                _context.SaveChanges();
                return RedirectToAction("ListTickets");
            }
            return View(ticket);        
        }
    }
}

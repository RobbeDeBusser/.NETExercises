using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MotoGP.Models;
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
        // GET: Ticket/Add
        public IActionResult OrderTicket()
        {
            ViewData["BannerNr"] = 3;

            var ticket = new Ticket();
            ViewData["Country"] = new SelectList(_context.Countries.OrderBy(r => r.Name), "CountryID", "Name");
            var races = _context.Races.ToList();
            ViewData["Races"] = races;
            return View(ticket);
        }
        // POST: Ticket/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OrderTicket([Bind("Name,Email,Address,CountryID,RaceID,Number,OrderDate")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.OrderDate = DateTime.Now;

                _context.Add(ticket);
                _context.SaveChanges();
                return RedirectToAction("ConfirmOrder", new { id = ticket.TicketID });
            }

            return View(ticket);
        }
        public IActionResult ConfirmOrder(int id)
        {
            ViewData["BannerNr"] = 3;
            var ticket = _context.Tickets
                .Include(t => t.Race)
                .FirstOrDefault(t => t.TicketID == id);
            return View(ticket);
        }

        public IActionResult ListTickets(int raceID)
        {
            ViewData["Title"] = "Ordered Tickets";
            ViewData["BannerNr"] = 3;
            var SelectTicketVM = new ListTicketsViewModel();
            if (raceID != 0)
            {
                SelectTicketVM.TicketList = _context.Tickets.Include(t => t.Country).Where(t => t.RaceID == raceID).OrderBy(t => t.Name).ToList();
            }
            else
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

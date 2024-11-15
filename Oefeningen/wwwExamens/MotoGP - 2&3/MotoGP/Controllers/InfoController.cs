using Microsoft.AspNetCore.Mvc;
using MotoGP.Data;
using MotoGP.Models;
using Microsoft.EntityFrameworkCore;

namespace MotoGP.Controllers
{
    public class InfoController : Controller
    {
        private readonly GPContext _context;
        public InfoController(GPContext context)
        {
            _context = context;
        }
        public IActionResult ListRaces()
        {
            ViewData["BannerNr"] = 0;
            ViewData["Title"] = "Races";
            var races = _context.Races.OrderBy(r=>r.Date);
            return View(races.ToList());
        }
        public IActionResult ShowRace(int id)
        {
            var race = _context.Races.FirstOrDefault(r => r.RaceID == id);
            ViewData["Title"] = "Race - " + race.Name;
            ViewData["BannerNr"] = 0;
            return View(race);
        }
        public IActionResult ListTeams()
        {
            return View();
        }
        public IActionResult ListRiders()
        {
            var riders = _context.Riders.Include(r=>r.Country).OrderBy(r=>r.Number).ToList();
            ViewData["BannerNr"] = 2;
            ViewData["Title"] = "Riders";
            return View(riders);
        }
        public IActionResult BuildMap()
        {
            ViewData["BannerNr"] = 0;
            ViewData["Title"] = "Races on map";
            var races = _context.Races.OrderBy(r => r.Name);
            return View(races.ToList());
        }
    }
}

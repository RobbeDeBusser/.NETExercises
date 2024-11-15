using Microsoft.AspNetCore.Mvc;
using MotoGP.Data;
using MotoGP.Models;
using Microsoft.EntityFrameworkCore;
using MotoGP.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            ViewData["BannerNr"] = 3;
            List<Team> teams = _context.Teams.Include(t=>t.Riders).OrderBy(t=>t.Name).ToList();
            return View(teams);
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
        public IActionResult SelectRace(int raceID = 0)
        {
            ViewData["BannerNr"] = 0;
            ViewData["Title"] = "Races";
            var ListRacesVM = new SelectRaceViewModel();
            if(raceID != 0)
            {
                ListRacesVM.RaceList = _context.Races.Where(r => r.RaceID == raceID).OrderBy(r=>r.Name).ToList();
            }else
            {
                ListRacesVM.RaceList = _context.Races.OrderBy(r => r.Name).ToList();
            }
            ListRacesVM.Races = new SelectList(_context.Races.OrderBy(r => r.Name), "RaceID", "Name");
            ListRacesVM.raceID = raceID;
            return View(ListRacesVM);
        }
        public IActionResult ListTeamsRiders(int? teamID)
        {
            ViewData["BannerNr"] = 3;
            ViewData["Title"] = "Teams & Riders";
            var selectTeamsVM = new SelectTeamsViewModel();
            selectTeamsVM.TeamList = _context.Teams.OrderBy(t => t.Name).ToList();
            if (teamID.HasValue)
            {
                selectTeamsVM.RiderList = _context.Riders.Where(r=>r.TeamID == teamID).Include(r=>r.Country).OrderBy(r=>r.LastName).ToList();            }
            else
            {
                selectTeamsVM.RiderList = new List<Rider>();
            }
            return View(selectTeamsVM);
        }
    }
}

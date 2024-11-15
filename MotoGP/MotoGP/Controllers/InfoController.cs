using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MotoGP.Models.ViewModels;
using MotoGP.Models;

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
            var races = _context.Races.OrderBy(r => r.Date).ToList();
            var groupedRaces = races.GroupBy(r => r.Date.Month);
            return View(groupedRaces);
        }
        public IActionResult BuildMap()
        {
            ViewData["BannerNr"] = 0;
            var races = _context.Races.OrderBy(r => r.Name);
            //var races = new List<Race>
            //{
            //    new Race { RaceID = 1, X = 517, Y = 19, Name = "Assen" },
            //    new Race { RaceID = 2, X = 859, Y = 249, Name = "Losail Circuit" },
            //    new Race { RaceID = 3, X = 194, Y = 428, Name = "Autódromo Termas de Río Hondo" }
            //};

            return View(races.ToList());
        }
        public IActionResult ShowRace(int id)
        {
            ViewData["BannerNr"] = 0;

            var race = _context.Races.FirstOrDefault(r => r.RaceID == id);
            ViewData["Title"] = "Race - " + race.Name;

            return View(race);
        }
        public IActionResult ListRiders()
        {
            ViewData["BannerNr"] = 1;

            var riders = _context.Riders.OrderBy(r => r.Number);

            return View(riders.ToList());
        }
        public IActionResult SelectRace(int selectedRaceID = 0)
        {
            ViewData["BannerNr"] = 1;

            var races = _context.Races.OrderBy(r => r.Name).Select(r => new SelectListItem
            {
                Value = r.RaceID.ToString(),
                Text = r.Name
            }).ToList();

            var viewModel = new SelectRaceViewModel()
            {
                Races = races,
                SelectedRaceID = selectedRaceID,
                SelectedRace = selectedRaceID != 0 ? _context.Races.FirstOrDefault(r => r.RaceID == selectedRaceID) : null
            };

            return View(viewModel);
        }
        public IActionResult ListTeamsRiders(int? teamID)
        {
            ViewData["BannerNr"] = 3;
            ViewData["Title"] = "Teams & Riders";
            var selectTeamsVM = new SelectTeamsViewModel();
            selectTeamsVM.TeamList = _context.Teams.OrderBy(t => t.Name).ToList();
            if (teamID.HasValue)
            {
                selectTeamsVM.RiderList = _context.Riders.Where(r => r.TeamID == teamID).Include(r => r.Country).OrderBy(r => r.LastName).ToList();
            }
            else
            {
                selectTeamsVM.RiderList = new List<Rider>();
            }
            return View(selectTeamsVM);
        }
        public IActionResult ListTeams()
        {
            ViewData["BannerNr"] = 3;
            List<Team> teams = _context.Teams.Include(t => t.Riders).OrderBy(t => t.Name).ToList();
            return View(teams);
        }
    }
}

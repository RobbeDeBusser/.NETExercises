using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MusicStore.Data;
using MusicStore.Models;

namespace MusicStore.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly StoreContext _context;

		public HomeController(ILogger<HomeController> logger, StoreContext context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Index()
		{
			var albums = _context.Albums.OrderBy(a => Guid.NewGuid()).Take(6);

			return View(albums.ToList());
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}

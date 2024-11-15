﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data;
using MusicStore.Models;

namespace MusicStore.Components
{
    [ViewComponent(Name = "GenreMenu")]
    public class GenreMenuComponent : ViewComponent
    {
        private readonly StoreContext _context;

        public GenreMenuComponent(StoreContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var genres = await _context.Genres.Take(8).ToListAsync();

            // Pass the genres as parameters to the view
            return View(genres);
        }
    }
}

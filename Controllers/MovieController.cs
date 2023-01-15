using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieOnDemand.ApplicationDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieOnDemand.Controllers
{
    public class MovieController : Controller
    {
        private readonly AppDbContext _db;

        public MovieController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _db.Movies.Include(m => m.Cinema).OrderBy(m => m.EndDate).ToListAsync();
            return View(movies);
        }
    }
}

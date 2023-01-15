using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieOnDemand.ApplicationDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieOnDemand.Controllers
{
    public class CinemaController : Controller
    {
        private readonly AppDbContext _db;

        public CinemaController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var cinemas = await _db.Cinemas.ToListAsync();
            return View(cinemas);
        }
    }
}

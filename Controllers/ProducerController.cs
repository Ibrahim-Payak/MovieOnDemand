using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieOnDemand.ApplicationDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieOnDemand.Controllers
{
    public class ProducerController : Controller
    {
        private readonly AppDbContext _db;

        public ProducerController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var producers = await _db.Producers.ToListAsync();
            return View(producers);
        }
    }
}

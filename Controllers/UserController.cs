using Microsoft.AspNetCore.Mvc;
using MovieOnDemand.ApplicationDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieOnDemand.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _db;

        public UserController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}

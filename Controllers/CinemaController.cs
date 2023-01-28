using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieOnDemand.ApplicationDbContext;
using MovieOnDemand.Data.Interface;
using MovieOnDemand.Data.Static;
using MovieOnDemand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieOnDemand.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CinemaController : Controller
    {
        private readonly ICinemasService _service;

        public CinemaController(ICinemasService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var cinemas = await _service.GetAllAsync();
            return View(cinemas);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }
            await _service.AddAsync(cinema);
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var cinema = await _service.GetByIdAsync(id);
            if(cinema == null)
            {
                return View("NotFound");
            }
            return View(cinema);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var cinema = await _service.GetByIdAsync(id);
            if (cinema == null)
            {
                return View("NotFound");
            }
            return View(cinema);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }
            await _service.UpdateAsync(cinema);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var cinema = await _service.GetByIdAsync(id);
            if (cinema == null)
            {
                return View("NotFound");
            }
            return View(cinema);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Cinema cinema)
        {
            await _service.DeleteAsync(cinema.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}

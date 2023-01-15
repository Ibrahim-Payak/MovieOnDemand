
using Microsoft.AspNetCore.Mvc;
using MovieOnDemand.ApplicationDbContext;
using MovieOnDemand.Data.Interface;
using MovieOnDemand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieOnDemand.Controllers
{
    public class ActorController : Controller
    {
        private readonly IActorsService _service;

        public ActorController(IActorsService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var actors = await _service.GetAllAsync();
            return View(actors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Create(Actor actor)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(actor);
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }

        //Get Req.: Actor/View/1
        public async Task<IActionResult> View(int id)
        {
            Actor actor = await _service.GetByIdAsync(id);
            if (actor == null)
            {
                return View("NotFound");
            }
            return View(actor);
        }

        //Get Update Method
        public async Task<IActionResult> Edit(int id)
        {
            Actor actor = await _service.GetByIdAsync(id);
            if(actor == null)
            {
                return View("NotFound");
            }
            return View(actor);
        }

        //post update method
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.UpdateAsync(actor);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            Actor actor = await _service.GetByIdAsync(id);
            if(actor == null)
            {
                return View("NotFound");
            }

            return View(actor);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Actor actor)
        {
            if(actor == null)
            {
                return View("NotFound");
            }
            await _service.DeleteAsync(actor.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}

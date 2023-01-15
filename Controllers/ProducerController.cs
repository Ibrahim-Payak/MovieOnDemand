using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieOnDemand.ApplicationDbContext;
using MovieOnDemand.Data.Interface;
using MovieOnDemand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieOnDemand.Controllers
{
    public class ProducerController : Controller
    {
        private readonly IProducersService _service;

        public ProducerController(IProducersService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var producers = await _service.GetAllAsync();
            return View(producers);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var producer = await _service.GetByIdAsync(id);
            if(producer == null)
            {
                return View("NotFound");
            }
            return View(producer);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var producer = await _service.GetByIdAsync(id);
            if (producer == null)
            {
                return View("NotFound");
            }
            return View(producer);
        }

        public async Task<IActionResult> Edit(int id, Producer newproducer)
        {
            if (!ModelState.IsValid)
            {
                return View(newproducer);
            }
            await _service.UpdateAsync(newproducer);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            await _service.AddAsync(producer);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var producer = await _service.GetByIdAsync(id);
            if(producer == null)
            {
                return View("NotFound");
            }
            return View(producer);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Producer producer)
        {
            if(producer == null)
            {
                return View("NotFound");
            }
            await _service.DeleteAsync(producer.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieOnDemand.ApplicationDbContext;
using MovieOnDemand.Data.Interface;
using MovieOnDemand.Data.Static;
using MovieOnDemand.Data.ViewModel;
using MovieOnDemand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieOnDemand.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class MovieController : Controller
    {
        private readonly IMoviesService _service;

        public MovieController(IMoviesService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string searchString)
        {
            var movies = await _service.GetAllAsync(m => m.Cinema);

            if (!string.IsNullOrEmpty(searchString))
            {
                //if searching
                movies = movies.Where(m => m.Name.ToLower().Contains(searchString.ToLower()) || m.Description.ToLower().Contains(searchString.ToLower())).ToList();
            }

            return View(movies);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _service.GetMovieByIdAsync(id);
            return View(movie);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var movieDropdownData = await _service.GetMovieDropDownValues();
            ViewBag.Actors = new SelectList(movieDropdownData.Actors, "Id", "FullName");
            ViewBag.Cinemas = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownData.Producers, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieVM movie)
        {
            if (!ModelState.IsValid)
            {
                //To show data in Dropdown after error
                var movieDropdownData = await _service.GetMovieDropDownValues();
                ViewBag.Actors = new SelectList(movieDropdownData.Actors, "Id", "FullName");
                ViewBag.Cinemas = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownData.Producers, "Id", "FullName");
                return View(movie);
            }
            await _service.AddNewMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _service.GetMovieByIdAsync(id);
            if(movie == null) return View("NotFound");

            //model we need to pass to view page is type MovieVM, convert from Movie to MovieVM
            var res = new MovieVM()
            {
                Id = movie.Id,
                Name = movie.Name,
                Description = movie.Description,
                Price = movie.Price,
                ImageUrl = movie.ImageUrl,
                MovieCategory = movie.MovieCategory,
                StartDate = movie.StartDate,
                EndDate = movie.EndDate,
                ProducerId = movie.ProducerId,
                CinemaId = movie.CinemaId,
                ActorIds = movie.Actors_Movies.Select(m => m.ActorId).ToList()
            };

            var movieDropdownData = await _service.GetMovieDropDownValues();
            ViewBag.Actors = new SelectList(movieDropdownData.Actors, "Id", "FullName");
            ViewBag.Cinemas = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownData.Producers, "Id", "FullName");

            return View(res);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MovieVM newmovie)
        {
            if (!ModelState.IsValid)
            {
                var movieDropdownData = await _service.GetMovieDropDownValues();
                ViewBag.Actors = new SelectList(movieDropdownData.Actors, "Id", "FullName");
                ViewBag.Cinemas = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownData.Producers, "Id", "FullName");

                return View(newmovie);
            }
            await _service.UpdateNewMovieAsync(newmovie);
            return RedirectToAction(nameof(Index));
        }
    }
}

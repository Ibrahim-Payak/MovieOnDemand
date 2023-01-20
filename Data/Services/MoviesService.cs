using Microsoft.EntityFrameworkCore;
using MovieOnDemand.ApplicationDbContext;
using MovieOnDemand.Data.Base;
using MovieOnDemand.Data.Interface;
using MovieOnDemand.Data.ViewModel;
using MovieOnDemand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieOnDemand.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly AppDbContext _db;
        public MoviesService(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task AddNewMovieAsync(MovieVM movieVM)
        {
            var newMovie = new Movie()
            {
                Name = movieVM.Name,
                Price = movieVM.Price,
                ProducerId = movieVM.ProducerId,
                CinemaId = movieVM.CinemaId,
                Description = movieVM.Description,
                StartDate = movieVM.StartDate,
                EndDate = movieVM.EndDate,
                MovieCategory = movieVM.MovieCategory,
                ImageUrl = movieVM.ImageUrl
            };

            await _db.Movies.AddAsync(newMovie);
            await _db.SaveChangesAsync();

            //need to add Actor_Movie

            foreach (var actorId in movieVM.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    //since newMovie save in db, it has id with it
                    MovieId = newMovie.Id,
                    ActorId = actorId
                };
                await _db.Actors_Movies.AddAsync(newActorMovie);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDetails = await _db.Movies
                .Include(c => c.Cinema) //include cinema 
                .Include(p => p.Producer) //include producer
                .Include(am => am.Actors_Movies).ThenInclude(m => m.Actor) //include actors-movies,
                .FirstOrDefaultAsync(m => m.Id == id); //since actor not directly connected to movies, we will use ThenInclude with am

            return movieDetails;
        }

        public async Task<MovieDropDownVM> GetMovieDropDownValues()
        {
            var res = new MovieDropDownVM();
            res.Actors = await _db.Actors.OrderBy(m => m.FullName).ToListAsync();
            res.Producers = await _db.Producers.OrderBy(m => m.FullName).ToListAsync();
            res.Cinemas = await _db.Cinemas.OrderBy(m => m.Name).ToListAsync();

            return res;
        }

        public async Task UpdateNewMovieAsync(MovieVM movieVM)
        {
            var movieDb = await _db.Movies.FirstOrDefaultAsync(m => m.Id == movieVM.Id);

            if(movieDb != null)
            {
                movieDb.Name = movieVM.Name;
                movieDb.Description = movieVM.Description;
                movieDb.Price = movieVM.Price;
                movieDb.ImageUrl = movieVM.ImageUrl;
                movieDb.StartDate = movieVM.StartDate;
                movieDb.EndDate = movieVM.EndDate;
                movieDb.MovieCategory = movieVM.MovieCategory;
                movieDb.ProducerId = movieVM.ProducerId;
                movieDb.CinemaId = movieVM.CinemaId;

                await _db.SaveChangesAsync();
            }

            //remove existing actors
            var existingActors = await _db.Actors_Movies.Where(m => m.MovieId == movieVM.Id).ToListAsync();
            _db.Actors_Movies.RemoveRange(existingActors);
            await _db.SaveChangesAsync();


            //add Movie Actors

            foreach (var actorId in movieVM.ActorIds)
            {
                var actorMovie = new Actor_Movie()
                {
                    MovieId = movieDb.Id, /*or movieVM.Id*/
                    ActorId = actorId
                };
                await _db.AddAsync(actorMovie);
                await _db.SaveChangesAsync();
            }
        }
    }

}

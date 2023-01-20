using MovieOnDemand.Data.Base;
using MovieOnDemand.Data.ViewModel;
using MovieOnDemand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieOnDemand.Data.Interface
{
    public interface IMoviesService : IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);

        Task<MovieDropDownVM> GetMovieDropDownValues();

        Task AddNewMovieAsync(MovieVM movieVM);
        Task UpdateNewMovieAsync(MovieVM movieVM);
    }
}

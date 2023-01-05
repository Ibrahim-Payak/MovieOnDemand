using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MovieOnDemand.ApplicationDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieOnDemand.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                //ensur database is already created
                context.Database.EnsureCreated();
                //Cinema (if data is not present)
                if (!context.Cinemas.Any())
                {

                }
                //Actor
                if (!context.Actors.Any())
                {

                }
                //Movie
                if (!context.Movies.Any())
                {

                }
                //Producer
                if (!context.Producers.Any())
                {

                }
                //Actor_Movie
                if (!context.Actors_Movies.Any())
                {

                }
            }
        }
    }
}

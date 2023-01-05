
using Microsoft.EntityFrameworkCore;
using MovieOnDemand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieOnDemand.ApplicationDbContext
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        //need to add some instuction for Translator (DbContext) for many to many relationship
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //combination of primary key
            modelBuilder.Entity<Actor_Movie>().HasKey(am => new
            {
                am.ActorId,
                am.MovieId
            });

            //defining Joining model in c# side
            //Actor_Movie relation with movie model: Actor_Movie having 1 movie with many Actor_Movie & foreign key is MovieId
            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Movie)
                .WithMany(am => am.Actors_Movies).HasForeignKey(m => m.MovieId);

            //Actor_Movie relation with Actor model: Actor_Movie having 1 Actor with many Actor_Movie & foreign key is ActorId
            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Actor)
                .WithMany(am => am.Actors_Movies).HasForeignKey(a => a.ActorId);

            base.OnModelCreating(modelBuilder);
        }

        //Table name foe each model
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Actor_Movie> Actors_Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Producer> Producers { get; set; }
    }
}

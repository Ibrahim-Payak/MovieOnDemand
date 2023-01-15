using Microsoft.EntityFrameworkCore;
using MovieOnDemand.ApplicationDbContext;
using MovieOnDemand.Data.Base;
using MovieOnDemand.Data.Interface;
using MovieOnDemand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieOnDemand.Data.Services
{
    public class ActorsService : EntityBaseRepository<Actor>, IActorsService
    {
        public ActorsService(AppDbContext db) : base(db)
        {

        }        
    }
}

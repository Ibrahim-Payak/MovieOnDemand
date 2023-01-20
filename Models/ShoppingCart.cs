using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieOnDemand.ApplicationDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieOnDemand.Models
{
    //add data to shoppingCart
    public class ShoppingCart
    {
        private readonly AppDbContext _db;

        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public ShoppingCart(AppDbContext db)
        {
            _db = db;
        }

        //to get all shoppingcartitems
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _db.ShoppingCartItems
                .Where(m => m.ShoppingCartId == ShoppingCartId)
                .Include(m => m.Movie).ToList());
        }

        //to get ShoppingCart total
        public double GetShoppingCartTotal()
        {
            var total = _db.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId)
                        .Select(n => n.Movie.Price * n.Amount).Sum();
            return total;
        }


        public void AddItemToCart(Movie movie)
        {
            //check we already have this item in our cart
            //if yes, then we only need to increase by 1
            //if no, then we need to add item into cart and set 1
            var shoppingCartItem = _db.ShoppingCartItems.FirstOrDefault(m => m.Movie.Id == movie.Id 
                                    && m.ShoppingCartId == ShoppingCartId);
            if(shoppingCartItem == null)
            {
                //first time
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Movie = movie,
                    Amount = 1
                };
                _db.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount += 1;
            }
            _db.SaveChanges();
        }

        public void RemoveItemFromCart(Movie movie)
        {
            var shoppingCartItem = _db.ShoppingCartItems.FirstOrDefault(n => n.Movie == movie
                                    && n.ShoppingCartId == ShoppingCartId);
            if(shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount == 1)
                {
                    _db.ShoppingCartItems.Remove(shoppingCartItem);
                }
                else
                {
                    shoppingCartItem.Amount -= 1;
                }
            }
            _db.SaveChanges();
        }

        //static bcz, we are using this in Startup.cs file
        //services to get session and check we already have or not
        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            //This query get session using ServiceProvider
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public async Task ClearShoppingCartAsync()
        {
            var items = await _db.ShoppingCartItems.Where(m => m.ShoppingCartId == ShoppingCartId).ToListAsync();
            _db.ShoppingCartItems.RemoveRange(items);
            await _db.SaveChangesAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using MovieOnDemand.ApplicationDbContext;
using MovieOnDemand.Data.Interface;
using MovieOnDemand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieOnDemand.Data.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _db;

        public OrderService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
        {
            var orders = await _db.Orders.Include(m => m.OrderItems)
                .ThenInclude(m => m.Movie)
                .Where(m => m.UserId == userId).ToListAsync();

            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string emailId)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = emailId
            };

            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();

            //now order id is created, we will add all items in tht order id

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    MovieId = item.Movie.Id,
                    OrderId = order.Id,
                    Price = item.Movie.Price
                };
                await _db.OrderItems.AddAsync(orderItem);
            }
            await _db.SaveChangesAsync();
        }

    }
}

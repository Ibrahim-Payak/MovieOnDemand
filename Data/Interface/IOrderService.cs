using MovieOnDemand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieOnDemand.Data.Interface
{
    public interface IOrderService
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string emailId);

        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string role);
    }
}

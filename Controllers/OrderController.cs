using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieOnDemand.Data.Interface;
using MovieOnDemand.Data.ViewModel;
using MovieOnDemand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieOnDemand.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrderService _orderService;

        public OrderController(IMoviesService moviesService, ShoppingCart shoppingCart, IOrderService orderService)
        {
            _moviesService = moviesService;
            _shoppingCart = shoppingCart;
            _orderService = orderService;
        }

        public IActionResult ShoppingCart()
        {
            //here return type is diff. than Shopping Cart, so we will create new View model of that type

            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }

        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = await _moviesService.GetMovieByIdAsync(id);

            if(item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> AddItemToCart(int id)
        {
            var item = await _moviesService.GetMovieByIdAsync(id);

            if(item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemoveItemFromCart(int id)
        {
            var item = await _moviesService.GetMovieByIdAsync(id);

            if(item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string userEmail = User.FindFirst(ClaimTypes.Email).Value;

            if (items.Count == 0) return View("NotFound");

            await _orderService.StoreOrderAsync(items, userId, userEmail);
            await _shoppingCart.ClearShoppingCartAsync();

            return View();
        }

        //to list all orders
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            //to get login userid we will use claimtypes
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string role = User.FindFirst(ClaimTypes.Role).Value;

            var list = await _orderService.GetOrdersByUserIdAndRoleAsync(userId, role);
            return View(list);
        }
    }
}

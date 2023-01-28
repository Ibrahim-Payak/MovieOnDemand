using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieOnDemand.ApplicationDbContext;
using MovieOnDemand.Data.Static;
using MovieOnDemand.Data.ViewModel;
using MovieOnDemand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieOnDemand.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _db;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        public IActionResult LogIn()
        {
            var res = new LogInVM();
            return View(res);
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInVM logInVM)
        {
            if (!ModelState.IsValid) return View(logInVM);

            var user = await _userManager.FindByEmailAsync(logInVM.EmailAddress);

            if(user != null)
            {
                var pwdCheck = await _userManager.CheckPasswordAsync(user, logInVM.Password);

                if (pwdCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, logInVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Movie");
                    }
                }
            }
            TempData["Error"] = "Wrong Credentials, please try again";
            return View(logInVM);
        }



        public IActionResult Register()
        {
            var res = new RegisterVM();
            return View(res);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);

            if(user == null)
            {
                var newUser = new ApplicationUser()
                {
                    FullName = registerVM.FullName,
                    UserName = registerVM.FullName,
                    Email = registerVM.EmailAddress
                };

                var newUserRes = await _userManager.CreateAsync(newUser, registerVM.Password);

                if (newUserRes.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                    return View("RegisterCompleted");
                }
                else
                {
                    TempData["Error"] = "Choose diffrent password";
                    return View(registerVM);
                }
            }

            TempData["Error"] = "This email address is already in use";
            return View(registerVM);
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movie");
        }

        public async Task<IActionResult> Users()
        {
            var res = await _db.Users.ToListAsync();
            return View(res);
        }

        //for AccessDenied page
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}

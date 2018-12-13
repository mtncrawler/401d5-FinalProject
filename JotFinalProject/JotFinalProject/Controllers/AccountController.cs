using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JotFinalProject.Data;
using JotFinalProject.Models;
using JotFinalProject.Models.AccountViewModel;
using JotFinalProject.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JotFinalProject.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private ApplicationDBContext _context;
        private JotDbContext _jotContext;
        private ICategory _category;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDBContext context, JotDbContext jotContext, ICategory category)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _jotContext = jotContext;
            _category = category;
        }

        /// <summary>
        /// Registration page
        /// </summary>
        /// <returns>Display/direct to registration</returns>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /// <returns>Completed profile</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel rvm)
        {
            if (ModelState.IsValid)
            {

                // start the registration process
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = rvm.Email,
                    Email = rvm.Email,
                    FirstName = rvm.FirstName,
                    LastName = rvm.LastName
                };

                var result = await _userManager.CreateAsync(user, rvm.Password);

                if (result.Succeeded)
                {
                    Claim nameClaim = new Claim("FirstName", $"{user.FirstName}");

                    await _userManager.AddClaimAsync(user, nameClaim);

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    // create default category upon registration for a user to add initial notes

                    Category defaultCategory = new Category()
                    {
                        Name = "Default",
                        UserID = user.Email
                    };
                    await _category.AddCategory(defaultCategory);
                                 
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }
            return View(rvm);
        }

        /// <summary>
        /// Displays Login Page
        /// </summary>
        /// <returns>Login view</returns>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Action that accepts user login
        /// </summary>
        /// <param name="lvm">User login credentials</param>
        /// <returns>Confirmed login or invalid username/password</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(lvm.Email, lvm.Password, false, false);

                if (result.Succeeded)
                {                 
                    return RedirectToAction("Index", "Note");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invlalid Username or Password.");
                }
            }
            return View(lvm);
        }

        /// <summary>
        /// Logout page
        /// </summary>
        /// <returns>Home page</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
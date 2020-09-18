using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.AspNetCore.Identity;
using Ecommerce.Models;
using Ecommerce.Data;
using Ecommerce.Models.ViewModel;

namespace Ecommerce.Controllers
{
    public class AccountController : Controller

    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<EcommerceUser> userManager;
        private readonly SignInManager<EcommerceUser> signInManager;
        public AccountController(ApplicationDbContext context, UserManager<EcommerceUser> userManager,
           SignInManager<EcommerceUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context;
        }
       
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new EcommerceUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    StreetAddress = model.StreetAddress,
                    City = model.City,
                    State = model.State,
                    Zipcode = model.Zipcode,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.Email,
                    Email = model.Email,
                    EmailConfirmed = true,
                    level=1,
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            var model = new LoginViewModel { ReturnUrl = returnUrl }; 
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            { 
                var result = await signInManager.PasswordSignInAsync(model.Username,
                   model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        
                        EcommerceUser user = await userManager.FindByNameAsync(model.Username);
                        if(user.level==0) return RedirectToAction("Index", "Products");
                        else  return RedirectToAction("Index", "Home");
                        
                    
                    }
                } 
            }
            ModelState.AddModelError("", "Invalid login attempt");
            
            return View(model);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit()
        {
            EcommerceUser user = await userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.Firstname = user.FirstName;
            ViewBag.Lastname = user.LastName;
            ViewBag.StreetAddress = user.StreetAddress;
            ViewBag.City = user.City;
            ViewBag.State = user.State;
            ViewBag.Zipcode = user.Zipcode;
            ViewBag.PhoneNumber = user.PhoneNumber;
            ViewBag.success = "";

            return View();
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateUserViewModel model) {
            EcommerceUser user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.StreetAddress = model.StreetAddress;
                user.City = model.City;
                user.State = model.State;
                user.Zipcode = model.Zipcode;
                user.PhoneNumber = model.PhoneNumber;

                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    ViewBag.Firstname = user.FirstName;
                    ViewBag.Lastname = user.LastName;
                    ViewBag.StreetAddress = user.StreetAddress;
                    ViewBag.City = user.City;
                    ViewBag.State = user.State;
                    ViewBag.Zipcode = user.Zipcode;
                    ViewBag.PhoneNumber = user.PhoneNumber;
                    ViewBag.success = "Update success";

                    return View();
                }
            }


            return View();

        }
    }

}
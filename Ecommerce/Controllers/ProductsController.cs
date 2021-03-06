﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Ecommerce.Models.ViewModel;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Ecommerce.Helpers;
using Microsoft.AspNetCore.Identity;
 


namespace Ecommerce.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    { 


        private readonly ApplicationDbContext _context;
        private readonly UserManager<EcommerceUser> userManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductsController(ApplicationDbContext context, UserManager<EcommerceUser> userManager, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
            this.userManager = userManager;



        }
       

        //phần dùng unittest
        public ActionResult DetailsView(int Id)
        {
            return View("Details");
        }

        public ActionResult DetailsAddProduct(int Id)
        {
            var product = new Product();
            product.productName = "Laptop";
            return View("Details", product);
        }

        public ActionResult DetailsRedirectIndex(int Id)
        {
            if (Id < 1)
                return RedirectToAction("Index");

            var product = new Product();
            product.productName = "Laptop";
            return View("Details", product);
        }



        // GET: Products
        [Route("admin")]
        public async Task<IActionResult> Index()
        {

            EcommerceUser user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user.level > 0)
                return RedirectToAction("Index", "Home");


            return View(await _context.Products.ToListAsync());
        }



        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,productName,price,productAmount,productInfo,active")] Product product)
        {

            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        private string Uploadfile(ProductViewModel model)
        {
            string filename = null;
            if (model.image != null)
            {
                string uploadfolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                filename = Guid.NewGuid().ToString() + "_" + model.image.FileName;
                string filepath = Path.Combine(uploadfolder, filename);
                using (var fileStream = new FileStream(filepath, FileMode.Create))
                {
                    model.image.CopyTo(fileStream);
                }

            }
            return filename;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create2(ProductViewModel model)
        {

            if (ModelState.IsValid)
            {
                string filename = Uploadfile(model);
                Product p = new Product
                {
                    productName = model.productName,
                    price = model.price,
                    productAmount = model.productAmount,
                    productInfo = model.productInfo,
                    active = model.active,
                    image = filename
                };
                _context.Add(p);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View();
        }



        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,productName,price,productAmount,productInfo,active")] Product product)
        {
            if (id != product.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ID == id);
        }
    }
}


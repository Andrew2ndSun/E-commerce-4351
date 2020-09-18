using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Helpers;


using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Controllers
{

    public class OrdersController : Controller
    {
        private readonly UserManager<EcommerceUser> userManager;
        private readonly SignInManager<EcommerceUser> signInManager;
        private readonly ApplicationDbContext _context;
     

        public OrdersController(ApplicationDbContext context, UserManager<EcommerceUser> userManager,
           SignInManager<EcommerceUser> signInManager)
        {
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

     

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            EcommerceUser user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user.level > 0)
                return RedirectToAction("Index", "Home");

            return View(await _context.Orders.ToListAsync());
        }

        public async Task<IActionResult> Success()
        {
            return View();
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.OrderInfos)
                    .ThenInclude(f => f.Product)
                .FirstOrDefaultAsync(m => m.OrderID == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public async Task<IActionResult> CreateAsync()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            if (cart != null)
                ViewBag.total = cart.Sum(item => item.Product.price * item.Quantity);
            else ViewBag.total = 0;
            //-------
            if (signInManager.IsSignedIn(User))
            {

                EcommerceUser user = await userManager.FindByNameAsync(User.Identity.Name);
                ViewBag.Name = user.FirstName + " " + user.LastName;

                //EcommerceUser user = await userManager.FindByNameAsync(User.Identity.StreetName);

            }
            else
            {
                ViewBag.Name = "";
            }
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,CustomerInfo,StreetAddress,City,State,Zipcode,PaymentMethod,DateAdded,Status,NameOnCard,CardNumber,ExpDay,SecCode")] Order order)
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            if (cart != null)
                ViewBag.total = cart.Sum(item => item.Product.price * item.Quantity);
            else ViewBag.total = 0;

            if (ModelState.IsValid)
            {
                order.OrderInfos = new List<OrderInfo>();
                List<Item> cartlist = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                for (int i = 0; i < cartlist.Count; i++)
                {
                    var orderinfoi = new OrderInfo { OrderID = order.OrderID, ProductID = cartlist[i].Product.ID, numOfProduct = cartlist[i].Quantity };
                    order.OrderInfos.Add(orderinfoi);

                }
                order.DateAdded = DateTime.Now;
                //----------
                _context.Add(order);
                await _context.SaveChangesAsync();
                //luu orderInfo
              
                    List<Item> cart1 = new List<Item>();

                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart1);

                    return RedirectToAction(nameof(Success));
                
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
             
            //-------
            if (signInManager.IsSignedIn(User))
            {

                EcommerceUser user = await userManager.FindByNameAsync(User.Identity.Name);
                ViewBag.Name = user.FirstName +" "+ user.LastName;

            }
            else
            {
                ViewBag.Name = "";
            }
            if (id == null)
            {
                return NotFound();
            }

            
            var order = await _context.Orders
               .Include(o => o.OrderInfos)
                   .ThenInclude(f => f.Product)
               .FirstOrDefaultAsync(m => m.OrderID == id);

            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,CustomerInfo,StreetAddress,City,State,Zipcode,PaymentMethod,DateAdded,Status")] Order order)
        {
           

            if (id != order.OrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderID))
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
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderID == id);
        }
    }
}

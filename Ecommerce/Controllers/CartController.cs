using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Models;
using Ecommerce.Helpers;
using Ecommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        { 
            _context = context;
        }

    

        [Route("cart")]
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            if (cart != null)
                ViewBag.total = cart.Sum(item => item.Product.price * item.Quantity);
            else
            {
                ViewBag.total = 0;
                ViewBag.Message = "The cart is currently empty!";
            }
            if(ViewBag.total == 0)
            {
                ViewBag.Message = "The cart is currently empty!";
            }    
            return View();
        }

        [Route("buy/{id}")]
        public async Task<IActionResult> BuyAsync(string id)
        {
            Product product = new Product();
             

            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item { Product = await _context.Products
                .FirstOrDefaultAsync(m => m.ID == int.Parse(id)), Quantity = 1 });

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item { Product = await _context.Products
                .FirstOrDefaultAsync(m => m.ID == int.Parse(id)), Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        [Route("remove/{id}")]
        public IActionResult Remove(string id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        private int isExist(string id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.ID.Equals(int.Parse(id)))
                {
                    return i;
                }
            }
            return -1;
        }

        public IActionResult IncreaseQty(string id)
        {
            //Get the cart info
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            //Get the index of the product based on the product id
            int index = isExist(id);
            //update the quantity
            cart[index].Quantity++;
           
            //Update the cart
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            //redirect to action Index to get the view
            return RedirectToAction("Index");
        }


        public IActionResult DecreaseQty(string id)
        {
            //Get the cart info
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            //Get the index of the product based on the product id
            int index = isExist(id);
            if (cart[index].Quantity > 1)
            {
                cart[index].Quantity--;
            }
            else
            {
                cart.RemoveAt(index);
            }
            //Update the cart
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            //redirect to action Index to get the view
            return RedirectToAction("Index");
        }

    }
}
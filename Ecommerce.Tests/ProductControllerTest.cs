using System;
using Xunit;

using Ecommerce.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Tests
{
    public class ProductControllerTest
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<EcommerceUser> _userManager;
        private readonly IWebHostEnvironment webHostEnvironment;


        private readonly ProductsController _productService;
        public ProductControllerTest()
        {

            _productService = new ProductsController(_context, _userManager, webHostEnvironment);

        }
        
        [Fact]
        public void TestDetailsView()
        {

            var result = _productService.DetailsView(2) as ViewResult;
            Assert.Equal("Details", result.ViewName);

        }
        
        [Fact]
        public void TestDetailsAddProduct()
        {
            var result = _productService.DetailsAddProduct(2) as ViewResult;
            var product = (Product)result.ViewData.Model;
            Assert.Equal("Laptop", product.productName);
        }
       
        [Fact]
        public void TestDetailsRedirectIndex()
        {

            RedirectToActionResult result = (RedirectToActionResult)_productService.DetailsRedirectIndex(-1);

            Assert.Equal("Index", result.ActionName);

        }
        [Fact]
        public void TestIndex()
        {

           var result = _productService.Index();

        }
        [Fact]
        public void TestDetail()
        {

            var result = _productService.Details(null);

        }
        [Fact]
        public void TestCreate()
        {

            var result = _productService.Create() as ViewResult;
            Assert.Equal(null, result.ViewName);

        }
    }
}

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
    public class AccountControllerTest
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<EcommerceUser> userManager;
        private readonly SignInManager<EcommerceUser> signInManager;

        private readonly AccountController _accountService;
        public AccountControllerTest()
        {

            _accountService = new AccountController(_context, userManager, signInManager);

        }

        [Fact]
        public void TestRegister()
        {

            var result = _accountService.Register();

        }

        [Fact]
        public void TestLogin()
        {

            var result = _accountService.Login();

        }

        [Fact]
        public void TestEdit()
        {

            var result = _accountService.Edit();

        }
    }
}

﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectReferencesDemo.Services.Data;
using ProjectReferencesDemo.Services.Models;
using ProjectReferencesDemo.Web.Models;
using System.Diagnostics;

namespace ProjectReferencesDemo.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;

        public HomeController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);

            var customers = context
                .Customers
                .Include(x => x.CustomerType)
                .Where(x => x.User == user)
                .ToList();

            return View(customers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new SaveCustomerViewModel()
            {
                Customer = new(),
                CustomerTypes = context.CustomerTypes.ToList()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveCustomerViewModel viewModel)
        {
            var user = await userManager.GetUserAsync(User);

            var customerType = context
                .CustomerTypes
                .Where(x => x.Id == viewModel.CustomerTypeId)
                .FirstOrDefault();

            if (customerType == null)
                return BadRequest();

            viewModel.Customer.DateOfRegistration = DateTime.Now;
            viewModel.Customer.User = user;
            viewModel.Customer.CustomerType = customerType;

            context.Customers.Add(viewModel.Customer);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            var customer = context
                .Customers
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (customer == null)
                return NotFound();

            var user = await userManager.GetUserAsync(User);
            if (customer.User != user)
                return Forbid();

            return View(customer);
        }

        [HttpPost]
        public IActionResult Remove(Customer customer)
        {
            context.Customers.Remove(customer);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var customer = context
                .Customers
                .Include(x => x.CustomerType)
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (customer == null)
                return NotFound();

            var user = await userManager.GetUserAsync(User);
            if (customer.User != user)
                return Forbid();

            return View(new SaveCustomerViewModel()
            {
                Customer = customer,
                CustomerTypes = context.CustomerTypes.ToList(),
                CustomerTypeId = customer.CustomerType.Id
            });
        }

        [HttpPost]
        public IActionResult Edit(SaveCustomerViewModel viewModel)
        {
            var customerinDb = context
                .Customers
                .Where(x => x.Id == viewModel.Customer.Id)
                .FirstOrDefault();

            if (customerinDb == null)
                return BadRequest();

            customerinDb.Name = viewModel.Customer.Name;
            customerinDb.Gender = viewModel.Customer.Gender;
            customerinDb.Age = viewModel.Customer.Age;

            var customerType = context
                .CustomerTypes
                .Where(x => x.Id == viewModel.CustomerTypeId)
                .SingleOrDefault();

            if (customerType != null)
                customerinDb.CustomerType = customerType;

            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
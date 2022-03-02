using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectReferencesDemo.Services.Data;
using ProjectReferencesDemo.Services.Models;
using ProjectReferencesDemo.Web.Models;
using System.Diagnostics;

namespace ProjectReferencesDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;

        public HomeController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var customers = context
                .Customers
                .Include(x => x.CustomerType)
                .ToList();

            return View(customers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateCustomerViewModel()
            {
                Customer = new(),
                CustomerTypes = context.CustomerTypes.ToList()
            });
        }

        [HttpPost]
        public IActionResult Create(CreateCustomerViewModel viewModel)
        {
            var customerType = context
                .CustomerTypes
                .Where(x => x.Id == viewModel.CustomerTypeId)
                .FirstOrDefault();

            if (customerType == null)
                return BadRequest();

            viewModel.Customer.DateOfRegistration = DateTime.Now;
            viewModel.Customer.CustomerType = customerType;

            context.Customers.Add(viewModel.Customer);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            var customer = context
                .Customers
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (customer == null)
                return NotFound();

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
        public IActionResult Edit(int id)
        {
            var customer = context
                .Customers
                .Include(x => x.CustomerType)
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (customer == null)
                return NotFound();

            return View(new CreateCustomerViewModel()
            {
                Customer = customer,
                CustomerTypes = context.CustomerTypes.ToList(),
                CustomerTypeId = customer.CustomerType.Id
            });
        }

        [HttpPost]
        public IActionResult Edit(CreateCustomerViewModel viewModel)
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
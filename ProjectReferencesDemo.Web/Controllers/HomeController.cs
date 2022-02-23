using Microsoft.AspNetCore.Mvc;
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
            var customers = context.Customers.ToList();

            return View(customers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Customer());
        }

        [HttpPost]
        public IActionResult Create(Customer newCustomer)
        {
            newCustomer.DateOfRegistration = DateTime.Now;
            context.Customers.Add(newCustomer);
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
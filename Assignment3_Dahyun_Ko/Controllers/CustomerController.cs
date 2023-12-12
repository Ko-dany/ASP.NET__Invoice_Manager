using Microsoft.AspNetCore.Mvc;
using Assignment3_Dahyun_Ko.Entities;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Assignment3_Dahyun_Ko.Controllers
{
    public class CustomerController : Controller
    {
        private CustomerInvoiceDBContext ctx { get; set; }

        public CustomerController(CustomerInvoiceDBContext ctx)
        {
            this.ctx = ctx;
        }

        /*********** Return the list of customers ***********/
        public IActionResult Customers()
        {
            var customers = ctx.Customers.Include(c => c.Invoices).ToList();
            return View(customers);
        }

        /*********** Add a new customer ***********/
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new Customer());
        }

        /*********** Edit an existing customer ***********/
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            Customer? customer = ctx.Customers.Find(id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (customer.CustomerId == 0) ctx.Customers.Add(customer);
                else ctx.Customers.Update(customer);

                ctx.SaveChanges();

                return RedirectToAction("Customers", new { id = customer.CustomerId });
            }
            else
            {
                ViewBag.Action = (customer.CustomerId == 0) ? "Add" : "Edit";
            }
            return View(customer);
        }


    }
}
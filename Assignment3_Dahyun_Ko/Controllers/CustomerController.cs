using Microsoft.AspNetCore.Mvc;
using Assignment3_Dahyun_Ko.Entities;
using Assignment3_Dahyun_Ko.Models;
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

        /*********** Return the list of invoices of the selected customer ***********/
        public IActionResult Invoices(int id)
        {
            var customer = ctx.Customers.Include(c => c.Invoices).ThenInclude(i=>i.InvoiceLineItems).Where(i => i.CustomerId == id).FirstOrDefault();
            Invoice newInvoice = new Invoice();
            newInvoice.InvoiceLineItems = new List<InvoiceLineItem>();

            if (customer != null)
            {
                CustomerInvoiceViewModel ciViewModel = new CustomerInvoiceViewModel()
                {
                    Customer = customer,
                    Invoice = newInvoice,
                    InvoiceLineItem = new InvoiceLineItem()
                };

                return View(ciViewModel);
            }
            else
            {
                return NotFound();
            }
        }

    

    }
}
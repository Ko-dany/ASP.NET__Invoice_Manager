using Microsoft.AspNetCore.Mvc;
using Assignment3_Dahyun_Ko.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Customers.Entities;
using Customers.Service;

namespace Assignment3_Dahyun_Ko.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerService customerService { get; }
        
        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        /*********** List of Customers ***********/
        public IActionResult Customers(string lowerBound="A", string upperBound="E")
        {
            var customers = customerService.GetCustomersFromTo(lowerBound, upperBound);
            CustomerActiveViewModel customerActiveViewModel = new CustomerActiveViewModel()
            {
                Customers = customers,
                Active = $"{lowerBound}-{upperBound}"
            };
            return View(customerActiveViewModel);
        }

        /*********** Add ***********/
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new Customer());
        }

        /*********** Edit ***********/
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            Customer? customer = customerService.GetCustomerById(id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                customerService.EditCustomer(customer);
                return RedirectToAction("Customers", new { id = customer.CustomerId });
            }
            else
            {
                ViewBag.Action = (customer.CustomerId == 0) ? "Add" : "Edit";
            }
            return View(customer);
        }

        /*********** Delete ***********/

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var customer = customerService.GetCustomerById(id);
            if(customer == null)
            {
                return NotFound();
            }

            customerService.UpdateDeletingStatus(customer);
            TempData["Deletion Message"] = $"The customer \"{customer.Name}\" is deleted.";
            TempData["CustomerId"] = customer.CustomerId;

            return RedirectToAction("Customers", "Customer");
        }

        /*********** Undelete ***********/

        [HttpGet]
        public IActionResult Undelete(int id)
        {
            var customer = customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            customerService.UpdateDeletingStatus(customer);

            return RedirectToAction("Customers", "Customer");
        }

        /*********** List of Invoices ***********/
        public IActionResult Invoices(int id)
        {
            var customer = customerService.GetInvoicesById(id);
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
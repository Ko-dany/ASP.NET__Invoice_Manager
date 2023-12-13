/*
 * Program: PROG2230-SEC4
 * Purpose: Assignment 5
 * Revision History:
 *      created by Dahyun Ko, Dec/13/2023
 */

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
        public IActionResult Invoices(int customerId, int invoiceId=1)
        {
            var customer = customerService.GetInvoicesById(customerId);
            var invoices = customer.Invoices;
            var paymentTerms = customerService.GetPaymentTerms();
            var selectedInvoice = customerService.GetSelectedInvoiceById(invoiceId);
            if (customer != null)
            {
                CustomerInvoiceViewModel ciViewModel = new CustomerInvoiceViewModel()
                {
                    Customer = customer,
                    Invoices = invoices,
                    SelectedInvoice = selectedInvoice,
                    PaymentTermsList = paymentTerms,
                    NewInvoice = new Invoice(),
                    NewInvoiceLineItem = new InvoiceLineItem()
                };
                return View(ciViewModel);
            }
            else
            {
                return NotFound();
            }
        }

        /*********** Add New Invoice ***********/
        public IActionResult AddInvoice(CustomerInvoiceViewModel ciViewModel, int customerId, int invoiceId=1)
        {
            if (ModelState.IsValid)
            {
                Invoice invoice = ciViewModel.NewInvoice;
                invoice.CustomerId = customerId;
                customerService.AddNewInvoice(invoice);
            }

            return RedirectToAction("Invoices", "Customer", new { customerId = customerId, invoiceId= invoiceId });
        }

        /*********** Add New Line Item ***********/
        public IActionResult AddLineItem(CustomerInvoiceViewModel ciViewModel, int customerId, int invoiceId=1)
        {
            if (ModelState.IsValid)
            {
                InvoiceLineItem invoiceLineItem = ciViewModel.NewInvoiceLineItem;
                invoiceLineItem.InvoiceId = invoiceId;
                customerService.AddNewLineItem(invoiceLineItem);
            }

            return RedirectToAction("Invoices", "Customer", new { customerId = customerId, invoiceId = invoiceId });
        }

    }
}
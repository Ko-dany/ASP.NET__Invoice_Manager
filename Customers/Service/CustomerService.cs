using Customers.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Service
{
    public class CustomerService : ICustomerService
    {
        private CustomerInvoiceDBContext customerInvoiceDBContext { get; set; }

        public CustomerService(CustomerInvoiceDBContext customerInvoiceDBContext)
        {
            this.customerInvoiceDBContext = customerInvoiceDBContext;
        }

        public List<Customer>? GetCustomersFromTo(string lowerBound = "A", string upperBound = "E")
        {
            return customerInvoiceDBContext.Customers.Where(c => c.Name.ToUpper().Substring(0, 1).CompareTo(lowerBound) >= 0 && c.Name.ToUpper().Substring(0, 1).CompareTo(upperBound) <= 0).OrderBy(c=>c.Name).ToList();
        }
        public Customer? GetCustomerById(int customerId)
        {
            return customerInvoiceDBContext.Customers.Find(customerId);
        }

        public Customer? EditCustomer(Customer customer)
        {
            if (customer.CustomerId == 0) customerInvoiceDBContext.Customers.Add(customer);
            else customerInvoiceDBContext.Customers.Update(customer);
            customerInvoiceDBContext.SaveChanges();

            return customer;
        }

        public void UpdateDeletingStatus(Customer customer)
        {
            customer.IsDeleted = !customer.IsDeleted;
            customerInvoiceDBContext.SaveChanges();
        }

        public Customer? GetInvoicesById(int customerId)
        {
            return customerInvoiceDBContext.Customers.Include(c => c.Invoices).ThenInclude(i => i.InvoiceLineItems).Where(i => i.CustomerId == customerId).FirstOrDefault();
        }

        public List<PaymentTerms> GetPaymentTerms()
        {
            return customerInvoiceDBContext.PaymentTermSet.OrderBy(p => p.PaymentTermsId).ToList();
        }

        public Invoice GetSelectedInvoiceById(int invoiceId)
        {
            return customerInvoiceDBContext.Invoices.Include(i => i.InvoiceLineItems).Where(i => i.InvoiceId == invoiceId).FirstOrDefault();
        }

        public void AddNewInvoice(Invoice newInvoice)
        {
            customerInvoiceDBContext.Invoices.Add(newInvoice);
            customerInvoiceDBContext.SaveChanges();
        }
        public void AddNewLineItem(InvoiceLineItem newInvoiceLineItem)
        {
            customerInvoiceDBContext.InvoiceLineItems.Add(newInvoiceLineItem);
            customerInvoiceDBContext.SaveChanges();
        }

    }
}

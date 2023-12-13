using Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Service
{
    public interface ICustomerService
    {
        public List<Customer>? GetCustomersFromTo(string lowerBound = "A", string upperBound = "E");
        public Customer? GetCustomerById(int customerId);
        public Customer? EditCustomer(Customer customer);
        public void UpdateDeletingStatus(Customer customer);
        public Customer? GetInvoicesById(int customerId);
        public List<PaymentTerms> GetPaymentTerms();
        public Invoice GetSelectedInvoiceById(int invoiceId);
        public void AddNewInvoice(Invoice newInvoice);
        public void AddNewLineItem(InvoiceLineItem newInvoiceLineItem);
    }
}

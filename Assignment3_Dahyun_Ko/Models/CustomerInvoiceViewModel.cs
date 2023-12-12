using Customers.Entities;

namespace Assignment3_Dahyun_Ko.Models
{
    public class CustomerInvoiceViewModel
    {
        public Customer? Customer { get; set; }
        public Invoice? Invoice { get; set; }
        public InvoiceLineItem? InvoiceLineItem { get; set; }
        public Invoice? ActiveInvoice { get; set; }
    }
}

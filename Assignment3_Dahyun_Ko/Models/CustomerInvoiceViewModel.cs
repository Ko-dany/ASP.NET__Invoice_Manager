using Customers.Entities;

namespace Assignment3_Dahyun_Ko.Models
{
    public class CustomerInvoiceViewModel
    {
        public Customer? Customer { get; set; }
        public ICollection<Invoice>? Invoices { get; set; }
        public Invoice? SelectedInvoice { get; set; }
        public List<PaymentTerms>? PaymentTermsList { get; set; }
        public InvoiceLineItem? NewInvoiceLineItem { get; set; }
        public Invoice? NewInvoice { get; set; }
    }
}

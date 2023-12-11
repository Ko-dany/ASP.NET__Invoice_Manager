namespace Assignment3_Dahyun_Ko.Entities
{
    public class Invoice
    {
        public int InvoiceId { get; set; }

        public DateTime? InvoiceDate { get; set; }

        public DateTime? InvoiceDueDate
        {
            get
            {
                return InvoiceDate?.AddDays(Convert.ToDouble(PaymentTerms?.DueDays));
            }
        }

        public double? PaymentTotal { get; set; } = 0.0;

        public DateTime? PaymentDate { get; set; }

        // FK:
        /* A property to navigate from an Invoice to its Payment Terms */
        public int PaymentTermsId { get; set; }

        public PaymentTerms? PaymentTerms { get; set; }

        // FK:
        /* A property to navigate from an Invoice to its Customer */
        public int CustomerId { get; set; }

        /* A property to navigate from an Invoice to all its Line Items */
        public ICollection<InvoiceLineItem>? InvoiceLineItems { get;  }
    }
}

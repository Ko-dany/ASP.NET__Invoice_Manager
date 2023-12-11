namespace Assignment3_Dahyun_Ko.Entities
{
    public class PaymentTerms
    {
        public int PaymentTermsId { get; set; }

        public string Description { get; set; } = null!;

        public int DueDays { get; set; }

        /* A property to navigate from a Payment Terms to all Invoices that have those payment terms */
        public ICollection<Invoice>? Invoices { get; }
    }
}

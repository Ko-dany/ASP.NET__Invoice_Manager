namespace Assignment3_Dahyun_Ko.Entities
{
    public class InvoiceLineItem
    {
        public int InvoiceLineItemId { get; set; }

        public double? Amount { get; set; }

        public string? Description { get; set; }

        // FK:
        /* A property to navigate from a Line Item to its Invoice */
        public int? InvoiceId { get; set; }
    }
}

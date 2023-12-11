namespace Assignment3_Dahyun_Ko.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string Name { get; set; } = null!;

        public string? Address1 { get; set; }

        public string? Address2 { get; set; }

        public string? City { get; set; } = null!;

        public string? ProvinceOrState { get; set; } = null!;

        public string? ZipOrPostalCode { get; set; } = null!;

        public string? Phone { get; set; }

        public string? ContactLastName { get; set; }

        public string? ContactFirstName { get; set; }

        public string? ContactEmail { get; set; }

        public bool IsDeleted { get; set; } = false;

        /* A property to navigate from a Customer to all its invoices */
        public ICollection<Invoice>? Invoices { get; }
    }
}

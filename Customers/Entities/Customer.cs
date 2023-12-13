using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Customers.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage ="Name is required.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Address1 is required.")]
        public string? Address1 { get; set; }

        public string? Address2 { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string? City { get; set; } = null!;

        [Required(ErrorMessage = "Province/State is required.")]
        [RegularExpression("^[A-Za-z]{2}$", ErrorMessage = "Province/State must be a 2 letter code.")]
        public string? ProvinceOrState { get; set; } = null!;

        [Required(ErrorMessage = "Zip/Postal code is required.")]
        [RegularExpression("^[A-Z][0-9][A-Z][ ]?[0-9][A-Z][0-9]$", ErrorMessage = "Zip/Postal code must be in the foramt \"A1A 1A1\"")]
        public string? ZipOrPostalCode { get; set; } = null!;

        [Required(ErrorMessage = "Phone is required.")]
        [RegularExpression("^[0-9]{3}-[0-9]{3}-[0-9]{4}$", ErrorMessage ="Phone number has to be in the format \"123-456-789\"")]
        public string? Phone { get; set; }

        public string? ContactLastName { get; set; }

        public string? ContactFirstName { get; set; }

        [EmailAddress(ErrorMessage ="Contact Email must be in a vaild email format")]
        public string? ContactEmail { get; set; }

        public bool IsDeleted { get; set; } = false;

        /* A property to navigate from a Customer to all its invoices */
        public ICollection<Invoice>? Invoices { get; }
    }
}

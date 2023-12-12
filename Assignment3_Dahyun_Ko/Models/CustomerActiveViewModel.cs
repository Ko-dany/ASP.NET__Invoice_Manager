using Customers.Entities;

namespace Assignment3_Dahyun_Ko.Models
{
    public class CustomerActiveViewModel
    {
        public List<Customer>? Customers { get; set; }
        public string Active { get; set; } = "A-E";
    }
}

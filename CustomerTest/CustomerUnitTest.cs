using Customers.Entities;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace CustomerTest
{
    public class CustomerUnitTest
    {
        [Fact]
        public void ReturnTrueIfPostalCodeIsValid()
        {
            //Arrange
            Customer customer = new Customer()
            {
                CustomerId = 9,
                Name = "Dany",
                Address1 = "123 Conestoga St",
                City = "Waterloo",
                ProvinceOrState = "ON",
                ZipOrPostalCode = "A1A 1A1",
                Phone = "416-123-4567",
            };

            //Act
            Regex postalCodeRegex = new Regex("^[A-Z][0-9][A-Z][ ]?[0-9][A-Z][0-9]$");
            bool postalCodeIsValid = false;

            if (postalCodeRegex.IsMatch(customer.ZipOrPostalCode)) { postalCodeIsValid = true; }

            //Assert
            Assert.True(postalCodeIsValid);
        }

        [Fact]
        public void ReturnTrueIfPhoneNumberIsValid()
        {
            //Arrange
            Customer customer = new Customer()
            {
                CustomerId = 3,
                Name = "John",
                Address1 = "123 Univercity Ave",
                City = "Waterloo",
                ProvinceOrState = "ON",
                ZipOrPostalCode = "N2J 2A4",
                Phone = "416-123-4567",
            };

            //Act
            Regex phoneRegex = new Regex("^[0-9]{3}-[0-9]{3}-[0-9]{4}$");
            bool phoneIsValid = false;

            if (phoneRegex.IsMatch(customer.Phone)) { phoneIsValid = true; }

            //Assert
            Assert.True(phoneIsValid);
        }
    }
}
using Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CustomerTest
{
    public class InvoiceUnitTest
    {
        [Fact]
        public void CheckIfTheTotalIsCorrect()
        {
            //Arrange
            double? result = 500;

            Invoice invoice = new Invoice()
            {
                InvoiceId = 1,
                InvoiceDate = new DateTime(2023, 12, 13),
                PaymentTermsId = 3,
                CustomerId = 2,
                InvoiceLineItems = new List<InvoiceLineItem>()
            };

            invoice.InvoiceLineItems.Add(new InvoiceLineItem() { Description = "Des1", Amount = 200 });
            invoice.InvoiceLineItems.Add(new InvoiceLineItem() { Description = "Des2", Amount = 300 });

            //Act
            double? total = invoice.InvoiceLineItems.Sum(i => i.Amount);

            //Assert
            Assert.Equal(total, result);
        }
    }
}

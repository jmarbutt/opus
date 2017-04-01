using System;
using System.Collections.ObjectModel;
using Opus.Samples.Models;

namespace Opus.Samples.Silverlight.Models
{
    public class PersonModel
    {
        public PersonModel()
        {
            FirstName = "John";
            LastName = "Doe";
            Phone1 = "123-123-1234";
            Phone2 = "234-456-7890";
            Address1 = "123 Main St.";
            Address2 = "Suite 101";
            City = "Birmingham";
            State = "AL";
            Zip = "35004";
            TotalSales = 23423.43;

            Orders = new ObservableCollection<OrderModel>();

            for (var i = 0; i < 10; i++)
                Orders.Add(new OrderModel
                               {
                                   Description = "Item " + i,
                                   OrderDate = DateTime.Now.AddDays(i),
                                   Qty = i,
                                   Amount = 1253.34*i
                               });
        }


        public string FirstName { get; set; }


        public string LastName { get; set; }


        public string EmailAddress { get; set; }


        public string Phone1 { get; set; }


        public string Phone2 { get; set; }


        public string Address1 { get; set; }


        public string Address2 { get; set; }


        public string City { get; set; }


        public string State { get; set; }


        public string Zip { get; set; }


        public ObservableCollection<OrderModel> Orders { get; set; }


        public double? TotalSales { get; set; }


        public string MyCustomControlProperty { get; set; }
    }
}
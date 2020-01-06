using System;
using System.Collections.Generic;
using System.Text;

namespace Munchy.Models
{
    public class SimpleProduct
    {

        public string Name { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public SimpleProduct(string name, double price, string description, int quantity)
        {
            this.Name = name;
            this.Price = price;
            this.Description = description;
            this.Quantity = quantity;
        }

        public SimpleProduct()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Munchy.Models
{
    public class Order
    {
        public string OrderId { get; set; }

        public List<SimpleProduct> OrderedProducts { get; set; }

        public string OrderedTime { get; set; }
        public string PaymentId { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
        public string InstallId { get; set; }

        public Order(Basket basket, Payment payment)
        {
            OrderedProducts = new List<SimpleProduct>();
            this.PaymentId = payment.id.ToString();
            OrderedTime = DateTime.Now.ToShortDateString();
            this.Status = payment.state;
            this.OrderId = payment.order_id;
            this.Price = basket.TotalPrice;
            this.InstallId = Helpers.DeviceHelper.GetMunchyId();

            foreach (var item in basket.Items)
            {
                OrderedProducts.Add(new SimpleProduct(item.Product.Name, item.Product.Price, item.Product.Description, item.Quantity));
            }
        }

        public Order()
        {

        }
    }
}

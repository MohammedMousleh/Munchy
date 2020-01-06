using Munchy.Models;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Munchy.Services
{
    public class OrderSerivice : IOrderService<Order>
    {
        public async Task<Order> CreateOrder(Models.Basket basket, Payment payment)
        {
                Order order = new Order(basket, payment);
                ICollectionReference collection = CrossCloudFirestore.
                     Current.Instance.
                     GetCollection("Orders");

                await collection.AddDocumentAsync(order);

            return await Task.FromResult(order);
        
        }
    }
}

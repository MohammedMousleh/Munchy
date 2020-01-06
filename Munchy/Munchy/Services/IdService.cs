using Plugin.CloudFirestore;
using Plugin.CloudFirestore.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Munchy.Services
{
    public class IdService : IIdService
    {
        IFirestore fireStoreInstance = CrossCloudFirestore.Current.Instance;

        public IdService()
        {

        }
        public async Task<Int64> CreateOrderIdAsync()
        {

            IDocumentSnapshot document = await fireStoreInstance.GetDocument("/OrderIds/unCmzg3mr1W9U9pAQQSk").GetDocumentAsync();

            OrderId orderId = document.ToObject<OrderId>();
            Int64 currentOrderId = orderId.Id; 
            orderId.Id = currentOrderId +1; 
            await fireStoreInstance.GetDocument("/OrderIds/unCmzg3mr1W9U9pAQQSk").UpdateDataAsync(orderId);

            return  await Task.FromResult(orderId.Id);
        }
    }

    class OrderId
    {

        public Int64 Id { get; set; }

        public OrderId()
        {

        }
    }
}

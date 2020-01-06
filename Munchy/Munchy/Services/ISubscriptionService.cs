using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Munchy.Services
{
    public interface ISubscriptionService<T>
    {
        Task<T> CreateSubscription(Int64 orderId);
        Task<T> AuthorizeSubscription(string subId, double amount, string token);
        Task<T> GetSubscription(string subId);
    
    }
}

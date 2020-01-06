using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Munchy.Services
{
    public interface IPaymentService<T>
    {
        Task<T> GetPayment(string paymentId);
        Task<T> CreatePayment();
        Task<T> AuthorizePayment(string paymentId);

        Task<T> CreateRecurringPayment(string id, bool captureNow, Int64 order_id); 

    }
}

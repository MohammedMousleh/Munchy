using Munchy.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Munchy.Services
{
    public interface IOrderService<T>
    {
        Task<T> CreateOrder(Munchy.Models.Basket basket, Payment  payment);
    }
}

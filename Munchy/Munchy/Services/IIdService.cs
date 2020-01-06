using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Munchy.Services
{
    public interface IIdService
    {

        Task<Int64> CreateOrderIdAsync();
        
    }
}

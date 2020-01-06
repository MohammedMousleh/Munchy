using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Munchy.Services
{
   public interface IProductsService<T>
    {
        Task<T> GetProductAsync(string id);
        Task<IEnumerable<T>> GetProductByThresHoldAsync(int thresHold);
        Task<IEnumerable<T>> GetProductsAsync(bool forceRefresh = false);
        Task<IEnumerable<T>> GetProductByCategory(string categoryName);
        Task<IEnumerable<T>> SerchForProductByName(string name);

    }
}

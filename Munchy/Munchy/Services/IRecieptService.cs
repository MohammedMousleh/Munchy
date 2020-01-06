using Munchy.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Munchy.Services
{
    public interface IRecieptService<T>
    {
        Task<T> AddRecieptAsync(Order order, string userId);
        Task<bool> UpdateRecieptAsync(T item);
        Task<bool> DeleteRecieptAsync(string id);
        Task<T> GetRecieptAsync(string id);
        Task<IEnumerable<T>> GetScannedReciepts(string id);
        Task<IEnumerable<T>> GetUnScannedReciepts(string id);

        Task<IEnumerable<T>> GetRecieptsAsync(string installId, bool forceRefresh = false);
    }
}

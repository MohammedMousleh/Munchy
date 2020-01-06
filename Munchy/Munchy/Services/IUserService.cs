using Munchy.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Munchy.Services
{
    public interface IUserService<T>
    {
        Task<bool> AddUserAsync(T item);
        Task<bool> UpdateUserAsync(T item);
        Task<bool> DeleteUserAsync(string id);
        Task<bool> CreateUserPasswordAsync(string id, string password); 
        Task<T> GetUserAsync(string installId);
        Task<bool> CreateUserCreditCard(string userId, SimpleCreditCard creditCard);
     
        Task<bool> CreateUserSubscription(string id, string subscriptionId);
        Task<IEnumerable<T>> GetUsersAsync(bool forceRefresh = false);
    }
}

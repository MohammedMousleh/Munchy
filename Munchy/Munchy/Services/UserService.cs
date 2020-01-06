using Munchy.Models;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Munchy.Services
{
    public class UserService : IUserService<User>
    {

        ICollectionReference userCollectionRef;


        public UserService()
        {
            userCollectionRef = CrossCloudFirestore.Current.Instance.GetCollection("User"); 
        }


        public async Task<bool> AddUserAsync(User user)
        {
            await this.userCollectionRef.AddDocumentAsync(user);
            return await Task.FromResult(true);
        }

        public Task<bool> DeleteUserAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserAsync(string munchyId)
        {
            var users = await userCollectionRef.GetDocumentsAsync();

            User user = users.ToObjects<User>().Where(u =>  u.InstallId == munchyId).FirstOrDefault(); ;

            if (user != null)
            {
                return await Task.FromResult(user);
            }

            return await Task.FromResult(user);
        }

        public async Task<IEnumerable<User>> GetUsersAsync(bool forceRefresh = false)
        {
            var users = await userCollectionRef.GetDocumentsAsync();
            return await Task.FromResult(users.ToObjects<User>());
        }

        public Task<bool> UpdateUserAsync(User item)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateUserPasswordAsync(string userId, string password)
        {
          
                var document = CrossCloudFirestore.Current.Instance
                    .GetCollection("User")
                    .GetDocument(userId);

                if (document != null)
                {
                    using (MD5 md5Hash = MD5.Create())
                    {
                        string hash =  GetMd5Hash(md5Hash, password);

                        await document.UpdateDataAsync(new { password = hash });
                    }

                }
            return await Task.FromResult(true);
        }


        public async Task<bool> CreateUserSubscription(string userId, string subscriptionId)
        {
                var document = CrossCloudFirestore.Current.Instance
                    .GetCollection("User")
                    .GetDocument(userId);

                if (document != null)
                {
                    await document.UpdateDataAsync(new { SubId = subscriptionId });
                }

            return await Task.FromResult(true);
        }

        public async Task<bool> CreateUserCreditCard(string userId, SimpleCreditCard creditCard)
        {
                var document = CrossCloudFirestore.Current.Instance
                    .GetCollection("User")
                    .GetDocument(userId);

                if (document != null)
                {
                    await document.UpdateDataAsync(new { CreditCard = creditCard });
                }
            return await Task.FromResult(true);
        }

        private string GetMd5Hash(MD5 md5Hash, string input)
        {

            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();


            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}

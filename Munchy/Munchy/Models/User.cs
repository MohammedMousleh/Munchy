using Plugin.CloudFirestore.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Munchy.Models
{
    public class User
    {
        [Id]
        public string Id { get; set; }
        public string InstallId { get; set; }
        public string SubId { get; set; }
        public string password { get; set; }
        public SimpleCreditCard CreditCard { get; set; }


        public User()
        {

        }

        public User CreateUser(string munchyId)
        {
            return new User
            {
                InstallId = munchyId,
                CreditCard = null,
                SubId = String.Empty,
                password = String.Empty
              
            };
             }
    }
}

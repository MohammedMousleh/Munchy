using Munchy.Models;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munchy.Services
{
   public  class TodaysMenuService
    {
 

        public TodaysMenuService()
        {

            

        }
        public async Task<bool> S()
        {
           await CrossCloudFirestore.Current.Instance.GetCollection("TodaysMenu").AddDocumentAsync(new Product
            {
                Allegener = new List<string> { "Mælk" }, 
                 AmountOffer = null, 
                  Categories = null, 
                   Description = "Dagens menu ", 
                    HasMeat = true, 
                     Id = Guid.NewGuid().ToString(), 
                      InStock = true, 
                       IsOnSale =false, 
                        IsHalal = true, 
                         IsWeightProduct = false, 
                          MeatType ="Kylling", 
                           Name = "Tartelleter", 
                            Offer = null, 
                             Price = 5, 
                              ProductImage = { ImageName="bagel.jpg", ImageSource= "https://firebasestorage.googleapis.com/v0/b/canteenapp-15034.appspot.com/o/crossaint1.jpg?alt=media&token=b7c085b0-c0d9-4b4f-90e2-29d214724702" },
                               Size = null
            });
             return await Task.FromResult(true);
        }


        public async Task<Product> GetTodaysMenu()
        {
            var doc = await CrossCloudFirestore.Current.Instance.GetCollection("TodaysMenu").GetDocumentsAsync();

            return await Task.FromResult(doc.ToObjects<Product>().FirstOrDefault());
        }
    }
}

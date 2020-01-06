using Munchy.Models;
using Plugin.CloudFirestore;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Munchy.Services
{
    public class ProductService : IProductsService<Product>
    {
        private ICollectionReference ProductCollectionref;


        public ProductService()
        {
            this.ProductCollectionref = CrossCloudFirestore.Current.Instance.GetCollection("Products");
        }
        public Task<Product> GetProductAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetProductByThresHoldAsync(int pageIndex)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var productCollection = await CrossCloudFirestore.Current
                                     .Instance
                                     .GetCollection("Products")
                                     .OrderBy("Name")
                                     .GetDocumentsAsync();
                                     
                return await Task.FromResult(new ObservableCollection<Product>(productCollection.ToObjects<Product>()));
            }

            else
            {
                await Application.Current.MainPage.DisplayAlert("Ingen internet forbindelse", "", "OK");
                return new ObservableCollection<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(bool forceRefresh = false)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var productCollection =  await CrossCloudFirestore.Current
                                     .Instance
                                     .GetCollection("Products")
                                     .OrderBy("Name")
                                     .GetDocumentsAsync();
                return await Task.FromResult(new ObservableCollection<Product>(productCollection.ToObjects<Product>()));
            }
            
            else
            {
                await Application.Current.MainPage.DisplayAlert("Ingen internet forbindelse","", "OK");
                return new ObservableCollection<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName) 
        {

            if (CrossConnectivity.Current.IsConnected)
            {
                var productCollection = await CrossCloudFirestore.Current
                                     .Instance
                                     .GetCollection("Products")
                                     .WhereArrayContains("Categories", categoryName)
                                     .GetDocumentsAsync();
                return await Task.FromResult(new ObservableCollection<Product>(productCollection.ToObjects<Product>()));
            }

            else
            {
                await Application.Current.MainPage.DisplayAlert("Ingen internet forbindelse", "", "OK");
                return new ObservableCollection<Product>();
            }
        }

        public async Task<IEnumerable<Product>> SerchForProductByName(string name)
        {
            var productCollection = await CrossCloudFirestore.Current
                                     .Instance
                                     .GetCollection("Products")
                                     .WhereEqualsTo("Name", name)
                                     .GetDocumentsAsync();

            return await Task.FromResult(new List<Product>(productCollection.ToObjects<Product>()));

        }
    }
}

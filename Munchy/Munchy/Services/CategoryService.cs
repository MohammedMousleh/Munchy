using Munchy.Models;
using Plugin.CloudFirestore;
using Plugin.CloudFirestore.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Munchy.Services
{
    public class CategoryService
    {

        private ICollectionReference CategoryCollectionRef;

        public CategoryService()
        {
            this.CategoryCollectionRef = CrossCloudFirestore.Current.Instance.GetCollection("Categories");

        }

        public async Task<List<string>> GetCategories()
        {
                var categoryCollection = await CategoryCollectionRef.GetDocumentsAsync();

                return await Task.FromResult(new ObservableCollection<Categories>(categoryCollection.ToObjects<Categories>()).FirstOrDefault().CategoriesList);
        }
    }

    public class Categories
    {

        [MapTo("Categories")]
        public List<string> CategoriesList { get; set; }
    }
}

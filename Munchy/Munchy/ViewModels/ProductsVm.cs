using Munchy.Models;
using Munchy.Services;
using Munchy.Views;
using MvvmHelpers;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Munchy.ViewModels
{
    public class ProductsVm : BaseViewModel
    {
        public Munchy.Models.Basket Basket
        {
            get { return BasketService.CurrentBasket; }
            set { SetProperty(ref BasketService.CurrentBasket, value); }
        }

        public string SearchedText { get; set; }

        CategoryService categoryService = new CategoryService();

        public ObservableRangeCollection<Product> AllProducts;
        public ObservableRangeCollection<CategorizedProducts> Products { get; set; }

        public ObservableCollection<Product> OfferProducts { get; set; }

        public ICommand LoadProductsCommand { get; set; }
        public ICommand AddToBasketCommand { get; set; }
        public ICommand LoadProductCategoryCommand { get; set; }

        public ProductsVm()
        {
            Icon = "foodIcon.png";
            SearchedText = "";
            Basket = BasketService.CurrentBasket;
            Products = new ObservableRangeCollection<CategorizedProducts>();
            OfferProducts = new ObservableCollection<Product>();
            AllProducts = new ObservableRangeCollection<Product>();
            LoadProductsCommand = new Command(async () => await ExecutreLoadProductsCommand());
            this.AddToBasketCommand = new Command(AddProductToBasket);
            this.LoadProductCategoryCommand = new Command(async (cp) => await LoadProductCategory(cp));
            this.SearchForProductCommand = new Command(async () => await SearchForProduct());
            ListenForRealtimeChanges();
        }


        async Task ExecutreLoadProductsCommand()
        {

            if (IsBusy)
                return;

            IsBusy = true;
            try
            {

                await GetAllProducts();
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Exception", e.ToString(), "OK");
            }
            finally
            {
                IsBusy = false;
            }



        }


        public void AddProductToBasket(object product)
        {
            Product productToBuy = (Product)product;
            Basket.AddItem(new ltem(productToBuy, 1));
            Basket.CalculatePrice();
        }


        public Command SearchForProductCommand { get; set; }


        public async Task SearchForProduct()
        {


            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var result = await ProductsService.SerchForProductByName(SearchedText);

                AllProducts.Clear();
                AllProducts.AddRange(result);
                await Application.Current.MainPage.Navigation.PushAsync(new ShowAllProductsPage(AllProducts));
            }
            catch(Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Fejl", e.ToString(), "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public ObservableCollection<Product> GetCategorizedProducts(string category, IEnumerable<Product> products)
        {

            return new ObservableCollection<Product>(products.Where(p => p.Categories.Any(c => c == category)));
        }


        public ObservableCollection<Product> GetOfferProducts(IEnumerable<Product> products)
        {
            ObservableCollection<Product> offerProducts = new ObservableCollection<Product>();

            return new ObservableCollection<Product>(products.Where(p => p.Offer != null));
        }


        public async Task GetAllProducts()
        {

            List<string> categories = await categoryService.GetCategories();

            var products = await ProductsService.GetProductsAsync();

            Products.Clear();
           
            var categorizedProducts = new List<CategorizedProducts>();
           
            GetOfferProducts(products).ToList().ForEach(p => OfferProducts.Add(p));

            categories.ForEach(cat => categorizedProducts.Add(new CategorizedProducts(GetCategorizedProducts(cat, products), cat)));

            Products.AddRange(categorizedProducts);
            ObservableRangeCollection<Product> offerProducts = new ObservableRangeCollection<Product>(products.Where(p => p.Offer != null));
            Products.Insert(0, new CategorizedProducts(offerProducts, "Tilbud"));

        }

        public void ListenForRealtimeChanges()
        {

            CrossCloudFirestore.Current
            .Instance
            .GetCollection("Products")
            .AddSnapshotListener((snapshot, error) =>
            {
                if (snapshot != null)
                {
                    foreach (var documentChange in snapshot.DocumentChanges)
                    {

                        switch (documentChange.Type)
                        {

                            case DocumentChangeType.Added:

                                if (this.Products != null && documentChange != null && this.Products.Count() != 0)
                                {
                                   
                                   AddToCorrespondingCategory(documentChange.Document.ToObject<Product>());
                                }

                                break;

                            case DocumentChangeType.Modified:

                                if (this.Products != null && documentChange != null)
                                {
                                    UpdateProdcuct(documentChange.Document.ToObject<Product>());
                                }
                                break;
                            case DocumentChangeType.Removed:
                              /*
                                var newList = new ObservableCollection<Product>(this.Products.Where(p => p.Id != documentChange.Document.ToObject<Product>().Id));
                                this.Products = newList;
                                */
                                break;
                        }
                    }
                }
            });
        }


        private void UpdateProdcuct(Product product)
        {
            Product retrivedProduct = FindProduct(product);
          
            if(retrivedProduct != null)
            {
                CheckOffer(retrivedProduct, product);
                retrivedProduct.Name = product.Name;
                retrivedProduct.Allegener = product.Allegener;
                retrivedProduct.AmountOffer = product.AmountOffer;
                retrivedProduct.Description = product.Description;
                retrivedProduct.HasMeat = product.HasMeat;
                retrivedProduct.InStock = product.InStock;
            
                retrivedProduct.IsOnSale = product.IsOnSale;
                retrivedProduct.MeatType = product.MeatType;
                retrivedProduct.Offer = product.Offer;
                retrivedProduct.ProductImage = product.ProductImage;
                retrivedProduct.Size = product.Size;
                retrivedProduct.Price = product.Price;
                retrivedProduct.Categories = product.Categories;
                retrivedProduct.IsWeightProduct = product.IsWeightProduct;
               
            }
        }


        private Product FindProduct(Product product)
        {
            foreach (var list in this.Products)
            {
               var retirievedProduct = list.Products.Where(p => p.Id == product.Id).FirstOrDefault();

                if(retirievedProduct != null)
                {
                    return retirievedProduct;
                }
            }
            return null;
        }


        private void CheckOffer(Product oldProduct, Product newProduct)
        {
            if(oldProduct.Offer != null && newProduct.Offer == null)
            {
                this.Products[0].Products.Remove(oldProduct);
            } else if(oldProduct == null && newProduct != null)
            {
                this.Products[0].Products.Add(newProduct);
            }
        }
        private void AddToCorrespondingCategory(Product product)
        {

            foreach (var cat in product.Categories)
            {
                var lists = this.Products.Where(cp => cp.Name == cat);

                if(lists != null)
                {
                    foreach (var list in lists)
                    {
                        list.Products.Add(product);
                    }
                } 
            }
           
          }

        private async Task LoadProductCategory(object  value)
        {
            CategorizedProducts cp = value as CategorizedProducts;

            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var result = await ProductsService.GetProductByCategory(cp.Name);
                AllProducts.Clear();
                result.ToList().ForEach(p => AllProducts.Add(p));
                await Application.Current.MainPage.Navigation.PushAsync(new ShowAllProductsPage(AllProducts));

            }
            
            catch(Exception e)
            {
                Debug.WriteLine(e);
            }

            finally
            {
                IsBusy = false;
            }
        }
    }

    public class CategorizedProducts
    {
        public string Name { get; set; }
        public ObservableCollection<Product> Products { get; set; }

        public CategorizedProducts(ObservableCollection<Product> products, string name)
        {
            this.Name = name;
            this.Products = products;
        }
    }

   
}

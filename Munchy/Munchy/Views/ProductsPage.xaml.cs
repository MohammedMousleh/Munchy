using Munchy.Models;
using Munchy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Munchy.Views
{
    [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class ProductsPage : ContentPage
    {

         ProductsVm viewModel;
        public ProductsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ProductsVm();
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Products.Count == 0)
                viewModel.LoadProductsCommand.Execute(null);
        }

        /*
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var product = e.SelectedItem as Product;
            if (product == null)
                return;

            //ProductListView.SelectedItem = null;
        }
        */
    }
}
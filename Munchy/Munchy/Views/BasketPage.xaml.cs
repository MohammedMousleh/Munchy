using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Munchy.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasketPage : ContentPage
    {

        BasketVM viewModel;
        public BasketPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new BasketVM();
            
        }

        
      private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
      {
       
         

          listView.SelectedItem = null;
      }
      
    }
}
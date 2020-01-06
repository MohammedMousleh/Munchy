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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecieptsPage : ContentPage
    {
        RecieptVM viewModel;
        public RecieptsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new RecieptVM();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var reciept = args.SelectedItem as Reciept;
            if (reciept == null)
                return;

            //await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            RecieptListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }
    }
}
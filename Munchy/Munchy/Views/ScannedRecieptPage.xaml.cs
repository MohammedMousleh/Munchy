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
    public partial class ScannedRecieptPage : ContentPage
    {
        RecieptVM viewModel;

        public ScannedRecieptPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new RecieptVM();
            
            MessagingCenter.Subscribe<ScanQRPage, Reciept>(this, "RecieptScanned", async (obj, reciept) =>
              {
                  this.viewModel.ScannedReciepts.Add(reciept);
              });
              
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var reciept = args.SelectedItem as Reciept;
            if (reciept == null)
                return;

            await Navigation.PushAsync(new ShowRecieptPage(reciept));

            RecieptListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.ScannedReciepts.Count == 0)
            {
                viewModel.LoadScannedRecieptsCommand.Execute(null);
            }
           
        }
    }
}
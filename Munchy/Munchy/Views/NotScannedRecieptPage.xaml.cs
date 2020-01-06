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
    public partial class NotScannedRecieptPage : ContentPage
    {

        RecieptVM viewModel;
        public NotScannedRecieptPage() 
        {
            InitializeComponent();
            BindingContext = viewModel = new RecieptVM();
           
            MessagingCenter.Subscribe<ScanQRPage, Reciept>(this, "RecieptScanned", async (obj, reciept) =>
            {
                this.viewModel.NotScannedReciepts.Remove(reciept);
            });
            

        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var reciept = args.SelectedItem as Reciept;
            if (reciept == null)
                return;
            
            await this.Navigation.PushAsync(new ScanQRPage(reciept));
            RecieptListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.NotScannedReciepts.Count == 0)
            {
                viewModel.LoadNotScannedRecieptsCommand.Execute(null);
            }

        }
    }
}
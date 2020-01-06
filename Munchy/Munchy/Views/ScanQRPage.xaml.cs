using Munchy.Models;
using Munchy.Services;
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
    public partial class ScanQRPage : ContentPage
    {

        ScanQRVM viewModel;
        Reciept reciept; 
        public ScanQRPage(Reciept reciept)
        {
            InitializeComponent();
            this.reciept = reciept;
            BindingContext = viewModel = new ScanQRVM();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#F5F6F1");

        }

        private async Task<bool> ShowReciept()
        {
            bool result = await DependencyService.Get<RecieptService>().UpdateRecieptAsync(reciept);

            if (result)
            {
                this.reciept.IsScanned = true;
                this.reciept.Text = "Tryk her og se din kvittering";
                await Application.Current.MainPage.Navigation.PushAsync(new ShowRecieptPage(reciept));
                Application.Current.MainPage.Navigation.RemovePage(this);
                return await Task.FromResult(reciept.IsScanned);
            }

            return await Task.FromResult(reciept.IsScanned);
        }

        private  void scanner_OnScanResult(ZXing.Result result)
        {

            Device.BeginInvokeOnMainThread(async () =>
           {
               string id = result.Text;

               if (id == "123")
               {
                   await ShowReciept();
                   MessagingCenter.Send(this,"RecieptScanned",this.reciept);
                   scanner.IsVisible = false;
                   return;
               }
           });
        }
    }
}
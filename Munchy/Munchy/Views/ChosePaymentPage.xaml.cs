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
    public partial class ChosePaymentPage : ContentPage
    {

        ChosePaymentVm viewModel; 
        public ChosePaymentPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ChosePaymentVm();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Send(this, "ChosePaymentAppear");

        }
    }
}
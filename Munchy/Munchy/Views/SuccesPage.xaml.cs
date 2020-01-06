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
    public partial class SuccesPage : ContentPage
    {
        private readonly Page page;
        public SuccesPage(string message, Page page)
        {
            InitializeComponent();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#F5F6F1");
            this.page = page;
            this.messageLbl.Text = message.ToUpper();
            NextPage();

        }

        private async void NextPage()
        {
            await Task.Delay(2000);

            await this.Navigation.PushAsync(this.page);
            this.Navigation.RemovePage(this);
        }

        protected override bool OnBackButtonPressed()
        {
            return true;

        }
    }
}
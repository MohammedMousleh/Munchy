using Munchy.Models;
using Munchy.Services;
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
    public partial class CreatePaymentAuthenticationPage : ContentPage
    {
        private string password = String.Empty;
        private User user;
        private string subscriptionId; 

        public CreatePaymentAuthenticationPage(string subId)
        {
            InitializeComponent();
            this.subscriptionId = subId;
        }

        private async void passowrdEntry_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (e.NewTextValue.Length == 4 && this.password == String.Empty)
            {
                this.titleLbl.Text = "Gentag adgangskoden";
                this.password = e.NewTextValue;
                this.passowrdEntry.Text = String.Empty;

            }
            else if (e.NewTextValue.Length == 4 && this.password.Length == 4)
            {
                if (ComparePasswords(e.NewTextValue, this.password))
                {
                   await DependencyService.Get<UserService>().CreateUserPasswordAsync(user.Id, e.NewTextValue);
                   await DependencyService.Get<UserService>().CreateUserSubscription(user.Id,this.subscriptionId);
                   await this.Navigation.PushAsync(new SuccesPage("Perfekt! Fremover kan du bruge adgangskoden ved betaling",this.Navigation.NavigationStack.FirstOrDefault()));
                }
                else
                {
                    this.passowrdEntry.Text = String.Empty;
                    this.password = String.Empty;
                    this.titleLbl.Text = "Adgangskoderne mathcer ikke.. Prøv igen";
                }
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            this.user = await DependencyService.Get<UserService>().GetUserAsync(Helpers.DeviceHelper.GetMunchyId());
            

        }

        private async void fingerprintBtn_Clicked(object sender, EventArgs e)
        {
            await DependencyService.Get<UserService>().CreateUserSubscription(user.Id, this.subscriptionId);
            await this.Navigation.PushAsync(new SuccesPage("Perfekt! Betal nu med fingeraftryk", this.Navigation.NavigationStack[1]));
            //this.Navigation.RemovePage(this.Navigation.NavigationStack[2]);

            Console.WriteLine("muddi");
            this.Navigation.NavigationStack.ToList().ForEach(Console.WriteLine);
        }

        private void passwordBtn_Clicked(object sender, EventArgs e)
        {
            this.fingerprintBtn.IsVisible = false;
            this.passwordBtn.IsVisible = false;
            this.titleLbl.Text = "Vælg din 4 cifret adgangskode";
            this.passowrdEntry.IsVisible = true;
        }

        public bool ComparePasswords(string password1, string password2)
        {

            if (password1 == password2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
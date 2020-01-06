using Microsoft.AppCenter;
using Munchy.Models;
using Munchy.Services;
using Nito.AsyncEx;
using Plugin.CloudFirestore;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Munchy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashScreenPage : ContentPage
    {
        UserService userService = new UserService();
        HttpClient httpclient = new HttpClient();
        public SplashScreenPage()
        {
            InitializeComponent();
            this.title.Text = "HEY! ";
            
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#F5F6F1");
            bool hasConnection = await CheckConnectivity();
            if (CrossConnectivity.Current.IsConnected && hasConnection)
            {

                BasketService.CurrentBasket = new Models.Basket(new System.Collections.ObjectModel.ObservableCollection<Models.ltem>());

                if (Helpers.DeviceHelper.GetMunchyId() == String.Empty)
                {
                    Helpers.DeviceHelper.SetMunchyId(Guid.NewGuid().ToString());
                    await userService.AddUserAsync(new User().CreateUser(Helpers.DeviceHelper.GetMunchyId()));
                }
                await Task.Delay(3000);
                await this.Navigation.PushAsync(new MainPage());
                this.Navigation.RemovePage(this);
            } 
            
            else 
            
            {
                await this.Navigation.PushAsync(new NoConnectivityPage());
                this.Navigation.RemovePage(this);
            }
        }

        private async Task<bool> CheckConnectivity()
        {
            try
            {
                var result = httpclient.GetAsync("https://google.dk");

                if (result.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return await Task.FromResult(true);
                }
                else
                {
                    return await Task.FromResult(false);
                }
            }catch(Exception e)
            {
                return await Task.FromResult(false);
            }

        }
    }
}
using System;
using System.Collections.Generic;
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
    public partial class NoConnectivityPage : ContentPage
    {
        HttpClient httpclient = new HttpClient();
        public NoConnectivityPage()
        {
            InitializeComponent();
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

        }

        async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            var access = e.NetworkAccess;
            var profiles = e.ConnectionProfiles;
            var hasConnection = await this.CheckConnectivity();

            if(access == NetworkAccess.Internet && hasConnection)
            {
                await this.Navigation.PushAsync(new SplashScreenPage());
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
            }
            catch (Exception e)
            {
                return await Task.FromResult(false);
            }
        }
    }
}
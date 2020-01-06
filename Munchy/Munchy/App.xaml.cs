using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Munchy.Services;
using Munchy.Views;
using Plugin.CloudFirestore;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Push;
using Xamarin.Essentials;
using System.Net.Http;
using System.Threading.Tasks;

namespace Munchy
{
    public partial class App : Application
    {
        HttpClient httpclient = new HttpClient();
        public App()
        {
            InitializeComponent();
           
            DependencyService.Register<MockDataStore>();
            DependencyService.Register<UserService>();
            DependencyService.Register<RecieptService>();
            DependencyService.Register<ProductService>();
            DependencyService.Register<BasketService>();
            DependencyService.Register<SubscriptionService>();
            DependencyService.Register<PaymentService>();
            DependencyService.Register<IdService>();
            DependencyService.Register<OrderSerivice>();
            DependencyService.Register<CreditCardService>();
            MainPage =  new NavigationPage(new SplashScreenPage());
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            var access = e.NetworkAccess;
            var profiles = e.ConnectionProfiles;
            var hasConnection = await this.CheckConnectivity();
            int count = 0;
            while (hasConnection == false)
            {
                count++; 

                if(count == 3)
                {
                    break;
                }
                 hasConnection= await CheckConnectivity();

                if(hasConnection == true)
                {
                    break;
                }


            }
            var page = Application.Current.MainPage; 

            if (access == NetworkAccess.Internet && hasConnection)
            {
                await page.Navigation.PushAsync(new SplashScreenPage());
               
            } else
            {
                await page.Navigation.PushAsync(new NoConnectivityPage());
            }
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=93268efa-e117-4651-ac75-029b43fd1ec3;", typeof(Analytics), typeof(Crashes), typeof(Push));
        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {
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

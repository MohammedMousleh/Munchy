using Munchy.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Munchy.Services
{
    public class SubscriptionService : ISubscriptionService<Subscription>
    {
        HttpClient client = PSPClient.Instance();
        public SubscriptionService()
        {
          
        }
        public  async Task<Subscription> AuthorizeSubscription(string subId, double amount, string token)
        {

            if (CrossConnectivity.Current.IsConnected)
            {

                var jsonData = JsonConvert.SerializeObject(new
                {
                    amount,
                    acquirer = "clearhaus",
                    card = new
                    {
                        token = token
                    }
                });

                HttpRequestMessage message = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://api.quickpay.net/subscriptions/" + subId + "/authorize"),
                    Content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json")
                };

                var res = await client.SendAsync(message);

                Subscription subscription = JsonConvert.DeserializeObject<Subscription>(res.Content.ReadAsStringAsync().Result);

                return await Task.FromResult(subscription);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Fejl", "Opret forbindelse til internettet og prøv igen", "OK");
                return null;
            }
        }

        public async Task<Subscription> CreateSubscription(Int64 orderId)
        {

            if (CrossConnectivity.Current.IsConnected)
            {

                var jsonData = JsonConvert.SerializeObject(new { order_id = orderId, currency = "dkk", description = "" });
                HttpRequestMessage message = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://api.quickpay.net/subscriptions/"),
                    Content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json")
                };

                var res = await client.SendAsync(message);

                Subscription subscription = JsonConvert.DeserializeObject<Subscription>(res.Content.ReadAsStringAsync().Result);

                return await Task.FromResult(subscription);
            }
            
            else
            {
                await Application.Current.MainPage.DisplayAlert("Fejl", "Opret forbindelse til internettet og prøv igen", "OK");
                return null;
            }
        }

        public async Task<Subscription> GetSubscription(string subId)
        {

            if(CrossConnectivity.Current.IsConnected)
            {
                HttpRequestMessage message = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://api.quickpay.net/subscriptions/" + subId)
                };

                var res = await client.SendAsync(message);

                Subscription subscription = JsonConvert.DeserializeObject<Subscription>(res.Content.ReadAsStringAsync().Result);

                return await Task.FromResult(subscription);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Fejl", "Opret forbindelse til internettet og prøv igen", "OK");
                return null;
            }
        }
    }
}


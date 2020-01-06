using Munchy.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Munchy.Services
{
    public class PaymentService : IPaymentService<Payment>
    {

        HttpClient client = PSPClient.Instance();

        IdService idService = new IdService(); 
        public async Task<Payment> GetPayment(string paymentId)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                HttpRequestMessage message = new HttpRequestMessage();

                message.Method = HttpMethod.Get;
                message.RequestUri = new Uri("https://api.quickpay.net/payments/" + paymentId);

                var res = await client.SendAsync(message);

                if (res.StatusCode == HttpStatusCode.OK)
                {
                    Payment payment = JsonConvert.DeserializeObject<Payment>(res.Content.ReadAsStringAsync().Result);
                    return await Task.FromResult(payment);

                }

                else
                {
                    await Application.Current.MainPage.DisplayAlert("Fejl", "Betalingen gik ikke igennem - prøv igen", "OK");
                    return null;
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Fejl", "Opret forbindelse til internettet og prøv igen", "OK");
                return null; 
            }
        }

        public async Task<Payment> CreatePayment()
        {

            try
            {
                HttpRequestMessage message = new HttpRequestMessage();
                long id = await idService.CreateOrderIdAsync();
                message.Method = HttpMethod.Post;
                message.RequestUri = new Uri("https://api.quickpay.net/payments?currency=dkk" + "&order_id=" + id + "test" + "&basket[][qty]=1&basket[][item_no]=1&basket[][item_name]=bagel&basket[][item_price]=100&basket[][vat_rate]=1");
                var res = await client.SendAsync(message);
                if (res.StatusCode == HttpStatusCode.Created)
                {
                    return JsonConvert.DeserializeObject<Payment>(await res.Content.ReadAsStringAsync());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Noget gik galt", "Din order kunne ikke gennemføres luk appen og prøv igen", "Ok");
                    return null;
                }
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Noget gik galt", "Luk appen helt ned og prøv igen", "Ok");
                return null;
            }

        }

        public async Task<Payment> AuthorizePayment(string paymentId)
        {
            double amount = BasketService.CurrentBasket.TotalPrice * 100;
            bool auto_capture = BasketService.CurrentBasket.Capture;
            try
            {
                HttpRequestMessage message = new HttpRequestMessage();
                var json = JsonConvert.SerializeObject(new
                {
                    amount,
                    language = "da",
                    auto_capture,
                    card = new
                    {
                        number = "1000000000000008",
                        cvd = "123",
                        expiration = "2112"
                    },
                    autoFee = false
                }
                );

                message.Method = HttpMethod.Post;
                message.RequestUri = new Uri("https://api.quickpay.net/payments/" + paymentId + "/authorize");
                message.Content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

                var res = await client.SendAsync(message);

                if (res.StatusCode == HttpStatusCode.Accepted)
                {
                    return JsonConvert.DeserializeObject<Payment>(res.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Noget gik galt", "Din order kunneikke gennemføres luk appen og prøv igen", "Ok");
                    return null;
                }
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Noget gik galt", "Luk appen helt ned og prøv igen", "Ok");
                return null;
            }
        }

        public async Task<Payment> CreateRecurringPayment(string id,bool auto_capture, Int64 order_id)
        {

           
            double amount = BasketService.CurrentBasket.TotalPrice * 100;
             
            var jsonData = JsonConvert.SerializeObject(new
            {
                order_id,
                amount,
                auto_capture
            });

            HttpRequestMessage message = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.quickpay.net/subscriptions/" + id + "/recurring"),
                Content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json")
            };

            var res = await client.SendAsync(message);

            Payment payment = JsonConvert.DeserializeObject<Payment>(res.Content.ReadAsStringAsync().Result);

            return payment;
        }

    }
}

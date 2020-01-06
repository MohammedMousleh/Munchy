using Munchy.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Munchy.Services
{
    public class CreditCardService : ICreditCardService<Card>
    {

        HttpClient client = PSPClient.Instance();
        public async Task<Card> AuthorizeCreditCard(string cardId, string number, string expiration, int cvd)
        {
            var jsonData = JsonConvert.SerializeObject(new
            {
                cancel_url = "https://www.google.dk/",
                continue_url = "https://mbsmunchy.herokuapp.com/cardsaved",
                language = "da",
                card = new
                {
                    number = number,
                    expiration = expiration,
                    cvd = cvd,
                    acquirer = "clearhaus"
                }
            });

            HttpRequestMessage message = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.quickpay.net/cards/" + cardId + "/authorize"),
                Content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json")
            };

            var res = await client.SendAsync(message);
            Card card = JsonConvert.DeserializeObject<Card>(res.Content.ReadAsStringAsync().Result);
            return await Task.FromResult(card);
        }

        public async Task<Card> CreateCreditCard()
        {
            HttpRequestMessage message = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.quickpay.net/cards")
            };

            var res = await client.SendAsync(message);

            Card card = JsonConvert.DeserializeObject<Card>(res.Content.ReadAsStringAsync().Result);

            return await Task.FromResult(card);
        }

        public async Task<CardDetails> CreateCreditCardToken(string cardId)
        {
            HttpRequestMessage message = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.quickpay.net/cards/" + cardId + "/tokens")
            };

            var res = await client.SendAsync(message);

            CardDetails cardDetails = JsonConvert.DeserializeObject<CardDetails>(res.Content.ReadAsStringAsync().Result);

            return await Task.FromResult(cardDetails);
        }

        public Task<Card> SaveCreditCard()
        {
            throw new NotImplementedException();
        }

       public class CardDetails{
            public string token { get; set; }
            public bool is_used { get; set; }
            public DateTime created_at { get; set; }
        }
    }
}

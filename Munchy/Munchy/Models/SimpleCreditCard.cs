using System;
using System.Collections.Generic;
using System.Text;

namespace Munchy.Models
{
    public class SimpleCreditCard
    {
        public string CardType { get; set; }
        public string ExpiryYear { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryMonth { get; set; }
        public SimpleCreditCard(string cardType, string expiryYear, string expiryMonth, string cardNumber)
        {
            this.CardType = cardType;
            this.ExpiryYear = expiryYear;
            this.ExpiryMonth = expiryMonth;
            this.CardNumber = cardNumber;
        }
        public SimpleCreditCard()
        {

        }
    }
}

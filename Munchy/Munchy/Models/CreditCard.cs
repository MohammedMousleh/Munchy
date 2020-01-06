using System;
using System.Collections.Generic;
using System.Text;

namespace Munchy.Models
{
    public class CreditCard
    {
        public string CardType { get; }
        public int ExpiryYear { get; set; }
        public string Cvv { get; set; }
        public string LastFourDigitsOfCardNumber { get; }
        public string CardholderName { get; set; }
        public string CardNumber { get; set; }
        public int ExpiryMonth { get; set; }

        public bool isExpireValid { get; set; }

        public CreditCard(string cardNumber, string cvv, int expireMonth, int expireYear, bool isExpireValid)
        {
            this.CardNumber = cardNumber;
            this.Cvv = cvv;
            this.ExpiryMonth = expireMonth;
            this.ExpiryYear = expireYear;
            this.isExpireValid = isExpireValid;
        }
    }
}

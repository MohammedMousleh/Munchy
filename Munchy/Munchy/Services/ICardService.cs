using Munchy.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Munchy.Services
{
   public interface ICardService
    {
  
            void StartCapture();

            string GetCardNumber();

            string GetCardholderName();

            CreditCard GetCard();
        
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Munchy.Services.CreditCardService;

namespace Munchy.Services
{
    public interface ICreditCardService<T>
    {
        Task<T> CreateCreditCard();
        Task<T> AuthorizeCreditCard(string cardId, string number, string expiration, int cvd);
        Task<CardDetails> CreateCreditCardToken(string cardId);

        Task<T> SaveCreditCard(); 

    }
}

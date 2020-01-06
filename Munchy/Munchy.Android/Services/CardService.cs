using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Card.IO;
using Munchy.Droid.Services;
using Munchy.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(CardService))]
namespace Munchy.Droid.Services
{
   public class CardService: ICardService
    {
        private Activity activity;
        public void StartCapture()
        {
            InitCardService();

            var intent = new Intent(activity, typeof(CardIOActivity));
            intent.PutExtra(CardIOActivity.ExtraRequireExpiry, true);
            intent.PutExtra(CardIOActivity.ExtraRequireCvv, true);
            intent.PutExtra(CardIOActivity.ExtraRequirePostalCode, false);
            intent.PutExtra(CardIOActivity.ExtraUseCardioLogo, true);


            activity.StartActivityForResult(intent, 101);
        }

        public string GetCardNumber()
        {
            var s = (InfoShareHelper.Instance.CardInfo != null) ? InfoShareHelper.Instance.CardInfo.CardNumber : null;
            System.Console.WriteLine();
            return s;
        }

        public string GetCardholderName()
        {
            var s = (InfoShareHelper.Instance.CardInfo != null) ? InfoShareHelper.Instance.CardInfo.CardholderName : null;
            System.Console.WriteLine(s);
            return s;
        }

        private void InitCardService()
        {
            // Init current activity
            var context = Forms.Context;
            activity = context as Activity;
        }

        public Models.CreditCard GetCard()
        {
            var card = (InfoShareHelper.Instance.CardInfo != null) ? InfoShareHelper.Instance.CardInfo : null;

            if (card != null)
            {


                return new
                    Models.CreditCard(
                    card.CardNumber,
                    card.Cvv,
                    card.ExpiryMonth,
                    card.ExpiryYear,
                    card.IsExpiryValid);
            }
            return null;

        }

        public class InfoShareHelper
        {
            private static InfoShareHelper instance = null;
            private static readonly object padlock = new object();

            public Card.IO.CreditCard CardInfo { get; set; }

            public static InfoShareHelper Instance
            {
                get
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new InfoShareHelper();
                        }
                        return instance;
                    }
                }
            }
        }
    }
}
using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Card.IO;
using static Munchy.Droid.Services.CardService;
using Android.Content;
using Munchy.Models;
using Munchy.Services;
using Munchy.ViewModels;
using Xamarin.Forms;
using Munchy.Views;
using Plugin.Fingerprint;
using Plugin.Badge.Droid;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(BadgedTabbedPageRenderer))]

namespace Munchy.Droid
{
    [Activity(Label = "Munchy", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        IPaymentService<Payment> paymentService = new PaymentService();
        User user;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Forms.SetFlags("Shell_Experimental", "Visual_Experimental", "CarouselView_Experimental", "FastRenderers_Experimental");
            CrossFingerprint.SetCurrentActivityResolver(() => this);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        protected async override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (data != null)
            {

                InfoShareHelper.Instance.CardInfo = data.GetParcelableExtra(CardIOActivity.ExtraScanResult).JavaCast<Card.IO.CreditCard>();
                var card = InfoShareHelper.Instance.CardInfo;


                var creditCard = new
                    Models.CreditCard(
                    card.CardNumber,
                    card.Cvv,
                    card.ExpiryMonth,
                    card.ExpiryYear,
                    card.IsExpiryValid);


                Payment payment = await paymentService.CreatePayment();
                await paymentService.AuthorizePayment(payment.id.ToString());
                payment = await paymentService.GetPayment(payment.id.ToString());

                if (payment != null && payment.accepted)
                {
                    Order order = new Order(BasketService.CurrentBasket, payment);
                    User user = await DependencyService.Get<UserService>().GetUserAsync(Helpers.DeviceHelper.GetMunchyId());
                    await DependencyService.Get<RecieptService>().AddRecieptAsync(order,user.Id);
                    Page page = Xamarin.Forms.Application.Current.MainPage;
                    await page.Navigation.PushAsync(new SuccesPage("Betalingen er nu gennemført", new SavedCardPage()));
                    Xamarin.Forms.Application.Current.MainPage.Navigation.RemovePage(Xamarin.Forms.Application.Current.MainPage.Navigation.NavigationStack[1]);
                    BasketService.CurrentBasket.ClearBasketItems();
                }

                string cardNumber = InfoShareHelper.Instance.CardInfo.CardNumber;
                string cvv = InfoShareHelper.Instance.CardInfo.Cvv;
                string expiration = InfoShareHelper.Instance.CardInfo.ExpiryYear.ToString().Substring(0, 2) + InfoShareHelper.Instance.CardInfo.ExpiryMonth.ToString();
            }

            else
            {
                Console.WriteLine("Scanning Canceled!");
            }

        }
    }
}
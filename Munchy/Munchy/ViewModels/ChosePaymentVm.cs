using Munchy.Models;
using Munchy.Services;
using Munchy.Views;
using Plugin.Fingerprint;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Munchy.ViewModels
{
    public class ChosePaymentVm : BaseViewModel
    {

        User user;

        public ChosePaymentVm()
        {
            PayNowCommand = new Command(PayNowExecute);
            OrderCommand = new Command(OrderNowExecute);

            MessagingCenter.Subscribe<ChosePaymentPage>(this, "ChosePaymentAppear", async (obj) =>
            {
                await Init();
            });

        }

        public ICommand PayNowCommand { get; set; }
        public ICommand OrderCommand { get; set; }


        public async Task Init()
        {

            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                user = await UserService.GetUserAsync(Helpers.DeviceHelper.GetMunchyId());
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void PayNowExecute()
        {
         
                IsBusy = true;
                Pay(true);
                IsBusy = false;

        }

        public void OrderNowExecute()
        {
            IsBusy = true;
            Pay(false);
            IsBusy = false;

        }


        private void Pay(bool captureNow)
        {
         
            if (this.IsNoAuthenticationPayment())
            {
                PayWithoutAuthetication(captureNow);

            }
            else if (IsPasswordPayment())
            {
                PayWithPassword(captureNow);
            }

            else
            {
                PayWithFingerPrint(captureNow);
            }
    
        }

        private void PayWithoutAuthetication(bool captureNow)
        {
            DependencyService.Get<ICardService>().StartCapture();
            BasketService.CurrentBasket.Capture = captureNow;
        }


        public void PayWithPassword(bool captureNow)
        {

        }

        private async void PayWithFingerPrint(bool captureNow)
        {

            IsBusy = true;
            var result = await CrossFingerprint.Current.AuthenticateAsync("Scan for at betale");

            if (result.Authenticated)
            {
                IsBusy = true;
                Int64 order_id = await IdService.CreateOrderIdAsync();
                Payment payment = await PaymentService.CreateRecurringPayment(user.SubId, captureNow, order_id);
                payment = await PaymentService.GetPayment(payment.id.ToString());

                if (payment.accepted)
                {
                    IsBusy = true;
                    Order order = await OrderService.CreateOrder(BasketService.CurrentBasket, payment);
                    Reciept reciept = await RecieptService.AddRecieptAsync(order, Helpers.DeviceHelper.GetMunchyId());
                    BasketService.CurrentBasket.ClearBasketItems();
                    if (captureNow)
                    {
                        Page page = Application.Current.MainPage;
                        await page.Navigation.PushAsync(new SuccesPage("Betalingen er nu gennemført", new ScanQRPage(reciept)));
                        Application.Current.MainPage.Navigation.RemovePage(Application.Current.MainPage.Navigation.NavigationStack[1]);


                    }
                    else
                    {
                        Page page = Application.Current.MainPage;
                        await page.Navigation.PushAsync(new SuccesPage("Betalingen er nu gennemført", Application.Current.MainPage.Navigation.NavigationStack[0]));
                        Application.Current.MainPage.Navigation.RemovePage(Application.Current.MainPage.Navigation.NavigationStack[0]);

                    }

                }
            }
            IsBusy = false;
          
        }

        private bool IsPasswordPayment()
        {
                if (user.SubId != "" && user.password != String.Empty)
                {
                    return true;
                }
                return false;
        }

        private bool IsNoAuthenticationPayment()
        {

            if (user.SubId == String.Empty)
            {
                return true;
            }
            return false;
        }
    }

}

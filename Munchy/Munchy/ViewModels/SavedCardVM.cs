using Munchy.Models;
using Munchy.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using static Munchy.Services.CreditCardService;

namespace Munchy.ViewModels
{
    public class SavedCardVM: BaseViewModel
    {

        public SavedCardVM()
        {
            OnSavedCardCommand = new Command(async () =>  await OnSaveCommandExecute());
            OnNoCommand = new Command(async () => await OnNoCommandExecute());
        }

        public ICommand OnSavedCardCommand { get; set; }
        public ICommand OnNoCommand { get; set; }

        private async Task OnSaveCommandExecute()
        {

            if (IsBusy)
                return;

            IsBusy = true;

            try
            {

                Int64 orderId = await IdService.CreateOrderIdAsync();

                User user = await UserService.GetUserAsync(Helpers.DeviceHelper.GetMunchyId());

                Card card = await CreditCardService.CreateCreditCard();

                card = await CreditCardService.AuthorizeCreditCard(card.id.ToString(), "1000000000000008", "2112", 123);

                Subscription subscription = await SubscriptionService.CreateSubscription(orderId);

                CardDetails cardDetails = await CreditCardService.CreateCreditCardToken(card.id.ToString());

                Subscription authSub = await SubscriptionService.AuthorizeSubscription(subscription.id.ToString(), 1000, cardDetails.token);

                authSub = await SubscriptionService.GetSubscription(authSub.id.ToString());

                if (authSub.accepted)
                {
                    SimpleCreditCard creditCard = new SimpleCreditCard(card.metadata.brand.ToString(), card.metadata.exp_year.ToString(), card.metadata.exp_month.ToString(), "XXXX XXXX XXXX " + card.metadata.last4);
                    await this.UserService.CreateUserCreditCard(user.Id, creditCard);
                    await Application.Current.MainPage.Navigation.PushAsync(new CreatePaymentAuthenticationPage(authSub.id.ToString()));
                }
            }
            catch(Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Fejl!", e.ToString(), "OK");
            }
            finally
            {
                IsBusy = false; 
            }
        }
        private async Task OnNoCommandExecute()
        {
            if (IsBusy)
                return;
            try
            {
                 Application.Current.MainPage.Navigation.RemovePage(Application.Current.MainPage.Navigation.NavigationStack[1]);
            }
            
            catch(Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Fejl!", e.ToString(), "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}

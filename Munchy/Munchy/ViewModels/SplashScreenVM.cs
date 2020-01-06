
using Munchy.Services;

namespace Munchy.ViewModels
{
   public class SplashScreenVM: BaseViewModel
    {
        public SplashScreenVM()
        {
            BasketService.CurrentBasket = new Models.Basket(new System.Collections.ObjectModel.ObservableCollection<Models.ltem>());
        }


        
    }
}

using Munchy.Models;
using Munchy.Services;
using Munchy.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Munchy.ViewModels
{
    public class BasketVM: BaseViewModel
    {

        public Munchy.Models.Basket Basket
        {
            get { return BasketService.CurrentBasket; }
            set { SetProperty(ref BasketService.CurrentBasket, value); }
        }



        public BasketVM()
        {
            Icon = "basketIcon.png";
            Basket = BasketService.CurrentBasket;
            RemoveOneProductFromItemCommand = new Command(RemoveProduct);
            AddOneProductToItemCommand = new Command(AddProduct);
            ChosePaymentMethodCommand = new Command(ChosePaymentMethod);
            DeleteItemCommand = new Command(DeleteItem);
        }

        public ICommand RemoveOneProductFromItemCommand { get; }
        public ICommand AddOneProductToItemCommand { get; }
        public ICommand ChosePaymentMethodCommand { get; }
        public ICommand DeleteItemCommand { get; }



        private void DeleteItem(object item)
        {
            ltem itemToDelete = (ltem)item;

            Basket.Items.Remove(itemToDelete);
            Basket.CalculatePrice(); 
        }

        public string CreateUniqueOrderId()
        {

            return ""; 

        }

        private async void RemoveProduct(object item)
        {
            ltem itemToChange = (ltem)item;

            if (itemToChange.Quantity > 1)
            {
                itemToChange.Quantity--;
            }
            else if (itemToChange.Quantity == 1)
            {
                var actionsShhet = await Application.Current.MainPage.DisplayActionSheet("Fjern produktet fra kurven", "Nej", "Ja");

                switch (actionsShhet)
                {
                    case "Ja":
                        DeleteItem(item);
                        break;
                }
            }
            Basket.CalculatePrice();
        }
        private void AddProduct(object item)
        {
            ltem itemToChange = (ltem)item;

            if (itemToChange.Quantity > 0)
            {
                itemToChange.Quantity++;
            }
            Basket.CalculatePrice();

        }



        public async void ChosePaymentMethod()
        {
            IsBusy = true;
            await Application.Current.MainPage.Navigation.PushAsync(new ChosePaymentPage());
            IsBusy = false;
       
        }

        public double GetFullPrice()
        {
            return Basket.TotalPrice * 100;
        }
    }
}

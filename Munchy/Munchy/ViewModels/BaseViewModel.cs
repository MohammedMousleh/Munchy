using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using Munchy.Models;
using Munchy.Services;

namespace Munchy.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();
        public IUserService<User> UserService => DependencyService.Get<IUserService<User>>();
       
        public IRecieptService<Reciept> RecieptService = DependencyService.Get<IRecieptService<Reciept>>();

        public IProductsService<Product> ProductsService = DependencyService.Get<IProductsService<Product>>();

        public ISubscriptionService<Subscription> SubscriptionService = DependencyService.Get<ISubscriptionService<Subscription>>();

        public IPaymentService<Payment> PaymentService = DependencyService.Get<IPaymentService<Payment>>();

        public IIdService IdService = DependencyService.Get<IIdService>();

        public IOrderService<Order> OrderService = DependencyService.Get<IOrderService<Order>>();

        public ICreditCardService<Card> CreditCardService = DependencyService.Get<ICreditCardService<Card>>();
        

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        string icon = string.Empty;
        public string Icon
        {
            get { return icon; }
            set { SetProperty(ref icon, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,[CallerMemberName]string propertyName = "",Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}

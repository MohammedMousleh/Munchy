using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Munchy.Models
{
   public  class Basket : INotifyPropertyChanged
    {


        public List<Product> existingProducts;
        private bool _capture;

        public bool Capture
        {
            get { return _capture; }
            set { _capture = value; NotifyPropertyChanged(); }
        }


        private ObservableCollection<ltem> _items;

        public ObservableCollection<ltem> Items
        {
            get { return _items; }
            set { _items = value; NotifyPropertyChanged(); }
        }

        private double _totalPrice;

        public double TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; NotifyPropertyChanged(); }
        }

        public void ClearBasketItems()
        {
            this.Items.Clear();
            this.TotalPrice = 0;
        }

        public Basket(ObservableCollection<ltem> items)
        {
            this.Items = items;
        }

        public void CalculatePrice()
        {
            double price = 0;

            foreach (var item in Items)
            {
                price += item.Price;
            }

            this.TotalPrice = price;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddItem(ltem newItem)
        {

            if (Items.Count == 0)
            {
                Items.Add(newItem);
                return;
            }

            ltem retrivedItem = Items.Where(i => i.Product.Id == newItem.Product.Id).FirstOrDefault();

            if (retrivedItem != null)
            {
                retrivedItem.Quantity++;
            }
            else
            {
                Items.Add(newItem);
            }
        }
    }
}

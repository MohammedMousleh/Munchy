using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Munchy.Models
{
    public class ltem: INotifyPropertyChanged
    {

        public ltem()
        {

        }
        private Product _prodduct;
        public Product Product
        {
            get { return _prodduct; }
            set { _prodduct = value; NotifyPropertyChanged(); }
        }

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; NotifyPropertyChanged(); CalculatePrice(); }
        }

        private double _price;
        public double Price
        {
            get { return _price; }
            set { _price = value; NotifyPropertyChanged(); }
        }

        public ltem(Product product, int quantity)
        {
            this.Product = product;
            this.Quantity = quantity;
            this.Price = this.Quantity * this.Product.Price;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CalculatePrice()
        {
            this.Price = this.Quantity * Convert.ToDouble(this.Product.Price);
        }
    }
}

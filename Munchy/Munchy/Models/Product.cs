using Plugin.CloudFirestore.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Munchy.Models
{
    public class Product : INotifyPropertyChanged
    {

        public Product()
        {

        }

        private bool _isOnSale;
       
        public bool IsOnSale { get { return _isOnSale; } set { _isOnSale = value; NotifyPropertyChanged(); } }


        private string _description;

        
        public string Description { get { return _description; } set { _description = value; NotifyPropertyChanged(); } }

        private bool _isHalal;

       
        public bool IsHalal { get { return _isHalal; } set { _isHalal = value; NotifyPropertyChanged(); } }

        private bool _hasMeat;
       
        public bool HasMeat { get { return _hasMeat; } set { _hasMeat = value; NotifyPropertyChanged(); } }



        private bool _isWeightProduct;
       
        public bool IsWeightProduct { get { return _isWeightProduct; } set { _isWeightProduct = value; NotifyPropertyChanged(); } }

        private bool _inStock;
     
        public bool InStock { get { return _inStock; } set { _inStock = value; NotifyPropertyChanged(); } }

        private string _name;

     
        public string Name { get { return _name; } set { _name = value; NotifyPropertyChanged(); } }

        private double _price;
      
        public double Price { get { return _price; } set { _price = value; NotifyPropertyChanged(); } }

        private string _id;
       
        public string Id { get { return _id; } set { _id = value; NotifyPropertyChanged(); } }

        private List<string> _allegener;

       
        public List<string> Allegener { get { return _allegener; } set { _allegener = value; NotifyPropertyChanged(); } }

        private string _meatType;
       
        public string MeatType { get { return _meatType; } set { _meatType = value; NotifyPropertyChanged(); } }

        private AmountOffer _amountOffer;

       
        public AmountOffer AmountOffer { get { return _amountOffer; } set { _amountOffer = value; NotifyPropertyChanged(); } }


        private ProductImage _productImage; 
        public ProductImage ProductImage
        {
            get { return _productImage; }
            set { _productImage = value; NotifyPropertyChanged(); }
        }


        private List<string> _categories;
        public List<string> Categories
        {
            get { return _categories; }
            set { _categories = value; NotifyPropertyChanged(); }
        }

        private string _documentId;

        [Id]
        public string DocumentId
        {
            get { return _documentId;  }
            set { _documentId = value; NotifyPropertyChanged(); }
        }

        private Offer _offer;
        public Offer Offer
        {
            get { return _offer; }
            set { _offer = value; NotifyPropertyChanged(); }
        }


        private string _size; 
        public string Size
        {
            get { return _size; }
            set { _size = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

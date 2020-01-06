using Plugin.CloudFirestore.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Munchy.Models
{
    public class Reciept
    {
        public Reciept()
        {

        }

        [Id]
        public string DoucmentId { get; set; }

        public string Id { get; set; }

        public DateTime _created;

        [MapTo("Created")]
        public DateTime Created
        {
            get { return _created; }
            set { _created = value; SetProperty(ref _created, value); }
        }

        private string _valuta;

        [MapTo("Valuta")]
        public string Valuta
        {
            get { return _valuta; }
            set { _valuta = value; SetProperty(ref _valuta, value); }
        }

        private bool _isScanned;

        [MapTo("IsScanned")]
        public bool IsScanned
        {
            get { return _isScanned; }
            set { _isScanned = value; SetProperty(ref _isScanned, value); }
        }

        private string _installId;

        [MapTo("UserId")]
        public string InstallId
        {
            get { return _installId; }
            set { _installId = value; SetProperty(ref _installId, value); }
        }


        private double _price;

        [MapTo("Price")]
        public double Price
        {
            get { return _price; }
            set { _price = value; SetProperty(ref _price, value); }
        }

        private Order _order;

        [MapTo("Order")]
        public Order Order
        {
            get { return _order; }
            set { _order = value; SetProperty(ref _order, value); }
        }

        private string _text;

        [MapTo("Text")]
        public string Text
        {
            get { return _text; }
            set { _text = value; SetProperty(ref _text, value); }
        }

        public Reciept(Order order, string installId)
        {
            this.Created = DateTime.Now;
            this.Order = order;
            this.InstallId = installId;
            this.IsScanned = false;
            this.Valuta = "DKK";
            Price = order.Price;
            this.Text = "Scan for at få din kvittering";
            this.Id = Guid.NewGuid().ToString();
        }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName]string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    
    }
}

using Munchy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Munchy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowRecieptPage : ContentPage
    {
        public ShowRecieptPage(Reciept reciept)
        {
            InitializeComponent();
            BindingContext = reciept;
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#F5F6F1");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Black;

        }
    }
}
using Munchy.Behavious;
using Munchy.Models;
using Munchy.Renderes;
using Munchy.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Munchy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowAllProductsPage : ContentPage
    {

        ProductsVm viewModel;

        public ShowAllProductsPage(ObservableCollection<Product> products)
        {
            InitializeComponent();
            BindingContext = viewModel =  new ProductsVm();
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            var currentColumn = 0;
            var currentRow = 0;

            for (int i = 0; i < products.Count; i++)
            {

                Product product = products[i];


                if (i % 2 == 0 && i !=0)
                {
                    grid.RowDefinitions.Add(new RowDefinition());
                    currentRow++;
                    currentColumn = 0;
                }
                
                grid.Children.Add(CreateFrame(product), currentColumn, currentRow);
                currentColumn++;
            }
        }


        private MaterialFrame CreateFrame(Product product)
        {
            MaterialFrame frame = new MaterialFrame();
            frame.HasShadow = true;
            frame.CornerRadius = 2;
            frame.Padding = 0;
            frame.HeightRequest = 280;
            frame.Elevation = 6;
            frame.IsVisible = product.InStock;
          
            Grid grid2 = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto }

                }
            };
           
            Image image = new Image { Source = product.ProductImage.ImageSource, Aspect = Aspect.AspectFill, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
            grid2.Children.Add(image,0, 0);
            grid2.ColumnSpacing = 5;
            grid2.Children.Add(new Image { Source = "tilbud.png", IsVisible= true, Aspect = Aspect.AspectFill, VerticalOptions = LayoutOptions.Center}, 0, 0);
            Grid.SetRowSpan(image, 2);
            grid2.Children.Add(new Label { Margin = new Thickness(10, 0, 0, 0), Text =product.Name, TextColor = Color.Black, FontSize = 15, FontFamily = "sans-serif-condensed", FontAttributes = FontAttributes.Bold },0,2);
            grid2.Children.Add(new Label { Margin = new Thickness(10, 0, 0, 0), Text ="Pris " + product.Price.ToString() + ".kr", TextColor = Color.Black, FontSize = 13, FontFamily = "sans-serif-condensed", FontAttributes = FontAttributes.Bold }, 0, 3);
         
            StackLayout stackLayout = new StackLayout {
                Orientation = StackOrientation.Horizontal, 
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            if (product.AmountOffer != null)
            {
                stackLayout.Children.Add(new Label
                {
                    FontSize = 10,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.Red,
                    FontFamily = "sans-serif-condensed",
                    Text = "Ta" + product.AmountOffer.Amount + "for"
                });
                stackLayout.Children.Add(new Label
                {
                    FontSize = 10,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.Red,
                    FontFamily = "sans-serif-condensed",
                    Text = product.AmountOffer.Price + ".kr"
                });
            }
            
            Button button = new Button { 
            
                FontSize = 15,
                FontAttributes = FontAttributes.Bold,
                FontFamily = "sans-serif-condensed",
                Text = "Tilføj til kurv", 
                BackgroundColor = Color.Transparent,
                Command = viewModel.AddToBasketCommand,
                CommandParameter = product,
               
            };
            button.Behaviors.Add(new ButtonAnimationBehavior());
            grid2.Children.Add(stackLayout, 0, 4);
            grid2.Children.Add(button, 0, 5);
            frame.Content = grid2;
            return frame;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.AllProducts.Count == 0)
            {
                viewModel.LoadProductCategoryCommand.Execute(null);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Munchy.Behavious
{
    class ButtonAnimationBehavior : Behavior<Button>
    {
        protected override void OnAttachedTo(Button bindable)
        {
            bindable.Clicked += ButtonClicked;
            base.OnAttachedTo(bindable);
        }



        private  void ButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;

            var startScale = button.Scale;

             Task.Run(() =>
            {
                button.TextColor = Color.FromHex("#74D3A7");
                button.ScaleTo(startScale + 0.23);
                Thread.Sleep(115);
                button.TextColor = Color.Black;
                button.ScaleTo(startScale);


            });
            
        }

        protected override void OnDetachingFrom(Button bindable)
        {
            bindable.Clicked += ButtonClicked;
            base.OnDetachingFrom(bindable);
        }
    }
}

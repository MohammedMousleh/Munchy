using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using Munchy.Droid.Renderes;
using Munchy.Renderes;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MaterialButton), typeof(MaterialButtonRenderer))]
namespace Munchy.Droid.Renderes
{
    public class MaterialButtonRenderer : Xamarin.Forms.Platform.Android.AppCompat.ButtonRenderer
    {


        public MaterialButtonRenderer(Context context): base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement == null)
                return;

            var materialButton = (MaterialButton)Element;

            // we need to reset the StateListAnimator to override the setting of Elevation on touch down and release.
            Control.StateListAnimator = new Android.Animation.StateListAnimator();

            // set the elevation manually
            ViewCompat.SetElevation(this, materialButton.Elevation);
            ViewCompat.SetElevation(Control, materialButton.Elevation);
        }

        public override void Draw(Canvas canvas)
        {
            var materialButton = (MaterialButton)Element;
            Control.Elevation = materialButton.Elevation;
            base.Draw(canvas);
        }


        /// <param name="e"></param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "Elevation")
            {
                var materialButton = (MaterialButton)Element;
                ViewCompat.SetElevation(this, materialButton.Elevation);
                ViewCompat.SetElevation(Control, materialButton.Elevation);
                UpdateLayout();
            }
        }
    }
}
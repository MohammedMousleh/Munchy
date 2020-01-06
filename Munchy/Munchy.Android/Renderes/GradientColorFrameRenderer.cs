using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Munchy.CustomControls;
using Munchy.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(GradientColorFrame), typeof(GradientColorFrameRenderer))]
namespace Munchy.Droid
{
    public class GradientColorFrameRenderer: VisualElementRenderer<Frame>
    {

        private Color StartColor { get; set; }
        private Color EndColor { get; set; }


        public GradientColorFrameRenderer(Context context) : base(context)
        {

        }

        protected override void DispatchDraw(global::Android.Graphics.Canvas canvas)
        {
            #region for Vertical Gradient
            //var gradient = new Android.Graphics.LinearGradient(0, 0, 0, Height,
            #endregion

            #region for Horizontal Gradient
            var gradient = new Android.Graphics.LinearGradient(0, 0, Width, 0,
            #endregion

                   this.StartColor.ToAndroid(),
                   this.EndColor.ToAndroid(),
                   Android.Graphics.Shader.TileMode.Mirror);

            var paint = new Android.Graphics.Paint()
            {
                Dither = true,
            };
            paint.SetShader(gradient);
            canvas.DrawRect(new Android.Graphics.Rect(0, 0, Width, Height), paint);
            base.DispatchDraw(canvas);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }
            try
            {
                var frame = e.NewElement as GradientColorFrame;
                this.StartColor = frame.StartColor;
                this.EndColor = frame.EndColor;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"ERROR:", ex.Message);
            }
        }

    }
}
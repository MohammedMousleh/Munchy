using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Munchy.Renderes
{

    public class MaterialButton : Button
    {
        public static BindableProperty ElevationProperty = BindableProperty.Create(nameof(Elevation), typeof(float), typeof(MaterialButton), 4.0f);

        public float Elevation
        {
            get
            {
                return (float)GetValue(ElevationProperty);
            }
            set
            {
                SetValue(ElevationProperty, value);
            }
        }
    }
}

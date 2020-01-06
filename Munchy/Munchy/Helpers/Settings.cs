using Munchy.Themes;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Munchy.Helpers
{

    public enum Theme
    {
        Light,
        Dark
    }
   public class Settings
    {

        public void SetTheme(Theme theme)
        {
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;

            if(mergedDictionaries != null)
            {
                mergedDictionaries.Clear();
                Device.BeginInvokeOnMainThread(() =>
                {
                    switch (theme)
                    {
                        case Theme.Light:
                            Application.Current.Resources.MergedDictionaries.Add(new LightTheme());
                            break;
                    }
                });
            }
            
        }
    }
}

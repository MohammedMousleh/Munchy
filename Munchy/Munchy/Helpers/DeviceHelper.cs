using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Munchy.Helpers
{
    public static class DeviceHelper
    {

        public static void SetMunchyId(string id)
        {

            Console.WriteLine(id);
            Console.WriteLine("Muddi");
            var device = DeviceInfo.Model;
            var manufacturer = DeviceInfo.Manufacturer;
            var version = DeviceInfo.VersionString;
            id = id + device + manufacturer + version;
            
            Preferences.Set("munchyid",id);
        }
       public static string GetMunchyId()
        {
            return  Preferences.Get("munchyid",String.Empty);

        }


    }
}

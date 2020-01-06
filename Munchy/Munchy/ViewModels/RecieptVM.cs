using Munchy.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Munchy.ViewModels
{
    public class RecieptVM: BaseViewModel
    {
      

        public ObservableCollection<Reciept> ScannedReciepts { get; set; }
        public ObservableCollection<Reciept> NotScannedReciepts { get; set; }

        public Command LoadScannedRecieptsCommand { get; set; }
        public Command LoadNotScannedRecieptsCommand { get; set; }

        public RecieptVM()
        {
            Icon = "receiptIcon.png";
            ScannedReciepts = new ObservableCollection<Reciept>();
            NotScannedReciepts = new ObservableCollection<Reciept>();
            LoadScannedRecieptsCommand = new Command(async () => await ExecuteLoadScannedRecieptsCommand());
            LoadNotScannedRecieptsCommand = new Command(async () => await ExecuteLoadNotScannedRecieptsCommand());

        }


        async Task ExecuteLoadScannedRecieptsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                ScannedReciepts.Clear();
                var reciepts = await RecieptService.GetScannedReciepts(Helpers.DeviceHelper.GetMunchyId());
                foreach (var reciept in reciepts)
                {
                    if(reciept.IsScanned == true)
                    {
                        ScannedReciepts.Add(reciept);

                    }
                }
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteLoadNotScannedRecieptsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                NotScannedReciepts.Clear();
                var reciepts = await RecieptService.GetUnScannedReciepts(Helpers.DeviceHelper.GetMunchyId());
                foreach (var reciept in reciepts)
                {
                    if(reciept.IsScanned == false)
                    {
                        NotScannedReciepts.Add(reciept);
                    }
                }
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}

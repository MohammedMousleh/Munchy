using Munchy.Models;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munchy.Services
{
    public class RecieptService : IRecieptService<Reciept>
    {
        private ICollectionReference RecieptCollectionref;


        public RecieptService()
        {
            RecieptCollectionref = CrossCloudFirestore.Current.Instance.GetCollection("Reciepts");
        }

        public async Task<Reciept> AddRecieptAsync(Order order, string userId)
        {
            Reciept reciept = new Reciept(order, userId);
            await RecieptCollectionref.AddDocumentAsync(reciept);
            return await Task.FromResult(reciept);
        }

        public Task<bool> DeleteRecieptAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Reciept> GetRecieptAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Reciept>> GetScannedReciepts(string installId)
        {
            var reciepts = await CrossCloudFirestore
                .Current
                .Instance.
                GetCollection("Reciepts")
                .WhereEqualsTo("IsScanned",true)
                .GetDocumentsAsync();
            return await Task.FromResult(reciepts.ToObjects<Reciept>().Where(r => r.Order.InstallId == installId));
        }

        public async Task<IEnumerable<Reciept>> GetUnScannedReciepts(string installId)
        {
            var reciepts = await CrossCloudFirestore
                .Current
                .Instance.
                GetCollection("Reciepts")
                .WhereEqualsTo("IsScanned", false)
                .GetDocumentsAsync();
            return await Task.FromResult(reciepts.ToObjects<Reciept>().Where(r => r.Order.InstallId == installId));
        }

        public async Task<IEnumerable<Reciept>> GetRecieptsAsync(string installId, bool forceRefresh = false)
        {

            var reciepts = await this.RecieptCollectionref.GetDocumentsAsync();
            return await Task.FromResult(reciepts.ToObjects<Reciept>().Where(r => r.Order.InstallId == installId));
        }

        public async Task<bool> UpdateRecieptAsync(Reciept reciept)
        {
            var reciepts = await GetRecieptsAsync(Helpers.DeviceHelper.GetMunchyId());
            Reciept retirvedReciept = reciepts.Where(r => r.Id == reciept.Id).FirstOrDefault();
            IDocumentReference document = CrossCloudFirestore.Current.Instance.GetCollection("Reciepts").GetDocument(retirvedReciept.DoucmentId);
            await document.UpdateDataAsync(new { IsScanned = true, Text = "Tryk her for at se kvittering" });
            return await Task.FromResult(true);
        }
    }
}

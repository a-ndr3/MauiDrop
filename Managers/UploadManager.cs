using Google.Apis.Drive.v3;
using MauiDrop.DataItems;
using MauiDrop.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDrop.Managers
{
    internal class UploadManager
    {
        private static readonly UploadManager instance = new UploadManager();
        public static UploadManager Instance => instance;

        public event Action<int, int> ProgressChanged;
        public event Action UploadCompleted;

        private ICloudService service = null;
        public ObservableCollection<UploadFile> FilesToUpload { set; get; }

        private UploadManager()
        {
            FilesToUpload = new ObservableCollection<UploadFile>();
        }

        public bool IsInitialized => service != null;

        public void Initialize(ICloudService cloudService)
        {
            if (IsInitialized || service == cloudService)
                return;

            service = cloudService;
        }
        public async Task StartUpload()
        {
            var tasks = FilesToUpload.Select(file => UploadFileAsync(file));

            await Task.WhenAll(tasks).ContinueWith(t =>
            {
                UploadCompleted?.Invoke();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        private async Task UploadFileAsync(UploadFile file)
        {
            var response = await service.UploadFileAsync(file);

            if (response == true)
            {
                file.IsUploaded = true;
                ProgressChanged?.Invoke(FilesToUpload.IndexOf(file) + 1, FilesToUpload.Count);
            }
        }
    }
}

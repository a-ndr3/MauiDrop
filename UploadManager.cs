using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDrop
{
    internal class UploadManager
    {
        private static readonly UploadManager instance = new UploadManager();
        public static UploadManager Instance => instance;

        public event Action<int, int> ProgressChanged;
        public event Action UploadCompleted;

        public ObservableCollection<UploadFile> FilesToUpload { get; }

        private UploadManager()
        {
            FilesToUpload = new ObservableCollection<UploadFile>();
        }

        public async Task StartUpload()
        {
            foreach (var file in FilesToUpload)
            {
                if (file.IsUploaded)
                    continue;
                await Task.Delay(3000);
                file.IsUploaded = true; 
            }
            UploadCompleted?.Invoke();
        }
        public void StartUpload1()
        {
            var tasks = FilesToUpload.Select(file => UploadFileAsync(file)).ToList();

            Task.WhenAll(tasks).ContinueWith(t => UploadCompleted?.Invoke());
        }

        private async Task UploadFileAsync(UploadFile file)
        {
            await Task.Delay(1000);  // simulate upload
            ProgressChanged?.Invoke(FilesToUpload.IndexOf(file) + 1, FilesToUpload.Count);
        }

        public bool IsUploading => FilesToUpload.Any(f => !f.Data.CanRead);
    }
}

using Google.Apis.Drive.v3;
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

        private DriveService service;

        public ObservableCollection<UploadFile> FilesToUpload { get; }

        private UploadManager()
        {
            FilesToUpload = new ObservableCollection<UploadFile>();
            service = GDriveHelper.GetDriveService();
        }

        public async Task StartUpload()
        {
            var tasks = FilesToUpload.Select(file => UploadFileAsync(file)).ToList();

            await Task.WhenAll(tasks).ContinueWith(t =>
            {
                UploadCompleted?.Invoke();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        private async Task UploadFileAsync(UploadFile file)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = file.FileName
            };

            FilesResource.CreateMediaUpload request;
            using (var stream = new System.IO.MemoryStream())
            {
                await file.Data.CopyToAsync(stream);
                stream.Seek(0, SeekOrigin.Begin);
                request = service.Files.Create(
                    fileMetadata, stream, GetMimeType(file.FileName));
                request.Fields = "id";
                request.Upload();
            }

            var response = request.ResponseBody;
            if (response != null)
            {
                file.IsUploaded = true;
                ProgressChanged?.Invoke(FilesToUpload.IndexOf(file) + 1, FilesToUpload.Count);
            }
        }
        private string GetMimeType(string fileName)
        {
            string mimeType = "application/octet-stream";
            string ext = System.IO.Path.GetExtension(fileName).ToLowerInvariant();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }
    }
}

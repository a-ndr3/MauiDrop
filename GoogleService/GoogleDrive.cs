using Google.Apis.Drive.v3;
using MauiDrop.DataItems;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDrop.GoogleService
{
    internal class GoogleDrive : Interfaces.ICloudService
    {
        private DriveService driveService;

        private ObservableCollection<CloudFile> GoogleFiles { get; set; }

        public GoogleDrive()
        {
            Init();
        }

        private void Init()
        {
            GDriveHelper.InitializeGoogleDriveAsync().Wait();
            driveService = GDriveHelper.GetDriveService();
            GoogleFiles = new ObservableCollection<CloudFile>();
        }

        public async Task<List<CloudFile>> GetFilesAsync()
        {
            GoogleFiles.Clear();
            FilesResource.ListRequest listRequest = driveService.Files.List();
            listRequest.Fields = "nextPageToken, files(id, name, size, createdTime)";
            listRequest.PageSize = 20;

            var response = await listRequest.ExecuteAsync();

            foreach (var file in response.Files.OrderByDescending(f => f.CreatedTimeDateTimeOffset))
            {
                string? size;
                if (file.Size != null)
                    size = (file.Size / 1024 / 1024 == 0) ? $"{file.Size / 1024} Kb" : $"{file.Size / 1024 / 1024} Mb";
                else
                    size = "";

                GoogleFiles.Add(new CloudFile
                {
                    Name = file.Name,
                    Size = size,
                    UploadDate = file.CreatedTimeDateTimeOffset.HasValue ? file.CreatedTimeDateTimeOffset.Value.ToString("dd.MM.yyyy") : "",
                });
            }
            return GoogleFiles.ToList();
        }

        public async Task<bool> UploadFileAsync(UploadFile file)
        {
            try
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
                    request = driveService.Files.Create(
                        fileMetadata, stream, GetMimeType(file.FileName));
                    request.Fields = "id";
                    await request.UploadAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error uploading file to Google Drive", ex);
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

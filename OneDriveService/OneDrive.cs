using MauiDrop.DataItems;
using Microsoft.Graph.Models;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDrop.OneDriveService
{
    internal class OneDrive : Interfaces.ICloudService
    {
        private GraphServiceClient graphClient;
        private ObservableCollection<CloudFile> OneDriveFiles { get; set; }

        public OneDrive()
        {
            Init();
        }

        private void Init()
        {
            OneDriveHelper.InitializeOneDriveAsync().Wait();
            graphClient = OneDriveHelper.GetGraphServiceClient();
            OneDriveFiles = new ObservableCollection<CloudFile>();
        }

        public async Task<List<CloudFile>> GetFilesAsync()
        {
            Drive? drive = null;
            OneDriveFiles.Clear();
           
            drive = await graphClient.Users[OneDriveHelper.userId].Drive.GetAsync();
           
            var items = drive?.Root?.Children;

            foreach (var item in items)
            {
                string size = item.Size.HasValue ? $"{item.Size / 1024} Kb" : "";
                OneDriveFiles.Add(new CloudFile
                {
                    Name = item.Name,
                    Size = size,
                    UploadDate = item.CreatedDateTime.HasValue ? item.CreatedDateTime.Value.ToString("dd.MM.yyyy") : "",
                });
            }
            return OneDriveFiles.ToList();
        }

        public async Task<bool> UploadFileAsync(UploadFile file)
        {
            try
            {
                //using (var stream = new MemoryStream())
                //{
                //    await file.Data.CopyToAsync(stream);
                //    stream.Seek(0, SeekOrigin.Begin);

                //    await graphClient.Me.Drive.Root.ItemWithPath(file.FileName).Content.Request().PutAsync<DriveItem>(stream);
                //}
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error uploading file to OneDrive", ex);
            }
        }
    }
}

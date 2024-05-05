using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using MauiDrop.DataItems;
using MauiDrop.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MauiDrop.Managers
{
    public class ViewCloudFilesManager
    {
        private ICloudService driveService;
        public ObservableCollection<CloudFile> CloudFiles { get; set; }

        public ViewCloudFilesManager(ICloudService service)
        {
            driveService = service;
            CloudFiles = new ObservableCollection<CloudFile>();
        }

        public async Task LoadFilesAsync()
        {
            var driveFiles = await driveService.GetFilesAsync();
            var temp = new List<CloudFile>();

            driveFiles.ForEach(file =>
            {
                temp.Add(file);
            });

            temp.OrderByDescending(file => file.UploadDate);

            foreach (var file in temp)
            {
                CloudFiles.Add(file);
            }
        }

        public async Task Update()
        {
            if (CloudFiles.Count > 0)
            {
                CloudFiles.Clear();
            }
            await LoadFilesAsync();
        }
    }
}

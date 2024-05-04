using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MauiDrop
{
    public class FilesViewModel
    {
        private DriveService driveService;
        public ObservableCollection<GoogleFile> GoogleFiles { get; set; }

        public FilesViewModel(DriveService service)
        {
            driveService = service;
            GoogleFiles = new ObservableCollection<GoogleFile>();
        }

        public async Task LoadFilesAsync()
        {
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

                GoogleFiles.Add(new GoogleFile
                {
                    Name = file.Name,
                    Size = size,
                    UploadDate = file.CreatedTime.Value.ToString("dd.MM.yyyy")
                });
            }
        }
    }

    public class GoogleFile
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public string UploadDate { get; set; }
    }
}

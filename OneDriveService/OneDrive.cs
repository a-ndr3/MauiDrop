using MauiDrop.DataItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDrop.OneDriveService
{
    internal class OneDrive : Interfaces.ICloudService
    {
        public Task<List<CloudFile>> GetFilesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UploadFileAsync(UploadFile file)
        {
            throw new NotImplementedException();
        }
    }
}

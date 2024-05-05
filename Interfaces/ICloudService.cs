using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiDrop.DataItems;

namespace MauiDrop.Interfaces
{
    public interface ICloudService
    {
        Task<bool> UploadFileAsync(UploadFile file);
        Task<List<CloudFile>> GetFilesAsync();
    }
}

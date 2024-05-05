using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDrop.DataItems
{
    public class UploadFile : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string fileName;
        private Stream data;
        private bool isUploaded;
        private string fileSize;

        public string FileName
        {
            get => fileName;
            set
            {
                fileName = value;
                OnPropertyChanged(nameof(FileName));
            }
        }

        public Stream Data
        {
            get => data;
            set
            {
                data = value;
                OnPropertyChanged(nameof(Data));
                OnPropertyChanged(nameof(FileSize));
            }
        }

        public bool IsUploaded
        {
            get => isUploaded;
            set
            {
                isUploaded = value;
                OnPropertyChanged(nameof(IsUploaded));
            }
        }

        public UploadFile(string fileName, Stream data)
        {
            FileName = fileName;
            Data = data;
            IsUploaded = false;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public string FileSize => GetFileSize();
        public string GetFileSize()
        {
            return (Data.Length / 1024 / 1024 == 0) ? $"{Data.Length / 1024} Kb" : $"{Data.Length / 1024 / 1024} Mb";
        }
    }
}

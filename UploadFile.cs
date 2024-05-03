using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDrop
{
    public class UploadFile : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string fileName;
        private Stream data;
        private bool isUploaded;

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
            this.FileName = fileName;
            this.Data = data;
            this.IsUploaded = false;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

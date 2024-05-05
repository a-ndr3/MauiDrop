using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using Microsoft.Maui.ApplicationModel.DataTransfer;
using Microsoft.Maui.Storage;

namespace MauiDrop;

public partial class UploadPage : ContentPage
{
    public UploadPage()
    {
        InitializeComponent();
        BindingContext = UploadManager.Instance;

        UploadManager.Instance.ProgressChanged += OnUploadProgressChanged;
        UploadManager.Instance.UploadCompleted += OnUploadCompleted;
    }

    private async void OnUploadClicked(object sender, EventArgs e)
    {
        await UploadManager.Instance.StartUpload();
    }

    private void OnUploadProgressChanged(int completed, int total)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            // update UI with progress
        });
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        UploadManager.Instance.ProgressChanged -= OnUploadProgressChanged;
        UploadManager.Instance.UploadCompleted -= OnUploadCompleted;
    }

    private void OnUploadCompleted()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            DisplayAlert("Success", "All files have been uploaded.", "OK");
        });
    }

    void OnDragOver(object sender, DragEventArgs e)
    {
        DropBorder.Stroke = Color.FromHex("#378D83");
#if WINDOWS
    var dragUI = e.PlatformArgs.DragEventArgs.DragUIOverride;
    dragUI.IsCaptionVisible = false;
    dragUI.IsGlyphVisible = false;
#endif
        e.AcceptedOperation = DataPackageOperation.Copy;
    }

    void OnDragLeave(object sender, DragEventArgs e)
    {
        DropBorder.Stroke = Color.FromHex("#E2E6EA");
    }

    private async void OnBrowseFilesClicked(object sender, EventArgs e)
    {
        try
        {
            var pickOptions = new PickOptions
            {
                PickerTitle = "Please select a file"
            };

            var result = await FilePicker.PickMultipleAsync(pickOptions);
            if (result != null)
            {
                foreach (var file in result)
                {
                    var stream = await file.OpenReadAsync();
                    UploadManager.Instance.FilesToUpload.Add(new UploadFile(file.FileName, stream));
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "An error occurred while picking files: " + ex.Message, "OK");
        }
    }

    private async void OnFileDrop(object sender, DropEventArgs e)
    {
        DropBorder.Stroke = Color.FromHex("#E2E6EA");

        var items = e.Data;
        var x = 1;


        //if (e.Data != null && e.Data.Contains(DataPackageOperation.Copy))
        //{
        //    var files = await e.Data.GetFileAsync();
        //    if (files != null)
        //    {
        //        var stream = await files.OpenReadAsync();
        //        uploadManager.FilesToUpload.Add(new UploadFile(files.FileName, stream));
        //    }
        //}

    }

}
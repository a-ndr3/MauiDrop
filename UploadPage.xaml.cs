using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using Microsoft.Maui.ApplicationModel.DataTransfer;
using Microsoft.Maui.Storage;
using MauiDrop.Managers;
using MauiDrop.DataItems;
using MauiDrop.Interfaces;

namespace MauiDrop;

public partial class UploadPage : ContentPage
{
    public UploadPage(ICloudService service)
    {
        InitializeComponent();
        BindingContext = UploadManager.Instance;
        UploadManager.Instance.Initialize(service);

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

        if (UploadManager.Instance.FilesToUpload != null)
        {
            var listOfUploaded = UploadManager.Instance.FilesToUpload.Where(f => f.IsUploaded).ToList();
            foreach (var file in listOfUploaded)
            {
                UploadManager.Instance.FilesToUpload.Remove(file);
            }
        }
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
                await MoveFilesToUpload(result);
            }
            
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "An error occurred while picking files: " + ex.Message, "OK");
        }
    }

    private async Task MoveFilesToUpload(IEnumerable<FileResult> files)
    {
        foreach (var file in files)
        {
            var stream = await file.OpenReadAsync();
            UploadManager.Instance.FilesToUpload.Add(new UploadFile(file.FileName, stream));
        }
    }

    private async void OnFileDrop(object sender, DropEventArgs e)
    {
        DropBorder.Stroke = Color.FromHex("#E2E6EA");
    }

}
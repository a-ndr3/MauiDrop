using Microsoft.Maui.Storage;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MauiDrop;

public partial class Settings : ContentPage
{
    public Settings()
    {
        InitializeComponent();
    }

    private async void OnChooseOneDriveFileClicked(object sender, EventArgs e)
    {
        var result = await FilePicker.Default.PickAsync(new PickOptions
        {
            PickerTitle = "Please select a key .xml file"
        });

        if (result != null)
        {
            if (result.FullPath.EndsWith(".xml"))
            {
                ODFilePathLabel.Text = result.FullPath;
                Preferences.Set("KeyFilePath", result.FullPath);
            }
            else
            {
                await DisplayAlert("Error", "Please select a .xml file", "OK");
            }
        }
    }

    private async void OnChooseFileClicked(object sender, EventArgs e)
    {
        var result = await FilePicker.Default.PickAsync(new PickOptions
        {
            PickerTitle = "Please select a key .json file"
        });

        if (result != null)
        {
            if (result.FullPath.EndsWith(".json"))
            {
                FilePathLabel.Text = result.FullPath;
                Preferences.Set("KeyFilePath", result.FullPath);
            }
            else
            {
                await DisplayAlert("Error", "Please select a .json file", "OK");
            }
        }
    }
}
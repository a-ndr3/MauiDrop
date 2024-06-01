using Microsoft.Maui.Storage;
#if WINDOWS
using Microsoft.UI.Input;
#endif
using System;
using System.IO;
using System.Threading.Tasks;

namespace MauiDrop;

public partial class Settings : ContentPage
{
    public Settings()
    {
        InitializeComponent();
        this.Loaded += SettingsLoaded;
    }

    private void SettingsLoaded(object sender, EventArgs e)
    {
#if WINDOWS
        Microsoft.UI.Xaml.Controls.Button gpathbtn = (Microsoft.UI.Xaml.Controls.Button)OFile.Handler.PlatformView;
        Microsoft.UI.Xaml.Controls.Button onedbtn = (Microsoft.UI.Xaml.Controls.Button)ODriveFile.Handler.PlatformView;
        ElementExtension.ChangeCursor(gpathbtn, InputSystemCursor.Create(InputSystemCursorShape.Hand));
        ElementExtension.ChangeCursor(onedbtn, InputSystemCursor.Create(InputSystemCursorShape.Hand));
        gpathbtn.AddHoverEffect();
        onedbtn.AddHoverEffect();
#endif
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
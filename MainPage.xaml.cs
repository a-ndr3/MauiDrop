
using MauiDrop.GoogleService;
using MauiDrop.Interfaces;
using MauiDrop.OneDriveService;

namespace MauiDrop
{
    public partial class MainPage : ContentPage
    {
        ICloudService? _cloudService;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnGoogleDriveClicked(object sender, EventArgs e)
        {
            if (_cloudService != null || _cloudService is GoogleDrive)
            {
                await DisplayAlert("Already Connected", "You are already connected to a service.", "OK");
                return;
            }
            else
            {
                try
                {
                    _cloudService = new GoogleDrive();

                    if (await GDriveHelper.IsConnected())
                    {
                        GoogleButton(true);
                    }
                    else
                    {
                        GoogleButton(false);
                    }

                }
                catch (Exception ex)
                {
                    _cloudService = null;
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }

        private void GoogleButton(bool connected)
        {
            if (connected)
            {
                GDbutton.Text = "Connected";
                GDbutton.TextColor = Color.Parse("#00FF00");
                return;
            }
            else
            {
                GDbutton.Text = "Google Drive";
                GDbutton.TextColor = Color.Parse("#FFFFFF");
                return;
            }
        }
        
        private void OneDriveButton(bool connected)
        {
            if (connected)
            {
                ODbutton.Text = "Connected";
                ODbutton.TextColor = Color.Parse("#00FF00");
                return;
            }
            else
            {
                ODbutton.Text = "OneDrive";
                ODbutton.TextColor = Color.Parse("#FFFFFF");
                return;
            }
        }

        private async void OnOneDriveClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Error", "Tenant issue", "Cancel");
            
            return;

            if (_cloudService != null || _cloudService is OneDrive)
            {
                await DisplayAlert("Already Connected", "You are already connected to a service.", "OK");
                return;
            }
            else
            {
                try
                {
                    _cloudService = new OneDrive();

                    if (await OneDriveHelper.IsConnected())
                    {
                        OneDriveButton(true);
                    }
                    else
                    {
                        OneDriveButton(false);
                    }
                }
                catch (Exception ex)
                {
                    _cloudService = null;
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }

        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Settings());
        }

        private async void OnUploadFilesClicked(object sender, EventArgs e)
        {
            if (_cloudService != null)
                await Navigation.PushAsync(new UploadPage(_cloudService));
            else
            {
                await DisplayAlert("Not Connected", "Please connect to services first.", "OK");
            }
        }

        private async void OnBrowseFilesClicked(object sender, EventArgs e)
        {
            if (_cloudService != null)
            {
                await Navigation.PushAsync(new FilesPage(_cloudService));
            }
            else
            {
                await DisplayAlert("Not Connected", "Please connect to services first.", "OK");
            }
        }

    }

}

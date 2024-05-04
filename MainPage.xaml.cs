
namespace MauiDrop
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnGoogleDriveClicked(object sender, EventArgs e)
        {
            try
            {
                if (GDriveHelper.IsConnected())
                {
                    GDriveHelper.Disconnect();
                    GDbutton.Text = "Google Drive";
                    GDbutton.TextColor = Color.Parse("#FFFFFF");
                    return;
                }
                else
                {
                    await GDriveHelper.InitializeGoogleDriveAsync();
                    if (GDriveHelper.IsConnected())
                    {
                        GDbutton.Text = "Connected";
                        GDbutton.TextColor = Color.Parse("#00FF00");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void OnOneDriveClicked(object sender, EventArgs e)
        {
            //todo implement connect/disconnect logic + 1 connection at a time
            if (ODbutton.Text == "Connected")
            {
                ODbutton.Text = "OneDrive";
                ODbutton.TextColor = Color.Parse("#FFFFFF");
                return;
            }
            ODbutton.Text = "Connected";
            ODbutton.TextColor = Color.Parse("#00FF00");
        }

        private void OnUploadFilesClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UploadPage());
        }

        private async void OnBrowseFilesClicked(object sender, EventArgs e)
        {
            if (GDriveHelper.IsConnected())
            {
                await Navigation.PushAsync(new FilesPage(GDriveHelper.GetDriveService()));
            }
            else
            {
                await DisplayAlert("Not Connected", "Please connect to services first.", "OK");
            }
        }

    }

}

#if WINDOWS
using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Hosting;
#endif
using MauiDrop.GoogleService;
using MauiDrop.Interfaces;
using MauiDrop.OneDriveService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Controls;


namespace MauiDrop
{

#if WINDOWS
    public static class ElementExtension
    {
        public static void ChangeCursor(this UIElement uiElement, InputCursor cursor)
        {
            Type type = typeof(UIElement);
            type.InvokeMember("ProtectedCursor", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance, null, uiElement, new object[] { cursor });
            
            if (uiElement is FrameworkElement frameworkElement)
            {
                frameworkElement.Margin = new Microsoft.UI.Xaml.Thickness(5);
            }
        }

        public static void AddHoverEffect(this UIElement uiElement)
        {
            uiElement.PointerEntered += (s, e) =>
            {
                Microsoft.UI.Xaml.Media.ScaleTransform scaleTransform = new Microsoft.UI.Xaml.Media.ScaleTransform() { ScaleX = 1.05, ScaleY = 1.05 };
                uiElement.RenderTransform = scaleTransform;
                
            };

            uiElement.PointerExited += (s, e) =>
            {
                Microsoft.UI.Xaml.Media.ScaleTransform scaleTransform = new Microsoft.UI.Xaml.Media.ScaleTransform() { ScaleX = 1.0, ScaleY = 1.0 };
                uiElement.RenderTransform = scaleTransform;               
            };
        }
    }
#endif

    public partial class MainPage : ContentPage
    {
        ICloudService? _cloudService;

        public MainPage()
        {
            InitializeComponent();
            this.Loaded += MainLoaded;
        }

        private void MainLoaded(object sender, EventArgs e)
        {
#if WINDOWS
            Microsoft.UI.Xaml.Controls.Button googleBtn = (Microsoft.UI.Xaml.Controls.Button)GDbutton.Handler.PlatformView;
            Microsoft.UI.Xaml.Controls.Button oneDrvBtn = (Microsoft.UI.Xaml.Controls.Button)ODbutton.Handler.PlatformView;
            Microsoft.UI.Xaml.Controls.Button browseBtn = (Microsoft.UI.Xaml.Controls.Button)UPbtn.Handler.PlatformView;
            Microsoft.UI.Xaml.Controls.Button uploadBtn = (Microsoft.UI.Xaml.Controls.Button)BRSbtn.Handler.PlatformView;
            Microsoft.UI.Xaml.Controls.Button settingsBtn = (Microsoft.UI.Xaml.Controls.Button)STbutton.Handler.PlatformView;
            ElementExtension.ChangeCursor(googleBtn, InputSystemCursor.Create(InputSystemCursorShape.Hand));
            ElementExtension.ChangeCursor(oneDrvBtn, InputSystemCursor.Create(InputSystemCursorShape.Hand));
            ElementExtension.ChangeCursor(browseBtn, InputSystemCursor.Create(InputSystemCursorShape.Hand));
            ElementExtension.ChangeCursor(uploadBtn, InputSystemCursor.Create(InputSystemCursorShape.Hand));
            ElementExtension.ChangeCursor(settingsBtn, InputSystemCursor.Create(InputSystemCursorShape.Hand));
            googleBtn.AddHoverEffect();
            oneDrvBtn.AddHoverEffect();
            browseBtn.AddHoverEffect();
            uploadBtn.AddHoverEffect();
            settingsBtn.AddHoverEffect();
#endif
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

using Google.Apis.Drive.v3;
using MauiDrop.Managers;
#if WINDOWS
using Microsoft.UI.Input;
#endif

namespace MauiDrop;

public partial class FilesPage : ContentPage
{
    private ViewCloudFilesManager viewModel;

    public FilesPage(Interfaces.ICloudService service)
    {
        InitializeComponent();
        this.Loaded += FilesPageLoaded;
        viewModel = new Managers.ViewCloudFilesManager(service);
        BindingContext = viewModel;
    }

    private void FilesPageLoaded(object? sender, EventArgs e)
    {
#if WINDOWS
        Microsoft.UI.Xaml.Controls.Button browseBtn = (Microsoft.UI.Xaml.Controls.Button)GDbutton.Handler.PlatformView;
        ElementExtension.ChangeCursor(browseBtn, InputSystemCursor.Create(InputSystemCursorShape.Hand));
        browseBtn.AddHoverEffect();
#endif
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.LoadFilesAsync();
    }

    private async void Update()
    {
        await viewModel.Update();
    }

    private void GDbutton_Clicked(object sender, EventArgs e)
    {
        Update();
    }
}
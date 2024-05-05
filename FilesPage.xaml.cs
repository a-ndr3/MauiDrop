using Google.Apis.Drive.v3;
using MauiDrop.Managers;

namespace MauiDrop;

public partial class FilesPage : ContentPage
{
    private ViewCloudFilesManager viewModel;

    public FilesPage(Interfaces.ICloudService service)
    {
        InitializeComponent();
        viewModel = new Managers.ViewCloudFilesManager(service);
        BindingContext = viewModel;
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
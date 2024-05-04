using Google.Apis.Drive.v3;

namespace MauiDrop;

public partial class FilesPage : ContentPage
{
    private FilesViewModel viewModel;

    public FilesPage(DriveService service)
    {
        InitializeComponent();
        viewModel = new FilesViewModel(service);
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.LoadFilesAsync();
    }
}
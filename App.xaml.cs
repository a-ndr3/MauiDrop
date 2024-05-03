namespace MauiDrop
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell();
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = new Window();
            window.MaximumHeight = 800;
            window.MaximumWidth = 375;
            window.MinimumHeight = 800;
            window.MinimumWidth = 375;
            window.Title = "MauiDrop";
            window.Page = new AppShell();
            return window;
        }
    }
}

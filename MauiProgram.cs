using Microsoft.Extensions.Logging;
using Google.Apis.Webfonts.v1;

namespace MauiDrop
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>();               

            return builder.Build();
        }
    }
}

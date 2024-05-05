using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace MauiDrop.GoogleService
{
    internal class GDriveHelper
    {
        private static UserCredential credential;
        private static DriveService service;

        public static async Task InitializeGoogleDriveAsync()
        {
            //todo change it to use settings/another resource
            string path = "C:/Users/abloh/source/repos/MauiDrop/Resources/Raw/keys.json";
            string path2 = "C:/Users/abloh/source/repos/MauiDrop/Resources/Raw/token.json";
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    new[] { DriveService.Scope.Drive },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(path2, true));
            }

            service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "MauiDrop",
            });

        }

        public static async Task<bool> IsConnected()
        {
            if (credential != null && credential.Token.IsStale)
            {
                await credential.RefreshTokenAsync(CancellationToken.None);
            }

            return credential != null && !credential.Token.IsStale;
        }

        public static void Disconnect()
        {
            //todo implement disconnect logic
            credential?.RevokeTokenAsync(CancellationToken.None);
        }

        internal static DriveService GetDriveService()
        {
            return service;
        }
    }
}

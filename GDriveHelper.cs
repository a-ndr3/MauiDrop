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

namespace MauiDrop
{
    internal class GDriveHelper
    {
        private static UserCredential credential;
        private static DriveService service;

        public static async Task InitializeGoogleDriveAsync()
        {
            string path = "Resources/raw/keys.json";
            string path2 = "Resources/raw/token.json";
           
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

        public static bool IsConnected()
        {
            return credential != null && !credential.Token.IsStale;
        }

        public static void Disconnect()
        {
            credential?.RevokeTokenAsync(CancellationToken.None);
        }
    }
}

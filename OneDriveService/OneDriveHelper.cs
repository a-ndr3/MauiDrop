using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MauiDrop.OneDriveService
{
    internal class OneDriveHelper
    {
        private static GraphServiceClient graphClient;

        public static string? userId { get; set; }
        public static async Task InitializeOneDriveAsync()
        {
            var clientId = "";
            var tenantId = "";
            var clientSecret = "";

            var confidentialClientApplication = ConfidentialClientApplicationBuilder
                .Create(clientId)
                .WithTenantId(tenantId)
                .WithClientSecret(clientSecret)
                .Build();

            var options = new AuthorizationCodeCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
            };

            var scopes = new[] { "https://graph.microsoft.com/.default" };

            var clientSecretCredential = new ClientSecretCredential(
                 tenantId, clientId, clientSecret, options);

            graphClient = new GraphServiceClient(clientSecretCredential, scopes);
        }

        public static GraphServiceClient GetGraphServiceClient()
        {
            return graphClient;
        }

        public static async Task<bool> IsConnected()
        {
            try
            {
                var user = await graphClient.Users.GetAsync();

                userId = user?.Value?.Last().Id;

                return true;
            }
            catch (ServiceException ex)
            {
                return false;
            }
        }

        public static void Disconnect()
        {

        }
    }
}
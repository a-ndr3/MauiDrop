![logomd](https://github.com/a-ndr3/MauiDrop/assets/66060105/5d75ffea-0b8c-4d64-86c5-9d4da760c4b9)

## This is a small project for the .NET course💻 I am taking

# README

MauiDrop is a file uploader application for the .NET course, supporting Google Drive and OneDrive integrations. This app allows users to authenticate with their cloud storage accounts and upload files seamlessly.

## Features
- Google Drive and OneDrive Integration: Upload files directly to your cloud storage accounts.
- Asynchronous: Smooth user experience with non-blocking file uploads.
- Cross-Platform: Built using .NET MAUI, supporting multiple platforms.

## Getting Started
### Prerequisites
- .NET 6.0 SDK or later
- Google API Client Library for .NET
- Microsoft Graph SDK

## Installation
- Clone the repository to your local machine
```
git clone https://github.com/a-ndr3/MauiDrop.git
cd MauiDrop
```

## Authentication
### Google Drive
- Set Up Credentials: Obtain OAuth 2.0 credentials from the Google Developers Console.
- Configure the Project: Add your client_id and client_secret to the appsettings.json or environment variables.
- Authenticate: The application will prompt for Google authentication on the first run.

### OneDrive
> [!NOTE]
OneDrive support is implemented however there are restrictions preventing usage of API

## Usage
### Uploading Files
- Select Cloud Service: Choose either Google Drive or OneDrive from the main interface.
- Authenticate: Follow the prompts to authenticate with the selected cloud service.
- Upload Files: Select files from your local machine to upload. The application will handle the rest asynchronously.

## Configuration
You can configure using json keys or through environment variables. Key configurations include:
- GoogleClientId
- GoogleClientSecret

![image](https://github.com/a-ndr3/MauiDrop/assets/66060105/f8dc14db-316c-4ab3-88e2-022132afe920)

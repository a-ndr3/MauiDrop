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
> [!NOTE]
You can also check the released versions and download a runnable app.

## Authentication
### Google Drive
- Set Up Credentials: Obtain OAuth 2.0 credentials from the Google Developers Console.
- Configure the Project: Get your keys.json file using [Google Cloud](https://cloud.google.com/)
- Authenticate: The application will prompt for Google authentication on the first run.

### OneDrive
> [!WARNING]
OneDrive support is implemented however there are restrictions preventing usage of API

## Usage
### Uploading Files
- Select Cloud Service: Choose either Google Drive or OneDrive from the main interface.
- Authenticate: Follow the prompts to authenticate with the selected cloud service.
- Upload Files: Select files from your local machine to upload. The application will handle the rest asynchronously.

## Configuration
You can configure the app using Settings button on the Main page.
- For now the configuration includes only storage of the KEY files
  
![image](https://github.com/a-ndr3/MauiDrop/assets/66060105/cd252292-51ec-4a4f-9f1b-e05265cfe890)


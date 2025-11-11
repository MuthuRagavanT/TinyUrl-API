# Tiny Url Project

This is a full-stack **Tiny URL** application built with:
- **Frontend:** Angular 20
- **Backend:** ASP.NET Core Web API (.Net 8)
- **Database:** Azure SQL Database
- **Hosting:** Azure App Service (Frontend + Backend)
- **CI/CD:** GitHub Actions
- **Infrastructure:** Azure Resources (Web App, Azure SQL)

- ---

## Features

### Backend (ASP.NET Core API)
- REST API built using Minimal API / Controller.
- Generates a 6-character short code.
- Redirects short code to the original URL.
- Uses Azure SQL for persistent storage.
- Supports CORS for Angular frontend.
- Validates original URL before saving.

- ## Project Setup Instructions

1. Clone the repository:
   ```bash
   git clone (https://github.com/MuthuRagavanT/TinyUrl-API.git)

2. Add connection string in appsettings.json {Server=tcp:tinyurlserver.database.windows.net,1433;Initial Catalog=tinyurldb;Persist Security Info=False;User ID=adminlogin;Password=tiny@2025;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;}
3. Run the Prject
4. Project is available now on https://localhost:7021/swagger

- ## Deployment Details

- **Azure Service:** Azure Web App (API)
- **URL:** https://tiny-rul-api.azurewebsites.net/swagger/

- ## CI/CD Configuration

- Configured using GitHub Actions.
- Every push to Master triggers: Api build & deploy to tiny-url-api.

- ## Environment Variables

- Stored in Azure App Settings:

- TinyUrlDb
  
- ## Author

- **Name:** Muthu Ragavan T
- **Email:** ragavant517@gmail.com
- **Project Title:** Tiny URL

# Assessment Web API

### Solution WebAPISolution

- dotnet new sln --output WebAPISolution
- cd WebAPISolution
- new-item README.md

#### Project WebAPI

- dotnet new web --name WebAPI -f net5.0
- dotnet sln add .\WebAPI

#### Packages for WebAPI

- dotnet add .\WebAPI package Microsoft.EntityFrameworkCore --version 5.0.17 (Will provide DbContext and DbSet)
- dotnet add .\WebAPI package Microsoft.EntityFrameworkCore.SqlServer --version 5.0.17 (for working with sqlServer)
- dotnet add .\WebAPI package Microsoft.EntityFrameworkCore.Design --version 5.0.17 (will generate migrations)
- dotnet add .\WebAPI package Microsoft.EntityFrameworkCore.Proxies --version 5.0.17 (for LazyLoading)

#### Dotnet Tool for the project WebAPI

- dotnet tool list --global (will give the list of tools installed globally)
- dotnet ef tool
  - dotnet tool install --global dotnet-ef

#### Dotnet ef Command's

- dotnet ef migrations add InitialDb --output-dir Models/Migrations/WebMigrations --context WebAPIDbContext --project .\WebAPI
- dotnet ef database update --project .\WebAPI
- dotnet ef database drop --project .\WebAPI

#### Git Branches

- 01Start
- 02ModelEntities
- 03DbContext
- 04DotnetEFTool
- 05ProductSeedData

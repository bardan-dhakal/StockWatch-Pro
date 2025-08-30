# Stock Management Web API

## Description

This is a .NET Web API project. It serves as a starting point for building a RESTful API using .NET Core.

## Project Structure

- **Solution File**: `api.sln`
- **Project File**: `api.csproj`
- **Configuration Files**: `appsettings.json`, `appsettings.Development.json`
- **Source Files**: `Program.cs`
- **HTTP Request File**: `api.http`
- **Git Ignore File**: `.gitignore`

## Installation

1. Clone the repository:

   ```sh
   git clone <repository-url>
   cd net-stock-management
   ```

2. Open the solution in Visual Studio:
   ```sh
   code api.sln
   ```

## Usage

1. Restore the dependencies:

   ```sh
   dotnet restore
   ```

2. Build the project:

   ```sh
   dotnet build
   ```

3. Run the project:

   ```sh
   dotnet run
   ```

4. Test the API using the `api.http` file or any API testing tool (e.g., Postman, curl).

## Database Configuration

This project uses PostgreSQL as the database. The connection string can be found and modified in the `appsettings.json` file.

### appsettings.json

\`\`\`json
{
"ConnectionStrings": {
"WebApiDatabase": "Host=localhost; Database=stockmanagement; Username=postgres; Password=123"
},
"Logging": {
"LogLevel": {
"Default": "Information",
"Microsoft": "Warning",
"Microsoft.Hosting.Lifetime": "Information"
}
},
"AllowedHosts": "\*"
},
"JWT": {
"Issuer": "http://localhost:5246",
"Audience": "http://localhost:5246",
"SigningKey": "your-secure-signing-key-here"
}
\`\`\`

Ensure that you update the `WebApiDatabase` connection string with your actual PostgreSQL server, database name, username, and password. Also, update the JWT signing key with a secure key for production use.

## Docker Setup

You can run this project using Docker. Follow the steps below to build and run the Docker container.

1. Create a `Dockerfile` in the root directory:

   \`\`\`dockerfile
   FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
   WORKDIR /app
   EXPOSE 80

   FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
   WORKDIR /src
   COPY ["api.csproj", "./"]
   RUN dotnet restore "api.csproj"
   COPY . .
   WORKDIR "/src"
   RUN dotnet build "api.csproj" -c Release -o /app/build

   FROM build AS publish
   RUN dotnet publish "api.csproj" -c Release -o /app/publish

   FROM base AS final
   WORKDIR /app
   COPY --from=publish /app/publish .
   ENTRYPOINT ["dotnet", "api.dll"]
   \`\`\`

2. Build the Docker image:

   ```sh
   docker build -t first-dotnet-web-api .
   ```

3. Run the Docker container:

   ```sh
   docker run -d -p 8080:80 --name first-dotnet-web-api first-dotnet-web-api
   ```

4. Access the API at `http://localhost:8080`.

## Contributing

1. Fork the repository.
2. Create a new feature branch:
   ```sh
   git checkout -b feature/your-feature-name
   ```
3. Commit your changes:
   ```sh
   git commit -m 'Add some feature'
   ```
4. Push to the branch:
   ```sh
   git push origin feature/your-feature-name
   ```
5. Open a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

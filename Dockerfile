# 1. Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app  # This sets the working directory inside the container

# Copy the .csproj file and restore dependencies
COPY BlogApp.AppHost/*.csproj ./  # Make sure this points to the correct directory for your csproj file
RUN dotnet restore

# Copy the entire project and publish the app
COPY . ./  # Copy all project files
RUN dotnet publish -c Release -o /out

# 2. Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out ./

# Expose the port the app will run on
EXPOSE 7042

# Start the app
ENTRYPOINT ["dotnet", "BlogApp.AppHost.dll"]

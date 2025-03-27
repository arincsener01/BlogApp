# 1. Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app  # Set the working directory in the container

# Copy the .csproj file from BlogApp/BlogApp.AppHost to the container
COPY BlogApp.AppHost/BlogApp.AppHost.csproj ./  # Ensure this points to the correct location of your .csproj file

# Set the working directory inside the container to where the .csproj file is located
WORKDIR /app

# Restore dependencies
RUN dotnet restore BlogApp.AppHost.csproj

# Copy all project files from BlogApp/BlogApp.AppHost to the container
COPY BlogApp.AppHost/. ./  # This will copy the rest of the project files into the current directory in the container

# Publish the application
RUN dotnet publish -c Release -o /out

# 2. Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out ./

# Expose the port the app will run on
EXPOSE 7042

# Start the app
ENTRYPOINT ["dotnet", "BlogApp.AppHost.dll"]

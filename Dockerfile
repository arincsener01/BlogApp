# 1. Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app  # Set the working directory in the container

# Copy the .csproj file from the correct path to the /app directory
COPY BlogApp.AppHost/*.csproj ./  # Ensure this points to the correct location of your .csproj file

# Now, change the working directory to the `BlogApp.AppHost` folder to handle the build
WORKDIR /app/BlogApp.AppHost

# Restore dependencies
RUN dotnet restore

# Copy all project files from the current directory into the container
COPY BlogApp.AppHost/. ./  # Copy all files from BlogApp.AppHost to the current directory in the container

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

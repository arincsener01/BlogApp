# Build aşaması
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Solution ve proje dosyalarını kopyala
COPY ["BlogApp.sln", "./"]
COPY ["API.BLOG/API.BLOG.csproj", "API.BLOG/"]
COPY ["APP.BLOG/APP.BLOG.csproj", "APP.BLOG/"]
COPY ["CORE/CORE.csproj", "CORE/"]
COPY ["BlogApp.ServiceDefaults/BlogApp.ServiceDefaults.csproj", "BlogApp.ServiceDefaults/"]
COPY ["BlogApp.AppHost/BlogApp.AppHost.csproj", "BlogApp.AppHost/"]

# Aspire workload'ını yükle
RUN dotnet workload install aspire

# Bağımlılıkları geri yükle
RUN dotnet restore

# Tüm kaynak kodları kopyala
COPY . .

# Uygulamayı derle ve yayınla
RUN dotnet build -c Release -o /app/build
RUN dotnet publish -c Release -o /app/publish

# Runtime aşaması
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "API.BLOG.dll"] 

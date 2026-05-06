# Build Stage / Derleme Aşaması
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files and restore dependencies
# Proje dosyalarını kopyala ve bağımlılıkları yükle
COPY ["InventoryService.API/InventoryService.API.csproj", "InventoryService.API/"]
COPY ["InventoryService.Application/InventoryService.Application.csproj", "InventoryService.Application/"]
COPY ["InventoryService.Domain/InventoryService.Domain.csproj", "InventoryService.Domain/"]
COPY ["InventoryService.Infrastructure/InventoryService.Infrastructure.csproj", "InventoryService.Infrastructure/"]
RUN dotnet restore "InventoryService.API/InventoryService.API.csproj"

# Copy all files and build the application
# Tüm dosyaları kopyala ve uygulamayı derle
COPY . .
WORKDIR "/src/InventoryService.API"
RUN dotnet publish "InventoryService.API.csproj" -c Release -o /app/publish

# Runtime Stage / Çalışma Zamanı Aşaması
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "InventoryService.API.dll"]
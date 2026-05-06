# Build Stage / Derleme Aşaması
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files and restore dependencies
# Proje dosyalarını kopyala ve bağımlılıkları yükle
COPY ["OrderService.API/OrderService.API.csproj", "OrderService.API/"]
COPY ["OrderService.Application/OrderService.Application.csproj", "OrderService.Application/"]
COPY ["OrderService.Domain/OrderService.Domain.csproj", "OrderService.Domain/"]
COPY ["OrderService.Infrastructure/OrderService.Infrastructure.csproj", "OrderService.Infrastructure/"]
RUN dotnet restore "OrderService.API/OrderService.API.csproj"

# Copy all files and build the application
# Tüm dosyaları kopyala ve uygulamayı derle
COPY . .
WORKDIR "/src/OrderService.API"
RUN dotnet publish "OrderService.API.csproj" -c Release -o /app/publish

# Runtime Stage / Çalışma Zamanı Aşaması
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "OrderService.API.dll"]
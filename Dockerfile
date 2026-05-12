# =========================
# STAGE 1: Build
# =========================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiamos todos los archivos del repositorio para resolver dependencias entre proyectos
COPY . .

# Restauramos dependencias
RUN dotnet restore "SistemaVentasAPI/SistemaVentasAPI.csproj"

# Publicamos la aplicación
# Usamos -a $TARGETARCH por si Render usa diferentes arquitecturas, aunque por defecto es x64
RUN dotnet publish "SistemaVentasAPI/SistemaVentasAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# =========================
# STAGE 2: Runtime
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copiamos los archivos publicados
COPY --from=build /app/publish .

# .NET 9, al igual que el 8, escucha en el puerto 8080 por defecto
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# El nombre del DLL debe ser exactamente el nombre de tu proyecto de salida
ENTRYPOINT ["dotnet", "SistemaVentasAPI.dll"]
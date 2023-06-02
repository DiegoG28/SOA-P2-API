# Establece la imagen base
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# Establece el directorio de trabajo
WORKDIR /app

# Copia el archivo de proyecto y restaura las dependencias
COPY SOA-P2/SOA-P2.csproj .
RUN dotnet restore

# Copia el resto de los archivos de la aplicación
COPY . .

# Publica la aplicación
RUN dotnet publish -c Release -o out

# Establece la imagen base para la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .

# Establece el comando de inicio de la aplicación
ENTRYPOINT ["dotnet", "SOA-P2.dll"]


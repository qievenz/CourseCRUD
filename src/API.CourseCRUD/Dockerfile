# Establece la imagen base para .NET 8 SDK
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Establece la imagen para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/API.CourseCRUD/API.CourseCRUD.csproj", "src/API.CourseCRUD/"]
COPY ["src/Application.CourseCRUD/Application.CourseCRUD.csproj", "src/Application.CourseCRUD/"]
COPY ["src/Infrastructure.CourseCRUD/Infrastructure.CourseCRUD.csproj", "src/Infrastructure.CourseCRUD/"]
COPY ["src/Core.CourseCRUD/Core.CourseCRUD.csproj", "src/Core.CourseCRUD/"]
RUN dotnet restore "src/API.CourseCRUD/API.CourseCRUD.csproj"

# Copia el código y compila la aplicación
COPY . .
WORKDIR "/src/src/API.CourseCRUD"
RUN dotnet build "API.CourseCRUD.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API.CourseCRUD.csproj" -c Release -o /app/publish

# Usa la imagen base para correr la aplicación
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.CourseCRUD.dll"]

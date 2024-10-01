FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/API.CourseCRUD/API.CourseCRUD.csproj", "src/API.CourseCRUD/"]
COPY ["src/Application.CourseCRUD/Application.CourseCRUD.csproj", "src/Application.CourseCRUD/"]
COPY ["src/Infrastructure.CourseCRUD/Infrastructure.CourseCRUD.csproj", "src/Infrastructure.CourseCRUD/"]
COPY ["src/Core.CourseCRUD/Core.CourseCRUD.csproj", "src/Core.CourseCRUD/"]
RUN dotnet restore "src/API.CourseCRUD/API.CourseCRUD.csproj"

COPY . .
WORKDIR "/src/src/API.CourseCRUD"
RUN dotnet build "API.CourseCRUD.csproj" -c Release -o /app/build

RUN find . -name *.sln -print0 | xargs -r -n 1 -0 dotnet test

FROM build AS publish
RUN dotnet publish "API.CourseCRUD.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.CourseCRUD.dll"]

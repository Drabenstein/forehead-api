#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/ForeheadApi/ForeheadApi.csproj", "src/ForeheadApi/"]
COPY ["src/ForeheadApi.Core/ForeheadApi.Core.csproj", "src/ForeheadApi.Core/"]
COPY ["src/ForeheadApi.Dtos/ForeheadApi.Dtos.csproj", "src/ForeheadApi.Dtos/"]
COPY ["src/ForeheadApi.Infrastructure/ForeheadApi.Infrastructure.csproj", "src/ForeheadApi.Infrastructure/"]
RUN dotnet restore "src/ForeheadApi/ForeheadApi.csproj"
COPY . .
WORKDIR "/src/src/ForeheadApi"
RUN dotnet build "ForeheadApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ForeheadApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ForeheadApi.dll"]
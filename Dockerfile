FROM mcr.microsoft.com/dotnet/sdk:7.0-jammy AS build
WORKDIR /source

COPY src/ForeheadApi/*.csproj ForeheadApi/
COPY src/ForeheadApi.Infrastructure/*.csproj ForeheadApi.Infrastructure/
COPY src/ForeheadApi.Core/*.csproj ForeheadApi.Core/
COPY src/ForeheadApi.Dtos/*.csproj ForeheadApi.Dtos/
RUN dotnet restore --use-current-runtime ForeheadApi/ForeheadApi.csproj

COPY src/ForeheadApi/ ForeheadApi/
COPY src/ForeheadApi.Infrastructure/ ForeheadApi.Infrastructure/
COPY src/ForeheadApi.Core/ ForeheadApi.Core/
COPY src/ForeheadApi.Dtos/ ForeheadApi.Dtos/

FROM build AS publish
WORKDIR /source/ForeheadApi
RUN dotnet publish --use-current-runtime --no-restore -o /app

FROM mcr.microsoft.com/dotnet/nightly/aspnet:7.0-jammy-chiseled AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ForeheadApi.dll"]
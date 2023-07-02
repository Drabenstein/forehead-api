FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:7.0-jammy AS build
ARG TARGETARCH
WORKDIR /source

COPY src/ForeheadApi/*.csproj ForeheadApi/
COPY src/ForeheadApi.Infrastructure/*.csproj ForeheadApi.Infrastructure/
COPY src/ForeheadApi.Core/*.csproj ForeheadApi.Core/
COPY src/ForeheadApi.Dtos/*.csproj ForeheadApi.Dtos/
RUN dotnet restore --use-current-runtime -a $TARGETARCH ForeheadApi/ForeheadApi.csproj

COPY src/ForeheadApi/ ForeheadApi/
COPY src/ForeheadApi.Infrastructure/ ForeheadApi.Infrastructure/
COPY src/ForeheadApi.Core/ ForeheadApi.Core/
COPY src/ForeheadApi.Dtos/ ForeheadApi.Dtos/
RUN dotnet publish --use-current-runtime -a $TARGETARCH -c Release --self-contained false --no-restore -o /app ForeheadApi/ForeheadApi.csproj

FROM mcr.microsoft.com/dotnet/nightly/aspnet:7.0-jammy-chiseled AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["./ForeheadApi"]
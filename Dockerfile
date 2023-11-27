FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine-arm64v8 AS build-env

WORKDIR /app

COPY ModuloAutenticacao.Api/*.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish ModuloAutenticacao.Api/ModuloAutenticacao.Api.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine-arm64v8
WORKDIR /app
COPY --from=build-env /app/out .


ENTRYPOINT ["dotnet", "ModuloAutenticacao.Api.dll"]
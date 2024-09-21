ARG OTEL_VERSION=1.4.0

#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["app/Domain/Microservices.Demo.Product.API/Microservices.Demo.Product.API.csproj", "app/Domain/Microservices.Demo.Product.API/"]
RUN dotnet restore "app/Domain/Microservices.Demo.Product.API/Microservices.Demo.Product.API.csproj"
COPY . .
WORKDIR "/src/app/Domain/Microservices.Demo.Product.API"

FROM build AS publish
RUN dotnet publish "Microservices.Demo.Product.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
ARG OTEL_VERSION
WORKDIR /app
COPY --from=publish /app/publish .

RUN apt-get update && apt-get install -y curl

ADD https://github.com/open-telemetry/opentelemetry-dotnet-instrumentation/releases/download/v${OTEL_VERSION}/otel-dotnet-auto-install.sh otel-dotnet-auto-install.sh
RUN apt-get update && apt-get install -y unzip && \
    OTEL_DOTNET_AUTO_HOME="/otel-dotnet-auto" sh otel-dotnet-auto-install.sh

ENTRYPOINT ["dotnet", "Microservices.Demo.Product.API.dll"]
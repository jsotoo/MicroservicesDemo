FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["net-prometheus.csproj", "./"]
RUN dotnet restore "net-prometheus.csproj"

COPY . .
WORKDIR /src
RUN dotnet build "net-prometheus.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "net-prometheus.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "net-prometheus.dll"]
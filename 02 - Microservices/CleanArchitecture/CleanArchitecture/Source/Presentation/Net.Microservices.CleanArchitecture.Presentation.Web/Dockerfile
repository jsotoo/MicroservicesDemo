#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Presentation/Net.Microservices.CleanArchitecture.Presentation.Web/Net.Microservices.CleanArchitecture.Presentation.Web.csproj", "Presentation/Net.Microservices.CleanArchitecture.Presentation.Web/"]
COPY ["Presentation/Net.Microservices.CleanArchitecture.Presentation.Framework/Net.Microservices.CleanArchitecture.Presentation.Framework.csproj", "Presentation/Net.Microservices.CleanArchitecture.Presentation.Framework/"]
COPY ["Core/Net.Microservices.CleanArchitecture.Core.Application/Net.Microservices.CleanArchitecture.Core.Application.csproj", "Core/Net.Microservices.CleanArchitecture.Core.Application/"]
COPY ["Core/Net.Microservices.CleanArchitecture.Core.Domain/Net.Microservices.CleanArchitecture.Core.Domain.csproj", "Core/Net.Microservices.CleanArchitecture.Core.Domain/"]
COPY ["Infrastructure/Net.Microservices.CleanArchitecture.Infrastructure/Net.Microservices.CleanArchitecture.Infrastructure.csproj", "Infrastructure/Net.Microservices.CleanArchitecture.Infrastructure/"]
COPY ["Infrastructure/Net.Microservices.CleanArchitecture.Infrastructure.Shared/Net.Microservices.CleanArchitecture.Infrastructure.Shared.csproj", "Infrastructure/Net.Microservices.CleanArchitecture.Infrastructure.Shared/"]
COPY ["Infrastructure/Net.Microservices.CleanArchitecture.Infrastructure.Auditing/Net.Microservices.CleanArchitecture.Infrastructure.Auditing.csproj", "Infrastructure/Net.Microservices.CleanArchitecture.Infrastructure.Auditing/"]
COPY ["Infrastructure/Net.Microservices.CleanArchitecture.Infrastructure.Resources/Net.Microservices.CleanArchitecture.Infrastructure.Resources.csproj", "Infrastructure/Net.Microservices.CleanArchitecture.Infrastructure.Resources/"]
COPY ["Net.Microservices.CleanArchitecture.Common/Net.Microservices.CleanArchitecture.Common.csproj", "Net.Microservices.CleanArchitecture.Common/"]
RUN dotnet restore "Presentation/Net.Microservices.CleanArchitecture.Presentation.Web/Net.Microservices.CleanArchitecture.Presentation.Web.csproj"
COPY . .
WORKDIR "/src/Presentation/Net.Microservices.CleanArchitecture.Presentation.Web"
RUN dotnet build "Net.Microservices.CleanArchitecture.Presentation.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Net.Microservices.CleanArchitecture.Presentation.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Net.Microservices.CleanArchitecture.Presentation.Web.dll"]

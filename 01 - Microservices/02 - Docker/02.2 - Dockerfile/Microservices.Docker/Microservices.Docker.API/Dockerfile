FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app
#Physical to Container
COPY bin/Release/net8.0/publish .

ENTRYPOINT ["dotnet","Microservices.Docker.API.dll"]
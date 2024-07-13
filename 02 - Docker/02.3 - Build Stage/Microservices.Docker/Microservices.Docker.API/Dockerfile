FROM mcr.microsoft.com/dotnet/sdk:8.0

WORKDIR /api
COPY . .

RUN dotnet dev-certs https
RUN dotnet restore
RUN dotnet publish -o /publish

WORKDIR /publish

ENTRYPOINT ["dotnet","/publish/Microservices.Docker.API.dll","--urls", "http://+:8080;https://+:443"]
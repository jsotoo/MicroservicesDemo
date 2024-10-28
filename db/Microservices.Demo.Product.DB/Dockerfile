#FROM mcr.microsoft.com/mssql/server:2022-latest AS sqlserver
##FROM microsoft/mssql-server-linux
#ENV SA_PASSWORD=Password1234
#ENV ACCEPT_EULA=Y
#COPY ["db/Microservices.Demo.Product.DB/Docker/entrypoint.sh", "/var/opt/sqlserver/entrypoint.sh"]
#COPY ["db/Microservices.Demo.Product.DB/Docker/SqlCmdStartup.sh", "/var/opt/sqlserver/SqlCmdStartup.sh"]
#COPY ["db/Microservices.Demo.Product.DB/Docker/SqlCmdScript.sql", "/var/opt/sqlserver/SqlCmdScript.sql"]
#USER root
#RUN  chmod +x /var/opt/sqlserver/SqlCmdStartup.sh
#USER mssql
#CMD /bin/bash /var/opt/sqlserver/entrypoint.sh


FROM mcr.microsoft.com/mssql/server:2022-latest

ENV SA_PASSWORD=Password1234
ENV ACCEPT_EULA=Y

USER root

# Determina la versión de Ubuntu
RUN . /etc/os-release && \
    echo "deb [arch=amd64] https://packages.microsoft.com/ubuntu/$VERSION_ID/prod $VERSION_CODENAME main" > /etc/apt/sources.list.d/mssql-release.list

RUN apt-get update && \
    apt-get install -y wget gnupg && \
    wget -qO- https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && \
    apt-get update && \
    ACCEPT_EULA=Y apt-get install -y mssql-tools unixodbc-dev && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

ENV PATH="$PATH:/opt/mssql-tools/bin"

COPY ["db/Microservices.Demo.Product.DB/Docker/entrypoint.sh", "/var/opt/sqlserver/entrypoint.sh"]
COPY ["db/Microservices.Demo.Product.DB/Docker/SqlCmdStartup.sh", "/var/opt/sqlserver/SqlCmdStartup.sh"]
COPY ["db/Microservices.Demo.Product.DB/Docker/SqlCmdScript.sql", "/var/opt/sqlserver/SqlCmdScript.sql"]

RUN chmod +x /var/opt/sqlserver/entrypoint.sh && \
    chmod +x /var/opt/sqlserver/SqlCmdStartup.sh

USER mssql

CMD /bin/bash /var/opt/sqlserver/entrypoint.sh
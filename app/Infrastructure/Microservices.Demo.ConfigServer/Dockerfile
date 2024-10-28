FROM openjdk:17-slim-buster AS base
WORKDIR /app

FROM maven:3.8.4-openjdk-17 AS build
WORKDIR /build

COPY pom.xml ./
RUN mvn dependency:go-offline

FROM build AS publish
COPY src ./src
RUN mvn -f pom.xml clean package -DskipTests

FROM base AS final
WORKDIR /app
COPY --from=publish /build/target/*.jar ./app.jar
COPY opentelemetry-javaagent.jar ./opentelemetry-javaagent.jar
COPY elastic-apm-agent.jar ./elastic-apm-agent.jar

ENV ELASTIC_APM_SERVER_URL=http://microservices.demo.apm-server:8200
ENV ELASTIC_APM_SERVICE_NAME=Microservices.Demo.ConfigServer
ENV ELASTIC_APM_ENVIRONMENT=production 

#ENTRYPOINT ["java", "-javaagent:opentelemetry-javaagent.jar", "-Dotel.exporter.otlp.endpoint=http://microservices.demo.otel-collector:4317", "-jar", "/app/app.jar"]
ENTRYPOINT ["java", "-javaagent:elastic-apm-agent.jar", "-javaagent:opentelemetry-javaagent.jar", "-Dotel.exporter.otlp.endpoint=http://microservices.demo.otel-collector:4317", "-Dotel.service.name=Microservices.Demo.ConfigServer","-Dotel.environment=production","-jar", "/app/app.jar"]
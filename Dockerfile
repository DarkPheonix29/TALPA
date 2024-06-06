# Use the .NET Core 8 runtime image as base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 443

RUN apt-get update && \
    apt-get install -y openconnect dos2unix && \
    apt-get clean

# Use the .NET Core 8 SDK image as build environment 
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the .csproj and restore dependencies
COPY ["TALPA/TALPA.csproj", "TALPA/"]
RUN dotnet restore "TALPA/TALPA.csproj"

# Copy the rest of the source code and build the application
COPY . .
WORKDIR "/src/TALPA"
RUN dotnet build "TALPA.csproj" -c Release -o /app --use-current-runtime --self-contained false

# Publish the application
FROM build AS publish
RUN dotnet publish "TALPA.csproj" -c Release -o /app/publish

# Final stage, copy the published application to the base image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Copy the SSL certificate and key
COPY certs/localhost.crt /etc/ssl/certs/localhost.crt
COPY certs/localhost.key /etc/ssl/private/localhost.key
RUN chmod 644 /etc/ssl/certs/localhost.crt

# Copy entrypoint script
WORKDIR /
COPY entrypoint.sh .
RUN dos2unix entrypoint.sh
RUN chmod +x entrypoint.sh

# Configure Kestrel to use HTTPS with the local certificate
ENV ASPNETCORE_URLS="https://+"
ENV ASPNETCORE_HTTPS_PORT=443
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/etc/ssl/certs/localhost.crt
ENV ASPNETCORE_Kestrel__Certificates__Default__KeyPath=/etc/ssl/private/localhost.key

USER root
ENTRYPOINT ["./entrypoint.sh"]

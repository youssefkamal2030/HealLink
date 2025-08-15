FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["HealLink.API/HealLink.Api.csproj", "HealLink.API/"]
COPY ["HealLink.Contracts/HealLink.Contracts.csproj", "HealLink.Contracts/"]
COPY ["HealLink.Domain/HealLink.Domain.csproj", "HealLink.Domain/"]
COPY ["healLink.Application/healLink.Application.csproj", "healLink.Application/"]
COPY ["HealLink.Infrastructure/HealLink.Infrastructure.csproj", "HealLink.Infrastructure/"]

RUN dotnet restore "HealLink.API/HealLink.Api.csproj"

# Copy all source code
COPY . .

# Build the application
WORKDIR "/src/HealLink.API"
RUN dotnet build "HealLink.Api.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "HealLink.Api.csproj" -c Release -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Create directory for uploads
RUN mkdir -p /app/wwwroot/Uploads

COPY --from=publish /app/publish .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Expose port 8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "HealLink.Api.dll"]
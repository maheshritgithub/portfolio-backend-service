# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore
COPY Portfolio.Service.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080

# Render uses PORT env variable; ASP.NET must bind to it
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}

ENTRYPOINT ["dotnet", "Portfolio.Service.dll"]

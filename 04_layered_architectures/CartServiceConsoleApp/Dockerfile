# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution and all project files
COPY ECommerceApp.sln ./
COPY RestApi/RestApi.csproj ./RestApi/
COPY CatalogService.Application/CatalogService.Application.csproj ./CatalogService.Application/
COPY CatalogService.Domain/CatalogService.Domain.csproj ./CatalogService.Domain/
COPY CatalogService.Infrastructure/CatalogService.Infrastructure.csproj ./CatalogService.Infrastructure/
COPY CatalogService.DataAccess/CatalogService.DataAccess.csproj ./CatalogService.DataAccess/

# Clear NuGet cache and restore dependencies
RUN dotnet nuget locals all --clear
RUN dotnet restore RestApi/RestApi.csproj

# Copy the rest of the application code
COPY RestApi/ ./RestApi/
COPY CatalogService.Application/ ./CatalogService.Application/
COPY CatalogService.Domain/ ./CatalogService.Domain/
COPY CatalogService.Infrastructure/ ./CatalogService.Infrastructure/
COPY CatalogService.DataAccess/ ./CatalogService.DataAccess/

# Build and publish the application
RUN dotnet publish RestApi/RestApi.csproj -c Release -o /app/publish

# Use the official .NET runtime image for the final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "RestApi.dll"]
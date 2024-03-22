# Use the official image as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image to build the project files
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore as distinct layers
COPY ["OrderService.Api/OrderService.Api.csproj", "./Services/Order/OrderService.Api/"]
COPY ["OrderService.Application/OrderService.Application.csproj", "./Services/Order/OrderService.Application/"]
COPY ["OrderService.Domain/OrderService.Domain.csproj", "./Services/Order/OrderService.Domain/"]
COPY ["OrderService.Infrastructure/OrderService.Infrastructure.csproj", "./Services/Order/OrderService.Infrastructure/"]

# Restore the projects
RUN dotnet restore "./Services/Order/OrderService.Api/OrderService.Api.csproj"
RUN dotnet restore "./Services/Order/OrderService.Application/OrderService.Application.csproj"
RUN dotnet restore "./Services/Order/OrderService.Domain/OrderService.Domain.csproj"
RUN dotnet restore "./Services/Order/OrderService.Infrastructure/OrderService.Infrastructure.csproj"

# Copy everything else and build
COPY . .
RUN dotnet build "OrderService.Api/OrderService.Api.csproj" -c Release -o /app/build

# Publish the project
FROM build AS publish
RUN dotnet publish "OrderService.Api/OrderService.Api.csproj" -c Release -o /app/publish

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderService.Api.dll"]

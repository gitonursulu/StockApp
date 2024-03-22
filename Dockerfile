FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Order.OrderAPI/Order.OrderAPI.csproj", "Order.OrderAPI/"]
COPY ["Order.Application/Order.Application.csproj", "Order.Application/"]
COPY ["Order.Domain/Order.Domain.csproj", "Order.Domain/"]
COPY ["Order.Infrastructure/Order.Infrastructure.csproj", "Order.Infrastructure/"]
RUN dotnet restore "Order.OrderAPI/Order.OrderAPI.csproj"
RUN dotnet restore "Order.Application/Order.Application.csproj"
RUN dotnet restore "Order.Domain/Order.Domain.csproj"
RUN dotnet restore "Order.Infrastructure/Order.Infrastructure.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "Order.OrderAPI/Order.OrderAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Order.OrderAPI/Order.OrderAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.OrderAPI.dll"]
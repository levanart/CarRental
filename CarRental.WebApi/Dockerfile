FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CarRental.WebApi/CarRental.WebApi.csproj", "CarRental.WebApi/"]
COPY ["CarRental.Application/CarRental.Application.csproj", "CarRental.Application/"]
COPY ["CarRental.Domain/CarRental.Domain.csproj", "CarRental.Domain/"]
COPY ["CarRental.Persistence/CarRental.Persistence.csproj", "CarRental.Persistence/"]
RUN dotnet restore "CarRental.WebApi/CarRental.WebApi.csproj"
COPY . .
WORKDIR "/src/CarRental.WebApi"
RUN dotnet build "./CarRental.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CarRental.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarRental.WebApi.dll"]

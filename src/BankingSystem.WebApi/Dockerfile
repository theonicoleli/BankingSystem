FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/BankingSystem.WebApi/BankingSystem.WebApi.csproj", "src/BankingSystem.WebApi/"]
COPY ["src/BankingSystem.Application/BankingSystem.Application.csproj", "src/BankingSystem.Application/"]
COPY ["src/BankingSystem.Infrastructure/BankingSystem.Infrastructure.csproj", "src/BankingSystem.Infrastructure/"]
COPY ["src/BankingSystem.Domain/BankingSystem.Domain.csproj", "src/BankingSystem.Domain/"]
COPY ["src/BankingSystem.SharedKernel/BankingSystem.SharedKernel.csproj", "src/BankingSystem.SharedKernel/"]

RUN dotnet restore "src/BankingSystem.WebApi/BankingSystem.WebApi.csproj"

COPY . .
WORKDIR "/src/src/BankingSystem.WebApi"
RUN dotnet build "BankingSystem.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "BankingSystem.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BankingSystem.WebApi.dll"]
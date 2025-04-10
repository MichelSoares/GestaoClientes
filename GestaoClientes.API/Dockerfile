FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

# build 
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["GestaoClientes.API/GestaoClientes.API.csproj", "GestaoClientes.API/"]
COPY ["GestaoClientes.Domain/GestaoClientes.Domain.csproj", "GestaoClientes.Domain/"]
COPY ["GestaoClientes.Infrastructure/GestaoClientes.Infrastructure.csproj", "GestaoClientes.Infrastructure/"]

RUN dotnet restore "GestaoClientes.API/GestaoClientes.API.csproj"

COPY . .

# compila
WORKDIR "/src/GestaoClientes.API"
RUN dotnet build "GestaoClientes.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GestaoClientes.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# start API
ENTRYPOINT ["dotnet", "GestaoClientes.API.dll"]

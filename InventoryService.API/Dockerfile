#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
WORKDIR /src
COPY ["InventoryService.API/InventoryService.API.csproj", "InventoryService.API/"]
COPY ["Events/SharedItems/SharedItems.csproj", "Events/SharedItems/"]
COPY ["../banreservaTecnicalTest/banreservaTecnicalTest.Infrastructure/Infrastructure.csproj", "../banreservaTecnicalTest/banreservaTecnicalTest.Infrastructure/"]
COPY ["../banreservaTecnicalTest/banreservaTecnicalTest.Library/DomainLayer.csproj", "../banreservaTecnicalTest/banreservaTecnicalTest.Library/"]
RUN dotnet restore "InventoryService.API/InventoryService.API.csproj"
COPY . .
WORKDIR "/src/InventoryService.API"
RUN dotnet build "InventoryService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InventoryService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InventoryService.API.dll"]
